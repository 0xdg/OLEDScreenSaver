using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Management;

namespace OLEDScreenSaver
{
    public class ScreenSaver
    {
        private System.Timers.Timer screenSaverTimer;
        public static Action showFormCallback;
        public static Action hideFormCallback;
        public static bool paused = false;
        public static bool displayed = false;
        private uint thresholdTime = 0;
        private uint pollrate = 0;

        public ScreenSaver()
        {
            thresholdTime = (uint)RegistryHelper.LoadTimeout() * 1000 * 60;
            pollrate = (uint)RegistryHelper.LoadPollRate();
        }

        public void Launch()
        {
            if (screenSaverTimer != null)
            {
                screenSaverTimer.Stop();
                screenSaverTimer.Close();
            }
            screenSaverTimer = new System.Timers.Timer(pollrate);
            screenSaverTimer.Elapsed += new System.Timers.ElapsedEventHandler(Tick);
            screenSaverTimer.Enabled = true;
            LogHelper.Log("Timer launched");
        }

        public void Tick(object source, System.Timers.ElapsedEventArgs e)
        {
            uint time = GetLastInputTime();
            if (time > thresholdTime)
            {
                OnCreateScreensaver();
            }
            else
            {
                OnCloseScreensaver();
            }
        }

        static uint GetLastInputTime()
        {
            uint idleTime = 0;
            Win32Helper.LASTINPUTINFO lastInputInfo = new Win32Helper.LASTINPUTINFO();
            lastInputInfo.cbSize = (uint)Marshal.SizeOf(lastInputInfo);
            lastInputInfo.dwTime = 0;

            uint envTicks = (uint)Environment.TickCount;

            if (Win32Helper.GetLastInputInfo(ref lastInputInfo))
            {
                uint lastInputTick = lastInputInfo.dwTime;

                idleTime = envTicks - lastInputTick;
            }
            Console.WriteLine("idleTime " + idleTime);

            return idleTime;
        }

        public void OnCreateScreensaver()
        {
            if (!paused)
            {
                LogHelper.Log("Creating screensaver");
                displayed = true;
                showFormCallback();
            }
        }

        public static void OnCloseScreensaver()
        {
            if (!paused && displayed)
            {
                Console.WriteLine("Closing screensaver");
                displayed = false;
                hideFormCallback();
            }
        }

        public void PauseScreensaver()
        {
            paused = true;
            StopTimer();
        }

        public void ResumeScreensaver()
        {
            paused = false;
            StartTimer();
        }

        public void RegisterShowFormCallback(Action pShowFormCallback)
        {
            showFormCallback = pShowFormCallback;
        }

        public void RegisterHideFormCallback(Action pHideFormCallback)
        {
            hideFormCallback = pHideFormCallback;
        }

        public void UpdateTimeout()
        {
            thresholdTime = (uint)RegistryHelper.LoadTimeout() * 60 * 1000;
        }
        public void UpdatePollRate()
        {
            pollrate = (uint)RegistryHelper.LoadPollRate();
            Launch();
        }

        public void StartTimer()
        {
            screenSaverTimer.Stop();
            screenSaverTimer.Start();
        }

        public void StopTimer()
        {
            screenSaverTimer.Stop();
        }
    }
}
