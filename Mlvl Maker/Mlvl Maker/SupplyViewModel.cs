using System;
using Caliburn.Micro;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mlvl_Maker



{
    public class SupplyViewModel : Screen
    {

        public SupplyViewModel()
        {
            SpellCaster.SendValue += UpdateData;
            MagicLevelMakerViewModel.CloseSupplyWindow += CloseWindow;
        }


        private void CloseWindow()
        {
            this.TryClose();
        }


        private void UpdateData(Enums.Place place, int value)
        {
            string _value = value.ToString();
            switch(place)
            {
                case Enums.Place.backpack:
                    _potionsInBackpack = _value;
                    break;

                case Enums.Place.potionStack:
                    _potionsOutside = _value;
                    break;
            }
        }


        private string _potionsInBackpack;
        public string potionsInBackpack
        {
            get { return _potionsInBackpack; }
            private set
            {
                _potionsInBackpack = value;
                NotifyOfPropertyChange("potionsInBackpack");
            }
        }

        private string _potionsOutside;
        public string potionsOutside
        {
            get { return _potionsOutside; }
            private set
            {
                _potionsOutside = value;
                NotifyOfPropertyChange("potionsOutside");
            }
        }




    }
}
