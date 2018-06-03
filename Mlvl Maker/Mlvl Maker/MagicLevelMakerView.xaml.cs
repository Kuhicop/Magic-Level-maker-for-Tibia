using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    /// Interaction logic for MagicLevelMakerView.xaml
    /// </summary>
    public partial class MagicLevelMakerView : Window
    {
        public MagicLevelMakerView()
        {
            InitializeComponent();
            KeyDown += KeyPress;
        }

        public static BindingKeyName SendKey;

        private void KeyPress(object sender, KeyEventArgs e)
        {
            string _name = e.Key.ToString();
            int _virtualKeyValue = KeyInterop.VirtualKeyFromKey(e.Key);
            SendKey(_name, _virtualKeyValue);
        }


    }
}
