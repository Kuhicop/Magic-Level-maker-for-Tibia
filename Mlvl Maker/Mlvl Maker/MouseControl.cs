using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows;

namespace Mlvl_Maker
{

    public class MouseControl
    {


        [DllImport("user32.dll")]
        public static extern long SetCursorPos(int x, int y);

        [DllImport("user32.dll")]
        public static extern int mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, IntPtr dwExtraInfo);

        [DllImport("user32.dll")]
        public static extern bool GetCursorPos(out POINT lpPoint);

        
        /// <summary>
        /// Moving stack of potions/empty vials
        /// </summary>
        /// <param name="destination"> Destination coordinates </param>
        /// <param name="place"> Starting coordinates </param>
        public static void MoveStack(POINT destination, POINT place)
        {
            SetCursorPos(place.X, place.Y);
            Thread.Sleep(5);
            mouse_event(0x002, 0, 0, 0, (IntPtr)0);
            Thread.Sleep(15);
            SetCursorPos(place.X + 2, place.Y + 2);
            Thread.Sleep(5);
            SetCursorPos(destination.X, destination.Y);
            Thread.Sleep(15);
            mouse_event(0x004, 0, 0, 0, (IntPtr)0);
            Thread.Sleep(5);           

        }



    }
}




public struct POINT
{
    public int X;
    public int Y;

    public static implicit operator Point(POINT point)
    {
        return new Point(point.X, point.Y);
    }
}
