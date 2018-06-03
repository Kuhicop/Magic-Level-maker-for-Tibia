﻿using System;
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
        }


        private Enums.PotionType _selectedPotion;

        private int _potionKey;
        private int _firstSpellKey;
        private int _secondSpellKey;




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
