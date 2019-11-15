using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SenderRoutingWin
{
    public class Configuration
    {
        public static string fileName = System.IO.Path.GetFileName(System.Windows.Forms.Application.ExecutablePath);
        public static bool addSetting(string key, string value)
        {
            try
            {
                System.Configuration.Configuration config = System.Configuration.ConfigurationManager.OpenExeConfiguration(fileName);
                config.AppSettings.Settings.Add(key, value);
                config.Save();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static string getSetting(string key)
        {
            try
            {
                System.Configuration.Configuration config = System.Configuration.ConfigurationManager.OpenExeConfiguration(fileName);
                string value = config.AppSettings.Settings[key].Value;
                return value;
            }
            catch
            {
                return string.Empty;
            }
        }
        public static bool updateSeeting(string key, string newValue)
        {
            try
            {
                System.Configuration.Configuration config = System.Configuration.ConfigurationManager.OpenExeConfiguration(fileName);
                string value = config.AppSettings.Settings[key].Value = newValue;
                config.Save();
                return true;
            }
            catch
            {
                return false;
            }            
        }
    }
}