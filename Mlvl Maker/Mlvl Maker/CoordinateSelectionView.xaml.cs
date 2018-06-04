using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Mlvl_Maker
{
    /// <summary>
    /// Interaction logic for CoordinateSelectionView.xaml
    /// </summary>
    public partial class CoordinateSelectionView : Window
    {
        public CoordinateSelectionView()
        {
            InitializeComponent();            
            KeyUp += KeyReleased;
        }


        public static CatchCoordinate CatchCoordinate;        

        private void KeyReleased(object sender, KeyEventArgs e)
        {
            if(KeyInterop.VirtualKeyFromKey(e.Key) == 32 )
            {
                CatchCoordinate(MouseControl.GetCurrentCursorPosition());
            }
        }


    }
}
