using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Jdsl.Models;
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace Jdsl.Helpers
{
    /// <summary>
    /// This is the Settings static class that can be used in your Core solution or in any
    /// of your client applications. All settings are laid out the same exact way with getters
    /// and setters. 
    /// </summary>
    public static class Settings
    {
        private static ISettings AppSettings
        {
            get { return CrossSettings.Current; }
        }

        #region Setting Constants

        private const string SettingsKey = "settings_key";
        private static readonly string SettingsDefault = string.Empty;

        #endregion


        public static string Username
        {
            get { return AppSettings.GetValueOrDefault("Username", ""); }
            set { AppSettings.AddOrUpdateValue("Username", value); }
        }

        public static string Password
        {
            get { return AppSettings.GetValueOrDefault("Password", ""); }
            set { AppSettings.AddOrUpdateValue("Password", value); }
        }

        public static string SecurityToken
        {
            get { return AppSettings.GetValueOrDefault("Token", ""); }
            set { AppSettings.AddOrUpdateValue("Token", value); }
        }

        public static string UserFullName
        {
            get { return AppSettings.GetValueOrDefault("UserFullName", ""); }
            set { AppSettings.AddOrUpdateValue("UserFullName", value); }
        }

        public static string UserRole
        {
            get { return AppSettings.GetValueOrDefault("UserRole", ""); }
            set { AppSettings.AddOrUpdateValue("UserRole", value); }
        }

        public static int UserId
        {
            get { return AppSettings.GetValueOrDefault("UserId", 0); }
            set { AppSettings.AddOrUpdateValue("UserId", value); }
        }

        public static string temp
        {
            get { return AppSettings.GetValueOrDefault("temp", ""); }
            set { AppSettings.AddOrUpdateValue("temp", value); }
        }

        public static ObservableCollection<Shop> ShopTempCollection {
            get; set; } = new ObservableCollection<Shop>();





    }

}