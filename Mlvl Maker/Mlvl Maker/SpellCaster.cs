using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mlvl_Maker
{
    public class SpellCaster
    {


        public SpellCaster()
        {
            MagicLevelMakerViewModel.SelectPotion += SelectedPotion;
            MagicLevelMakerViewModel.SendAssignedKeyValue += AssignKeyValue;
            CoordinateSelectionViewModel.SendCoordinate += SetCoordinates;
        }


        private Enums.PotionType _selectedPotion;

        private int _potionKey;
        private int _firstSpellKey;
        private int _secondSpellKey;

        private POINT _placeInBackpack;
        private POINT _placeWithPotions;
        private POINT _placeToDrop;


        private void SetCoordinates(POINT point, Enums.Place selectedPlace)
        {
            switch(selectedPlace)
            {
                case Enums.Place.backpack:
                    _placeInBackpack = point;
                    break;

                case Enums.Place.potionStack:
                    _placeWithPotions = point;
                    break;

                case Enums.Place.vialStack:
                    _placeToDrop = point;
                    break;
            }
        }

        private void SelectedPotion(Enums.PotionType kindOfPotion)
        {
            _selectedPotion = kindOfPotion;            
        }



        private void AssignKeyValue(int keyValue, Enums.Hotkey key)
        {
            switch(key)
            {
                case Enums.Hotkey.potion:
                    _potionKey = keyValue;
                    break;

                case Enums.Hotkey.firstSpell:
                    _firstSpellKey = keyValue;
                    break;

                case Enums.Hotkey.secondSpell:
                    _secondSpellKey = keyValue;
                    break;
            }
        }





    }
}
