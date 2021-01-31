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
            showFormCallback();
            StopTimer();
        }

        public static void OnCloseScreensaver()
        {
            Console.WriteLine("Closing screensaver");
            hideFormCallback();
        }

        public void PauseScreensaver()
        {
            StopTimer();
        }

        public void ResumeScreensaver()
        {
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
