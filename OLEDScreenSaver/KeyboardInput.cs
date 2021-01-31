using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace OLEDScreenSaver
{
    public class KeyboardInput
    {
        public event EventHandler<EventArgs> KeyBoardKeyPressed;

        private Win32Helper.HookDelegate keyBoardDelegate;
        private IntPtr keyBoardHandle;
        private const Int32 WH_KEYBOARD_LL = 13;
        public Action event_happened_callback;

        public KeyboardInput()
        {
            keyBoardDelegate = KeyboardHookDelegate;
            //SetHook();
        }

        public void SetHook()
        {
            keyBoardHandle = Win32Helper.SetWindowsHookEx(
                WH_KEYBOARD_LL, keyBoardDelegate, IntPtr.Zero, 0);
            LogHelper.Log("Keyboard Hook set");
        }

        public void RemoveHook()
        {
            var res = Win32Helper.UnhookWindowsHookEx(keyBoardHandle);
            LogHelper.Log("Keyboard Hook removed");
        }

        public void RegisterEventHappenedCallback(Action p_event_happened_callback)
        {
            event_happened_callback = p_event_happened_callback;
            LogHelper.Log("Registered KeyboardInput RegisterEventHappenedCallback");
        }

        private IntPtr KeyboardHookDelegate(
            Int32 Code, IntPtr wParam, IntPtr lParam)
        {
            if (Code < 0)
            {
                return Win32Helper.CallNextHookEx(
                    keyBoardHandle, Code, wParam, lParam);
            }

            if (KeyBoardKeyPressed != null)
                KeyBoardKeyPressed(this, new EventArgs());

            event_happened_callback();

            return Win32Helper.CallNextHookEx(
                keyBoardHandle, Code, wParam, lParam);
        }
    }
}
