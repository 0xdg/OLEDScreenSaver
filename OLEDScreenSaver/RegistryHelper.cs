using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using System.Windows.Forms;

namespace OLEDScreenSaver
{
    class RegistryHelper
    {
        const String DEFAULT_OLED_AWAITED_NAME = "LG TV SSCR";
        const String DEFAULT_TIMEOUT = "5";
        public static void InitValues()
        {
            if (!RegistryValuesSet())
            {
                SaveTimeout(DEFAULT_TIMEOUT);
                SaveScreenName(DEFAULT_OLED_AWAITED_NAME);
            }
        }

        public static Boolean RegistryValuesSet()
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\OLEDScreenSaver");
            if (key == null) { return false; }
            var value = key.GetValue("Timeout", "No Value");
            if (value.ToString() == "No Value") { return false; }
            value = key.GetValue("ScreenName", "No Value");
            if (value.ToString() == "No Value") { return false; }
            return true;
        }
        
        public static Boolean SaveTimeout(String timeout)
        {
            if (!int.TryParse(timeout, out _)) {
                string message = "The timeout value is not an integer.";
                string caption = "Error While Setting Timeout";
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                MessageBox.Show(message, caption, buttons);
            }
            RegistryKey key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\OLEDScreenSaver");
            key.SetValue("Timeout", timeout.ToString());
            key.Close();
            return true;
        }

        public static int LoadTimeout()
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\OLEDScreenSaver");
            int value = int.Parse(DEFAULT_TIMEOUT);
            if (key != null)
            {
                value = int.Parse(key.GetValue("Timeout").ToString());
                key.Close();
            }
            return value;
        }

        public static Boolean SaveScreenName(String new_name)
        {
            var screen = ScreenHelper.FindScreen(new_name);
            if (screen == null)
            {
                string message = "The screen name could not be found, are you sure you entered it correctly? Check the Windows display settings to get the proper name.";
                string caption = "Error While Getting Screen";
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                MessageBox.Show(message, caption, buttons);
            }
            else
            {
                RegistryKey key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\OLEDScreenSaver");
                key.SetValue("ScreenName", new_name);
                key.Close();
                return true;
            }
            return false;
        }

        public static String LoadScreenName()
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\OLEDScreenSaver");
            String value = DEFAULT_OLED_AWAITED_NAME;
            if (key != null)
            {
                value = key.GetValue("ScreenName").ToString();
                key.Close();
            }
            return value;
        }

        public static void SetStartup(Boolean enabled)
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey
                ("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

            if (enabled)
            {
                key.SetValue("OLEDScreenSaver", Application.ExecutablePath);
            }
            else
            {
                key.DeleteValue("OLEDScreenSaver", false);
            }
        }

        public static Boolean LoadStartup()
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey
                ("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            var value = key.GetValue("OLEDScreenSaver", "No Value").ToString();
            if (value == "No Value")
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
