﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Communication.Shared
{
    public sealed class Commands
    {
        private static readonly Lazy<Commands> lazy =
            new Lazy<Commands>(() => new Commands());

        public static Commands Instance => lazy.Value;

        private Commands()
        {
        }

        public readonly Dictionary<string, string>
            CommandsDictionary = new Dictionary<string, string>()
            {
                {"Success", "0xF"},
                {"Error", "0xE"},

                {"Register", "0x1"},
                {"Login", "0x2"},
                {"Disconnect", "0x4"},
                {"GetPeoples", "0x5"},
                {"ChangePassword", "0x6"},
                {"ChangeUsername", "0x7"},
                {"ChangeLogin", "0x8"},
                {"ChangeRank", "0x9"},
                {"JoinRoom", "0xA"},
                {"CreateRoom", "0xB"},
                {"AfterLogin", "0xC"},
                {"JoinQueue", "0x10"},
                {"UpdateDj", "0x11"},
                {"SendMessage", "0xD"}
            };

        /// <param name="message">text from receiver</param>
        /// <returns>Returns command in index 0 and parameters in index 1+</returns>
        public List<string> GetMessageParameters(string message)
        {
            var list = new List<string>();
            message = message.Substring(message.IndexOf(' ') + 1);
            
            list.AddRange(message.Split('$'));
            return list;
        }

        public string GetMessageCommand(string message) =>
            message.Remove(message.IndexOf(' '));
    }
}