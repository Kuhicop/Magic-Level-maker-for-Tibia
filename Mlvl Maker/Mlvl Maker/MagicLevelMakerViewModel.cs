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
    public delegate void SetCoordinate(POINT place, Enums.Place kindOfPlace);
    public delegate void CloseSupplyWindow();


    public class MagicLevelMakerViewModel : Screen
    {


        public MagicLevelMakerViewModel()
        {
            this.DisplayName = "Magic Level Trainer";
            switchTextStatus = "Turn ON";
            MagicLevelMakerView.SendKey += SetHotkey;
            SpellCaster.SendValue += UpdatePotions;
            CloseSupplyWindow += DoNothing;
            SpellCaster = new SpellCaster();
            _manager = new WindowManager();
            MagicalVocation();
            _botIsRunning = false;
            potionsInBackpack = 0.ToString();
            potionsOutside = 0.ToString();

            
        }

        public static CloseSupplyWindow CloseSupplyWindow;
        SpellCaster SpellCaster;
        WindowManager _manager;
        
        private Enums.Vocation _selectedVocation { get; set; }
        private Enums.Hotkey _selectedHotkey { get; set; }

        private bool _bindKeyAllow { get; set; }
        private bool _botIsRunning { get; set; }

        public static ChosedPotion SelectPotion;
        public static BindingKey SendAssignedKeyValue;
        public static ActualPotions SendAvailablePotions;
        public static BotStatusChanged BotStatus;


        #region Bindings

        private string _switchTextStatus;
        public string switchTextStatus
        {
            get { return _switchTextStatus; }
            private set
            {
                _switchTextStatus = value;
                NotifyOfPropertyChange("switchTextStatus");
            }
        }


        private string _potionsInBackpack;
        public string potionsInBackpack
        {
            get { return _potionsInBackpack; }
            set
            {                
                    _potionsInBackpack = value;
                    NotifyOfPropertyChange("potionsInBackpack");
                                
            }
        }

        private string _potionsOutside;
        public string potionsOutside
        {
            get { return _potionsOutside; }
            set
            {
                
                    _potionsOutside = value;
                    NotifyOfPropertyChange("potionsOutside");
                                
            }
        }



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


        protected override void OnDeactivate(bool close)
        {
            CloseSupplyWindow();
        }

        private void DoNothing()
        {

        }


        public void SwitchBot()
        {
            if(!_botIsRunning)
            {
                _botIsRunning = true;
                switchTextStatus = "Turn OFF";
                SendAvailablePotions(Enums.Place.backpack, Convert.ToInt32(potionsInBackpack));
                SendAvailablePotions(Enums.Place.potionStack, Convert.ToInt32(potionsOutside));
                BotStatus(_botIsRunning);
                _manager.ShowWindow(new SupplyViewModel());
            }

            else
            {
                _botIsRunning = false;
                switchTextStatus = "Turn ON";
                BotStatus(_botIsRunning);
                CloseSupplyWindow();
            }

        }



        /// <summary>
        /// Update potions count in app window while bot is running
        /// </summary>
        /// <param name="place"> Place of potions </param>
        /// <param name="potions"> Amount of potions </param>
        private void UpdatePotions(Enums.Place place, int potions)
        {
            string _potions = potions.ToString();
            switch(place)
            {
                case Enums.Place.backpack:
                    potionsInBackpack = _potions;
                    break;

                case Enums.Place.potionStack:
                    potionsOutside = _potions;
                    break;
            }

        }



        /// <summary>
        /// Setting text to button content & sending virtual key value to SpellCaster
        /// </summary>
        /// <param name="keyName"> Button content </param>
        /// <param name="keyValue"> Virtual key value </param>
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



        /// <summary>
        /// Selecting settings for mage
        /// </summary>
        public void MagicalVocation()
        {
            firstSpellName = "Utana Vid";
            secondSpellName = "Exura Vita";
            UMPIsAvailable = true;
            GMPIsAvailable = true;
            _selectedVocation = Enums.Vocation.Magical;
        }

        /// <summary>
        /// Selecting settings for paladin
        /// </summary>
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


        public void SetMouseCoordinates()
        {
            _manager.ShowWindow(new CoordinateSelectionViewModel());
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
