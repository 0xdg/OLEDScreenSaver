using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;
using System.ComponentModel;
using System.Messaging;
using System.IO;
using System.Runtime;

namespace OLEDScreenSaver
{
    class MouseInput
    {
        public event EventHandler<EventArgs> mouseMoved;

        private Win32Helper.HookDelegate mouseDelegate;
        private IntPtr mouseHandle;
        private const Int32 WH_MOUSE = 7;
        private const Int32 WH_MOUSE_LL = 14;
        public Action event_happened_callback;
        private Thread hookThread;

        public MouseInput()
        {
            mouseDelegate = MouseHookDelegate;
        }

        public void SetHook()
        {
            hookThread = new Thread(SetHookForThread);
            hookThread.IsBackground = true;
            hookThread.Start();
        }

        public void SetHookForThread()
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                mouseHandle = Win32Helper.SetWindowsHookEx(WH_MOUSE_LL, mouseDelegate, Win32Helper.GetModuleHandle(curModule.ModuleName), 0);
            }
            LogHelper.Log("Mouse Hook set");

            Message msg = new Message();
            while (!Win32Helper.GetMessage(ref msg, IntPtr.Zero, 0, 0))
            {
                Win32Helper.TranslateMessage(ref msg);
                Win32Helper.DispatchMessage(ref msg);
            }
        }

        public void RemoveHook()
        {
            Win32Helper.UnhookWindowsHookEx(mouseHandle);
            LogHelper.Log("Mouse Hook removed");
        }

        public void RegisterEventHappenedCallback(Action p_event_happened_callback)
        {
            event_happened_callback = p_event_happened_callback;
            LogHelper.Log("Registered MouseInput RegisterEventHappenedCallback");
        }

        private IntPtr MouseHookDelegate(
            Int32 Code, IntPtr wParam, IntPtr lParam)
        {
            if (Code < 0)
            {
                return Win32Helper.CallNextHookEx(
                    mouseHandle, Code, wParam, lParam);
            }
            else
            {
                if (mouseMoved != null)
                    mouseMoved(this, new EventArgs());

                event_happened_callback();

                return Win32Helper.CallNextHookEx(
                    mouseHandle, Code, wParam, lParam);
            }
        }
    }
}
