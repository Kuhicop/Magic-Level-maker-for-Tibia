using System;
using Caliburn.Micro;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mlvl_Maker
{

    public delegate void BindingKeyName(string keyName, int keyValue);
    public delegate void BindingKey(int keyValue, Enums.Hotkey key);
    public delegate void ChosedPotion(Enums.PotionType kindOfPotion);


    public class MagicLevelMakerViewModel : Screen
    {


        public MagicLevelMakerViewModel()
        {
            this.DisplayName = "Magic Level Maker";
            MagicLevelMakerView.SendKey += SetHotkey;
            SpellCaster = new SpellCaster();
            MagicalVocation();           
            
        }


        SpellCaster SpellCaster;
        
        private Enums.Vocation _selectedVocation { get; set; }
        private Enums.Hotkey _selectedHotkey { get; set; }

        private bool _bindKeyAllow { get; set; }

        public static ChosedPotion SelectPotion;
        public static BindingKey SendAssignedKeyValue;


        #region Bindings

        private string _firstSpellName;
        public string firstSpellName
        {
            get { return _firstSpellName; }
            private set
            {
                _firstSpellName = value;
                NotifyOfPropertyChange("firstSpellName");
            }
        }

        private string _secondSpellName;
        public string secondSpellName
        {
            get { return _secondSpellName; }
            private set
            {
                _secondSpellName = value;
                NotifyOfPropertyChange("secondSpellName");
            }
        }

        private bool _UMPIsAvailable;
        public bool UMPIsAvailable
        {
            get { return _UMPIsAvailable; }
            private set
            {
                _UMPIsAvailable = value;
                NotifyOfPropertyChange("UMPIsAvailable");
            }
        }

        private bool _GMPIsAvailable;
        public bool GMPIsAvailable
        {
            get { return _GMPIsAvailable; }
            private set
            {
                _GMPIsAvailable = value;
                NotifyOfPropertyChange("GMPIsAvailable");
            }
        }

        private bool _SMPIsChecked;
        public bool SMPIsChecked
        {
            get { return _SMPIsChecked; }
            private set
            {
                _SMPIsChecked = value;
                NotifyOfPropertyChange("SMPIsChecked");
            }
        }

        private string _potionHotkeyName;
        public string potionHotkeyName
        {
            get { return _potionHotkeyName; }
            private set
            {
                _potionHotkeyName = value;
                NotifyOfPropertyChange("potionHotkeyName");
            }
        }

        private string _firstSpellHotkeyName;
        public string firstSpellHotkeyName
        {
            get { return _firstSpellHotkeyName; }
            private set
            {
                _firstSpellHotkeyName = value;
                NotifyOfPropertyChange("firstSpellHotkeyName");
            }
        }

        private string _secondSpellHotkeyName;
        public string secondSpellHotkeyName
        {
            get { return _secondSpellHotkeyName; }
            private set
            {
                _secondSpellHotkeyName = value;
                NotifyOfPropertyChange("secondSpellHotkeyName");
            }
        }

        private bool _potionHotkeyIsChecked;
        public bool potionHotkeyIsChecked
        {
            get { return _potionHotkeyIsChecked; }
            set
            {
                _potionHotkeyIsChecked = value;
                NotifyOfPropertyChange("potionHotkeyIsChecked");
                if (value)
                {
                    firstSpellHotkeyIsChecked = false;
                    secondSpellHotkeyIsChecked = false;
                    _selectedHotkey = Enums.Hotkey.potion;
                }
            }
        }


        private bool _firstSpellHotkeyIsChecked;
        public bool firstSpellHotkeyIsChecked
        {
            get { return _firstSpellHotkeyIsChecked; }
            set
            {
                _firstSpellHotkeyIsChecked = value;
                NotifyOfPropertyChange("firstSpellHotkeyIsChecked");
                if (value)
                {
                    potionHotkeyIsChecked = false;
                    secondSpellHotkeyIsChecked = false;
                    _selectedHotkey = Enums.Hotkey.firstSpell;
                }
            }
        }

        private bool _secondSpellHotkeyIsChecked;
        public bool secondSpellHotkeyIsChecked
        {
            get { return _secondSpellHotkeyIsChecked; }
            set
            {
                _secondSpellHotkeyIsChecked = value;
                NotifyOfPropertyChange("secondSpellHotkeyIsChecked");
                if (value)
                {
                    potionHotkeyIsChecked = false;
                    firstSpellHotkeyIsChecked = false;
                    _selectedHotkey = Enums.Hotkey.secondSpell;
                }
            }
        }

        #endregion
        


        private void SetHotkey(string keyName, int keyValue)
        {
            if(_bindKeyAllow)
            {
                switch(_selectedHotkey)
                {
                    case Enums.Hotkey.potion:
                        potionHotkeyName = keyName;
                        SendAssignedKeyValue(keyValue, Enums.Hotkey.potion);
                        potionHotkeyIsChecked = false;
                        break;

                    case Enums.Hotkey.firstSpell:
                        firstSpellHotkeyName = keyName;
                        SendAssignedKeyValue(keyValue, Enums.Hotkey.firstSpell);
                        firstSpellHotkeyIsChecked = false;
                        break;

                    case Enums.Hotkey.secondSpell:
                        secondSpellHotkeyName = keyName;
                        SendAssignedKeyValue(keyValue, Enums.Hotkey.secondSpell);
                        secondSpellHotkeyIsChecked = false;
                        break;
                }
                _bindKeyAllow = false;
            }
        }




        public void MagicalVocation()
        {
            firstSpellName = "Utana Vid";
            secondSpellName = "Exura Vita";
            UMPIsAvailable = true;
            GMPIsAvailable = true;
            _selectedVocation = Enums.Vocation.Magical;
        }

        public void PaladinVocation()
        {
            firstSpellName = "Exura Gran San";
            secondSpellName = "Utani Hur";
            UMPIsAvailable = false;
            GMPIsAvailable = false;
            SMPIsChecked = true;
            SelectedSMP();
            _selectedVocation = Enums.Vocation.Paladin;
        }

        /// <summary>
        /// Reaction for click radiobutton "Ultimate Mana Potion"
        /// </summary>
        public void SelectedUMP()
        {
            SelectPotion(Enums.PotionType.UMP);
        }

        /// <summary>
        /// Reaction for click radiobutton "Great Mana Potion"
        /// </summary>
        public void SelectedGMP()
        {
            SelectPotion(Enums.PotionType.GMP);
        }

        /// <summary>
        /// Reaction for click radiobutton "Strong Mana Potion"
        /// </summary>
        public void SelectedSMP()
        {
            SelectPotion(Enums.PotionType.SMP);
        }

        /// <summary>
        /// Pressed PotionHotkey for select bind key
        /// </summary>
        public void PotionHotkey()
        {
            if (potionHotkeyIsChecked)
            {
                _selectedHotkey = Enums.Hotkey.potion;
                _bindKeyAllow = true;
            }
            else
                _bindKeyAllow = false;
        }

        /// <summary>
        /// Pressed FirstSpellHotkey for select bind key
        /// </summary>
        public void FirstSpellHotkey()
        {
            if (firstSpellHotkeyIsChecked)
            {
                _selectedHotkey = Enums.Hotkey.firstSpell;
                _bindKeyAllow = true;
            }
            else
                _bindKeyAllow = false;
            
        }

        /// <summary>
        /// Pressed SecondSpellHotkey for select bind key
        /// </summary>
        public void SecondSpellHotkey()
        {
            if (secondSpellHotkeyIsChecked)
            {
                _selectedHotkey = Enums.Hotkey.secondSpell;
                _bindKeyAllow = true;
            }
            else
                _bindKeyAllow = false;
        }




    }
}
