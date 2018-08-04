﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Communication.Client;
using Communication.Client.Logic.ResponseActions;
using Communication.Shared;
using Newtonsoft.Json;
using SharpDj.Core;
using SharpDj.Enums;
using SharpDj.Enums.Menu;
using SharpDj.Enums.User;
using SharpDj.Models.Helpers;
using SharpDj.ViewModel;
using SharpDj.ViewModel.Model;
using YoutubeExplode;

namespace SharpDj.Models.Client
{
    public class ClientLogic : BaseViewModel
    {
        private UserState _userState = UserState.NotLoggedIn;
        public UserState UserState
        {
            get => _userState;
            set
            {
                _userState = value;
                SdjMainViewModel.MainViewVisibility = value == UserState.Logged ? MainView.Main : MainView.Login;
            }
        }

        public ClientLogic(SdjMainViewModel main)
        {
            SdjMainViewModel = main;

            main.Client.Receiver.LoginErr += Receiver_LoginErr;
            main.Client.Receiver.RegisterAccExistErr += Receiver_RegisterAccExistErr;
            main.Client.Receiver.RegisterErr += Receiver_RegisterErr;
            main.Client.Receiver.SuccesfulRegister += Receiver_SuccesfulRegister;
            Communication.Client.Logic.ResponseActions.UpdateDjResponse.UpdateDj += UpdateDjResponseOnUpdateDj;
          //  main.Client.Receiver.SuccessfulLogin += Receiver_SuccessfulLogin;
         //  main.Client.Receiver.UpdateDj += Receiver_UpdateDj;
            
            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    if (UserState == UserState.Logged)
                    {
                        RefreshInfo();
                        Thread.Sleep(20000);
                    }
                    Thread.Sleep(100);
                }
            });
        }

        private void UpdateDjResponseOnUpdateDj(object sender, UpdateDjResponse.UpdateDjEventArgs e)
        {
            throw new NotImplementedException();
        }

/*        private void Receiver_UpdateDj(object sender, ClientReceiver.UpdateDjEventArgs e)
        {
            var inside = JsonConvert.DeserializeObject<Room.InsindeInfo>(e.Json);

            SdjMainViewModel.SdjBottomBarViewModel.BottomBarNumberOfPeopleInRoom = inside.Clients.Count;
            SdjMainViewModel.SdjBottomBarViewModel.BottomBarSizeOfPlaylistInRoom = inside.Djs.Count;
            SdjMainViewModel.SdjBottomBarViewModel.BottomBarMaxSizeOfPlaylistInRoom = 30;
            SdjMainViewModel.SdjBottomBarViewModel.BottomBarNumberOfPeopleInRoom = inside.Clients.Count;
            SdjMainViewModel.SdjBottomBarViewModel.BottomBarNumberOfAdministrationInRoom =
                inside.Clients.Count(x => x.Rank > 0);
            SdjMainViewModel.SdjRoomViewModel.SongsQueue = (sbyte)inside.Djs.SelectMany(dj => dj.Video).Count();

            var client = new YoutubeClient();
            var tmp = client.GetVideoAsync(inside.Djs[0].Video[0].Id).Result;
            SdjMainViewModel.SdjRoomViewModel.SongTitle = tmp.Title;
            SdjMainViewModel.SdjBottomBarViewModel.BottomBarTitleOfActuallySong = tmp.Title;
        }*/

        public void RefreshInfo()
        {
            string reply = SdjMainViewModel.Client.Sender.AfterLogin();
            if (reply == null) return;

            var source = JsonConvert.DeserializeObject<List<Room>>(reply);
            var roomstmp = new ObservableCollection<RoomSquareModel>();
            for (int i = 0; i < source.Count; i++)
            {
                roomstmp.Add(new RoomSquareModel(SdjMainViewModel)
                {
                    HostName = source[i].Host,
                    RoomName = source[i].Name,
                    AdminsInRoom = source[i].AmountOfAdministration,
                    PeopleInRoom = source[i].AmountOfPeople,
                    RoomDescription = source[i].Description,
                    RoomId = source[i].Id,
                });
            }
            roomstmp = new ObservableCollection<RoomSquareModel>
                (roomstmp.OrderByDescending(i=>i.PeopleInRoom));
            SdjMainViewModel.RoomCollection = roomstmp;
        }

        private void Receiver_LoginErr(object sender, EventArgs e)
        {
            SdjMainViewModel.SdjLoginViewModel.ErrorNotify = ErrorMessages.LoginErrorMessage;
        }

        private void Receiver_RegisterAccExistErr(object sender, EventArgs e)
        {
            SdjMainViewModel.SdjRegisterViewModel.ErrorNotify = ErrorMessages.RegisterAccountExistsMessage;
        }

        private void Receiver_RegisterErr(object sender, EventArgs e)
        {
            SdjMainViewModel.SdjRegisterViewModel.ErrorNotify = ErrorMessages.RegisterErrorMessage;
        }

        private void Receiver_SuccesfulRegister(object sender, EventArgs e)
        {
            SdjMainViewModel.MainViewVisibility = MainView.Login;
            Debug.Log("Register", "Succesful register");
        }

        /*private void Receiver_SuccessfulLogin(object sender, ClientReceiver.SuccesfulLoginEventArgs e)
        {
            SdjMainViewModel.Profile = new UserClient() { Rank = e.Rank, Username = e.Username };
            UserState = UserState.Logged;
            
            Debug.Log("Login", "Succesful login");
        }*/

        private SdjMainViewModel _sdjMainViewModel;
        public SdjMainViewModel SdjMainViewModel
        {
            get => _sdjMainViewModel;
            set
            {
                if (_sdjMainViewModel == value) return;
                _sdjMainViewModel = value;
                OnPropertyChanged("SdjMainViewModel");
            }
        }
    }
}
