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


        /// <summary>
        /// Setting user defined coordinates on screen to be able drag&drop stacks
        /// </summary>
        /// <param name="point"> Point on screen </param>
        /// <param name="selectedPlace"> Kind of selected place </param>
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

        /// <summary>
        /// Set user defined kind of potion for engine work
        /// </summary>
        /// <param name="kindOfPotion"> Selected potion type </param>
        private void SelectedPotion(Enums.PotionType kindOfPotion)
        {
            _selectedPotion = kindOfPotion;            
        }


        /// <summary>
        /// Setting Virtual keys to use in engine for cast spells and drink potions
        /// </summary>
        /// <param name="keyValue"> Virtual key value </param>
        /// <param name="key"> Kind of key in app </param>
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
