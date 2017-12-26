﻿using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json;
using System.IO.Compression;
using SharpDj;
using SharpDj.View;
using Debug = SharpDj.Models.Helpers.Debug;

namespace Updater
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Debug _debug;

        public MainWindow()
        {
            _debug = new Debug("Updater");

            Topmost = true;
            InitializeComponent();
            MediaElement.MediaEnded += MediaElement_MediaEnded;
            var task = Task.Factory.StartNew(Init);
        }

        private async Task Init()
        {
            _debug.Log("Local json");
            if (!File.Exists("config.json"))
            {
              _debug.Log("config.json doesn't exists");
            }

            var localJson = File.ReadAllText("config.json");
            var local = JsonConvert.DeserializeObject<FTPChecker>(localJson);

            _debug.Log("Ftp Json");
            var ftpJson = GetSourcePage.GetSource(local.UpdateUrl);
            var ftp = JsonConvert.DeserializeObject<FTPChecker>(ftpJson);

            if (Directory.Exists("tmp"))
                Directory.Delete("tmp", true);

            _debug.Log("Check Update Token");
            if (ftp.UpdateToken != local.UpdateToken)
            {
                _debug.Log("localToken: " + local.UpdateToken);
                _debug.Log("ftpToken: " +  ftp.UpdateToken);
                _debug.Log("Updating");
                Directory.CreateDirectory("tmp");
                _debug.Log("Download");
                await DownloadAsync(ftp.ZipToUpdate, "tmp/update.zip");
                ZipFile.ExtractToDirectory("tmp/update.zip", "tmp/");
                _debug.Log("Extract");
                File.Delete("tmp/update.zip");

                Directory.CreateDirectory("backup/backup " + local.Version);
                _debug.Log("Replace");
                foreach (var file in Directory.GetFiles("tmp/"))
                {   
                    var fileName = Path.GetFileName(file);

                    if (File.Exists(fileName))
                        File.Replace(file, fileName, @"backup/backup " + local.Version + "/" + fileName);
                    else
                        File.Move(file, fileName);
                }
                Directory.Delete("tmp", true);
                _debug.Log("Restarting");
                
                await Dispatcher.BeginInvoke((Action)delegate ()
                {
                    App.Current.Shutdown();
                    Process.Start("SharpDj.exe");
                    Close();
                });
            }
            _debug.Log("Closing updater");
            await Dispatcher.BeginInvoke((Action)delegate ()
            {                
                SdjMainView view = new SdjMainView();
                view.Show();
                Close();
            });
        }


        public async Task DownloadAsync(string url, string path)
        {
            WebClient wc = new WebClient();
            var download = wc.DownloadFileTaskAsync(url, path);
            await download;
        }

        private void MediaElement_MediaEnded(object sender, RoutedEventArgs e)
        {
            MediaElement.Position = TimeSpan.Zero;
            MediaElement.Play();
        }
    }
}
