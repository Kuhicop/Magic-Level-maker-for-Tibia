using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using System.Runtime.InteropServices;
using System.Windows;

namespace Mlvl_Maker
{
    public class WindowStatus
    {

        public WindowStatus()
        {
            _statusChecker = new DispatcherTimer();
            _statusChecker.Interval = new TimeSpan(0, 0, 0, 0, 35);
            _statusChecker.Tick += StatusCheckerTick;
            _statusChecker.Start();
        }


        DispatcherTimer _statusChecker;
        public static TopMostStatusChanged TopMostStatus;

        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);


        private bool CheckTibiaStatus()
        {
            const int _nChars = 256;
            StringBuilder _buff = new StringBuilder(_nChars);
            IntPtr _handle = GetForegroundWindow();

            if(GetWindowText(_handle, _buff, _nChars) > 0)
            {                
                if (_buff.ToString().Contains("Tibia -"))
                    return true;                
            }
            return false;

        }

        private void StatusCheckerTick(object sender, EventArgs e)
        {
            
            TopMostStatus(CheckTibiaStatus());
        }






    }
}
