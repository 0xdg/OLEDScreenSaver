using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OLEDScreenSaver
{
    public partial class ConfigForm : Form
    {
        private ScreenSaver screenSaver;
        public ConfigForm(ScreenSaver pScreenSaver)
        {
            InitializeComponent();
            this.Icon = Properties.Resources.Alecive_Flatwoken_Apps_Computer_Screensaver;
            startupCheckbox.Checked = RegistryHelper.LoadStartup();
            screenNameTextbox.Text = RegistryHelper.LoadScreenName();
            timeoutTextbox.Text = RegistryHelper.LoadTimeout().ToString();
            pollRateTextbox.Text = RegistryHelper.LoadPollRate().ToString();
            this.screenSaver = pScreenSaver;
        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
            Close();
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            //var screen = ScreenHelper.FindScreen(screenNameTextbox.Text);
            //if (screen == null)
            //{
            //    string message = "The screen name could not be found, are you sure you entered it correctly? Check the Windows display settings to get the proper name.";
            //    string caption = "Error While Setting ScreenName";
            //    MessageBoxButtons buttons = MessageBoxButtons.OK;
            //    MessageBox.Show(message, caption, buttons);
            //    this.Close();
            //    return;
            //}

            if (!int.TryParse(timeoutTextbox.Text, out _))
            {
                string message = "The timeout value is not an integer.";
                string caption = "Error While Setting Timeout";
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                MessageBox.Show(message, caption, buttons);
                this.Close();
                return;
            }

            if (!RegistryHelper.SaveScreenName(screenNameTextbox.Text) ||
                !RegistryHelper.SaveTimeout(timeoutTextbox.Text) ||
                !RegistryHelper.SavePollRate(pollRateTextbox.Text))
            {
                return;
            }
            RegistryHelper.SetStartup(startupCheckbox.Checked);
            this.DialogResult = DialogResult.Yes;
            Close();
        }
    }
}
