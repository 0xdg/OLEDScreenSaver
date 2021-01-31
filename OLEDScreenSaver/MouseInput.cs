using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLEDScreenSaver
{
    class MouseInput
    {
        public event EventHandler<EventArgs> KeyBoardKeyPressed;

        private Win32Helper.HookDelegate mouseDelegate;
        private IntPtr mouseHandle;
        private const Int32 WH_MOUSE_LL = 14;
        public Action event_happened_callback;

        public MouseInput()
        {
            mouseDelegate = MouseHookDelegate;
            SetHook();
        }

        public void SetHook()
        {
            mouseHandle = Win32Helper.SetWindowsHookEx(
                WH_MOUSE_LL, mouseDelegate, IntPtr.Zero, 0);
            LogHelper.Log("Mouse Hook set");
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

            if (KeyBoardKeyPressed != null)
                KeyBoardKeyPressed(this, new EventArgs());

            event_happened_callback();

            return Win32Helper.CallNextHookEx(
                mouseHandle, Code, wParam, lParam);
        }
    }
}
