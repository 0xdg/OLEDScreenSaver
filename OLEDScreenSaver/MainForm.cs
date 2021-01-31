using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Management;
using Microsoft.WindowsAPICodePack.ApplicationServices;
using System.Windows.Interop;

namespace OLEDScreenSaver
{
    public partial class MainForm : Form
    {
        private ContextMenu contextMenu1;
        private MenuItem menuItem1;
        private MenuItem menuItem2;
        private MenuItem menuItem3;
        private ScreenSaver screenSaver;
        private KeyboardInput keyboardInput;
        private MouseInput mouseInput;

        void MonitorOnChanged(object sender, EventArgs e)
        {
            LogHelper.Log(string.Format("Monitor status changed (new status: {0})", PowerManager.IsMonitorOn ? "On" : "Off"));
        }

        //private HwndSource _HwndSource;
        //private readonly IntPtr _ScreenStateNotify;


//        private IntPtr HwndHook(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
//        {
//            handler of console display state system event
//            if (msg == Win32Helper.WM_POWERBROADCAST)
//            {
//                if (wParam.ToInt32() == Win32Helper.PBT_POWERSETTINGCHANGE)
//                {
//                    var s = (Win32Helper.POWERBROADCAST_SETTING)Marshal.PtrToStructure(lParam, typeof(Win32Helper.POWERBROADCAST_SETTING));
//                    if (s.PowerSetting == Win32Helper.GUID_CONSOLE_DISPLAY_STATE)
//                    {
//                        LogHelper.Log("Event on screen happened!");
//                    }
//}
//            }

//            return IntPtr.Zero;
//        }

//        ~MainForm()
//        {
//            unregister for console display state system event 

//           _HwndSource.RemoveHook(HwndHook);
//            Win32Helper.UnregisterPowerSettingNotification(_ScreenStateNotify);
//        }

        public MainForm()
        {
            InitializeComponent();
            RegistryHelper.InitValues();


            // PowerManager.IsMonitorOnChanged += new EventHandler(MonitorOnChanged);
            //// . . .

            //// VM = DataContext as MainViewModel;

            //// register for console display state system event 
            //var hwnd = this.Handle;

            //_ScreenStateNotify = Win32Helper.RegisterPowerSettingNotification(hwnd, ref Win32Helper.GUID_CONSOLE_DISPLAY_STATE, Win32Helper.DEVICE_NOTIFY_WINDOW_HANDLE);
            //_HwndSource = HwndSource.FromHwnd(hwnd);
            //_HwndSource.AddHook(HwndHook);

            this.WindowState = FormWindowState.Minimized;
            this.ShowInTaskbar = false;
            this.notifyIcon1.Icon = SystemIcons.Application;
            this.contextMenu1 = new System.Windows.Forms.ContextMenu();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();

            this.contextMenu1.MenuItems.AddRange(
                        new System.Windows.Forms.MenuItem[] { this.menuItem1, this.menuItem2, this.menuItem3 });

            this.menuItem1.Index = 0;
            this.menuItem1.Text = "Pause";
            this.menuItem1.Click += new System.EventHandler(this.menuItem1_Click);
            this.menuItem1.Checked = false;

            this.menuItem2.Index = 1;
            this.menuItem2.Text = "Config";
            this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);

            this.menuItem3.Index = 2;
            this.menuItem3.Text = "Exit";
            this.menuItem3.Click += new System.EventHandler(this.menuItem3_Click);
            this.notifyIcon1.ContextMenu = this.contextMenu1;

            screenSaver = new ScreenSaver();
            screenSaver.RegisterHideFormCallback(HideFormCallback);
            screenSaver.RegisterShowFormCallback(ShowFormCallback);
            keyboardInput = new KeyboardInput();
            keyboardInput.RegisterEventHappenedCallback(screenSaver.InputEventCallback);
            mouseInput = new MouseInput();
            mouseInput.RegisterEventHappenedCallback(screenSaver.InputEventCallback);
            Hide();
        }


        private Boolean SetFormToOLEDScreen()
        {
            //LogHelper.Log("Custom getNumberOfConnectedMonitors got " + ScreenHelper.getNumberOfConnectedMonitors());
            var OLED_screen = ScreenHelper.FindScreen(RegistryHelper.LoadScreenName());
            if (OLED_screen != null)
            {
                Point p = new Point();
                p.X = Screen.PrimaryScreen.Bounds.Location.X;
                p.Y = Screen.PrimaryScreen.Bounds.Location.Y;
                this.Location = p;
                //LogHelper.Log("Found screen and setting pos " + p.X + " " + p.Y);
                //LogHelper.Log("OLED_screen.Bounds.Width " + OLED_screen.Bounds.Width + " OLED_screen.Bounds.Height " + OLED_screen.Bounds.Height + " OLED_screen.Bounds.Size " + OLED_screen.Bounds.Size);
                //LogHelper.Log("OLED_screen.Bounds.X " + OLED_screen.Bounds.X + " OLED_screen.Bounds.Y " + OLED_screen.Bounds.Y);
                return true;
            }
            LogHelper.Log("Screen not found");
            return false;
        }

        public void HideFormCallback()
        {
            Console.WriteLine("HideFormCallback");
            Invoke(new Action(() => {
                Hide();
                this.WindowState = FormWindowState.Minimized;
                Cursor.Show();
                Win32Helper.ShowCursor();
            }));
        }

        public void ShowFormCallback()
        {
            Console.WriteLine("ShowFormCallback");
            Invoke(new Action(() => {
                if (SetFormToOLEDScreen())
                {
                    Show();
                    this.WindowState = FormWindowState.Maximized;
                    Cursor.Hide();
                    this.BringToFront();
                    Win32Helper.HideCursor();
                }
            }));
        }

        private void NotifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            //if the form is minimized  
            //hide it from the task bar  
            //and show the system tray icon (represented by the NotifyIcon control)  
            if (this.WindowState == FormWindowState.Minimized)
            {
                Hide();
            }
        }

        private void menuItem1_Click(object Sender, EventArgs e)
        {
            if (this.menuItem1.Checked == false)
            {
                screenSaver.PauseScreensaver();
                keyboardInput.RemoveHook();
                mouseInput.RemoveHook();
                this.menuItem1.Checked = true;
                Console.WriteLine("Paused.");
            }
            else
            {
                screenSaver.ResumeScreensaver();
                keyboardInput.SetHook();
                mouseInput.SetHook();
                this.menuItem1.Checked = false;
                Console.WriteLine("Resumed.");
            }
        }

        private void menuItem2_Click(object Sender, EventArgs e)
        {
            ConfigForm config = new ConfigForm(screenSaver);
            var dialogResult = config.ShowDialog();
            if (dialogResult == DialogResult.Yes)
            {
                screenSaver.StartTimer(true);
            }
        }

        private void menuItem3_Click(object Sender, EventArgs e)
        {
            this.Close();
        }

    }
}
