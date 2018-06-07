using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace Mlvl_Maker
{
    public static class KeyboardControl
    {

        [DllImport("user32.dll")]
        public static extern void keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);

        /// <summary>
        /// Virtual press key
        /// </summary>
        /// <param name="keyValue"> Virtual key state </param>
        public static void PressKey(int keyValue)
        {
            byte _key = Convert.ToByte(keyValue);
            keybd_event(_key, 0, 0, 0);
            Thread.Sleep(Randomization.GenerateKeyDelay());
            keybd_event(_key, 0, 0x0002, 0);
            Thread.Sleep(Randomization.GenerateKeyDelay());
        }

    }
}
