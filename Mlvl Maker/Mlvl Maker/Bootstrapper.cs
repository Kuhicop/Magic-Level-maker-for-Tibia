using System.Windows;
using Caliburn.Micro;


namespace Mlvl_Maker
{
    public class Bootstrapper : BootstrapperBase
    {

        public Bootstrapper()
        {
            Initialize();
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<MagicLevelMakerViewModel>();
        }

    }
}
