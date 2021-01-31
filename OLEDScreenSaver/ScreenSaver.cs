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
        public ScreenSaver()
        {
            screenSaverTimer = new System.Timers.Timer(RegistryHelper.LoadTimeout() * 60 * 1000);
            screenSaverTimer.Elapsed += new System.Timers.ElapsedEventHandler(OnCreateScreensaver);
            screenSaverTimer.Enabled = true;
            LogHelper.Log("Timer launched");
        }    
        
        public void OnCreateScreensaver(object source, System.Timers.ElapsedEventArgs e)
        {
            LogHelper.Log("Creating screensaver");
            if (!paused)
            {
                showFormCallback();
                StopTimer();
            }
        }

        public static void OnCloseScreensaver()
        {
            Console.WriteLine("Closing screensaver");
            if (!paused)
            {
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
            StartTimer(false);
        }

        public void RegisterShowFormCallback(Action pShowFormCallback)
        {
            showFormCallback = pShowFormCallback;
        }

        public void RegisterHideFormCallback(Action pHideFormCallback)
        {
            hideFormCallback = pHideFormCallback;
        }

        public void StartTimer(Boolean refresh)
        {
            if (refresh)
            {
                screenSaverTimer.Interval = RegistryHelper.LoadTimeout() * 60 * 1000;
            }
            screenSaverTimer.Stop();
            screenSaverTimer.Start();
        }

        public void StopTimer()
        {
            screenSaverTimer.Stop();
        }

        public void InputEventCallback()
        {
            Console.WriteLine("InputEventCallback called");
            StartTimer(false);
            OnCloseScreensaver();
        }
    }
}
