using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Mlvl_Maker
{


    public delegate void BotStatusChanged(bool state);
    public delegate void TopMostStatusChanged(bool state);
    public delegate void ActualPotions(Enums.Place place, int potions);

    public class SpellCaster
    {


        public SpellCaster()
        {
            MagicLevelMakerViewModel.SelectPotion += SelectedPotion;
            MagicLevelMakerViewModel.SendAssignedKeyValue += AssignKeyValue;
            CoordinateSelectionViewModel.SendCoordinate += SetCoordinates;
            MagicLevelMakerViewModel.SendAvailablePotions += SetAvailablePotions;
            MagicLevelMakerViewModel.BotStatus += ToggleBot;            
            WindowStatus.TopMostStatus += IsGameTopMost;
            MagicLevelMakerView.ClosingApp += ToggleBot;
            WindowStatus = new WindowStatus();
            loop = new Thread(MakeBurningLoop);

        }

        Thread loopThread = null;
        Thread loop;

        WindowStatus WindowStatus;

        public static ActualPotions SendValue;

        private Enums.PotionType _selectedPotion { get; set; }
        private Enums.Vocation _selectedVocation { get; set; }

        private int _potionKey { get; set; }
        private int _firstSpellKey { get; set; }
        private int _secondSpellKey { get; set; }

        private POINT _placeInBackpack { get; set; }
        private POINT _placeWithPotions { get; set; }
        private POINT _placeToDrop { get; set; }

        private int _potionsInBackpack { get; set; }
        private int _potionsOutside { get; set; }

        private bool _IsEnabled { get; set; }
        private bool _IsGameTopMost { get; set; }
        private bool _IsLoopActivated { get; set; }



        private void SetAvailablePotions(Enums.Place place, int potions)
        {
            switch(place)
            {
                case Enums.Place.backpack:
                    _potionsInBackpack = potions;
                    break;

                case Enums.Place.potionStack:
                    _potionsOutside = potions;
                    break;
            }
        }


        private void ToggleBot(bool state)
        {            
            if (state)
            {
                _IsEnabled = true;
                loop = new Thread(MakeBurningLoop);
                loop.Start();                             
            }
            else
            {
                _IsEnabled = false;
                loop.Abort();
                        
                 
            }
        }

        
        private void IsGameTopMost(bool state)
        {
            //if (!state && _IsLoopActivated)
                //thread.Abort();
               // loopThread.Suspend();
            //if (state && _IsLoopActivated && !_IsGameTopMost)
                //loopThread.Resume();
                
            _IsGameTopMost = state;
        }


        /// <summary>
        /// Check if you have enought available potions for make loop.
        /// If potions are not in Backpack but potions are still on "ground", then they will be picked.
        /// </summary>
        /// <returns></returns>
        private bool PotionsCheck()
        {
            if (_potionsInBackpack >= 3)
                return true;
            else
            {
                if (_selectedPotion == Enums.PotionType.UMP && _potionsInBackpack >= 1)
                    return true;

                else if (_selectedPotion == Enums.PotionType.GMP && _potionsInBackpack >= 2)
                    return true;

                else if (_selectedVocation == Enums.Vocation.Paladin && _potionsInBackpack >= 2)
                    return true;

                else
                {
                    if (_potionsOutside >= 1)
                    {
                        TakeNewStack();
                        _potionsOutside--;
                        _potionsInBackpack += 100;
                        SendValue(Enums.Place.backpack, _potionsInBackpack);
                        SendValue(Enums.Place.potionStack, _potionsOutside);
                        return true;
                    }
                    else
                        return false;
                    
                }
            }            
        }

        /// <summary>
        /// Drop empty vials and pick new stack
        /// </summary>
        private void TakeNewStack()
        {
            MouseControl.MoveStack(_placeToDrop, _placeInBackpack);
            MouseControl.MoveStack(_placeInBackpack, _placeWithPotions);
        }

       

        private void MakeBurningLoop()
        {
            while(_IsEnabled && PotionsCheck())
            {
                Thread.Sleep(5);
                _IsLoopActivated = false;

                

                if(_IsGameTopMost)
                {
                    
                    //Task task = Task.Run(() => 
                    //{
                      //  loopThread = Thread.CurrentThread;
                                          
                                                
                        if (_selectedVocation == Enums.Vocation.Magical)
                        {
                            switch (_selectedPotion)
                            {
                                case Enums.PotionType.UMP:
                                    KeyboardControl.PressKey(_potionKey);
                                    _potionsInBackpack--;
                                    SendValue(Enums.Place.backpack, _potionsInBackpack);
                                    Thread.Sleep(Randomization.GenerateWait());
                                    KeyboardControl.PressKey(_firstSpellKey);
                                    Thread.Sleep(Randomization.GenerateWait());
                                    break;

                                case Enums.PotionType.GMP:
                                    KeyboardControl.PressKey(_potionKey);
                                    _potionsInBackpack--;
                                    SendValue(Enums.Place.backpack, _potionsInBackpack);
                                    Thread.Sleep(Randomization.GenerateWait());
                                    KeyboardControl.PressKey(_potionKey);
                                    _potionsInBackpack--;
                                    SendValue(Enums.Place.backpack, _potionsInBackpack);
                                    KeyboardControl.PressKey(_firstSpellKey);
                                    Thread.Sleep(Randomization.GenerateWait());
                                    break;

                                case Enums.PotionType.SMP:
                                    KeyboardControl.PressKey(_potionKey);
                                    _potionsInBackpack--;
                                    SendValue(Enums.Place.backpack, _potionsInBackpack);
                                    Thread.Sleep(Randomization.GenerateWait());
                                    KeyboardControl.PressKey(_potionKey);
                                    _potionsInBackpack--;
                                    SendValue(Enums.Place.backpack, _potionsInBackpack);
                                    Thread.Sleep(Randomization.GenerateWait());
                                    KeyboardControl.PressKey(_potionKey);
                                    _potionsInBackpack--;
                                    SendValue(Enums.Place.backpack, _potionsInBackpack);
                                    KeyboardControl.PressKey(_firstSpellKey);
                                    Thread.Sleep(Randomization.GenerateWait());
                                    break;
                            }
                        }

                        else
                        {
                            KeyboardControl.PressKey(_potionKey);
                            _potionsInBackpack--;
                            SendValue(Enums.Place.backpack, _potionsInBackpack);
                            Thread.Sleep(Randomization.GenerateWait());
                            KeyboardControl.PressKey(_potionKey);
                            _potionsInBackpack--;
                            SendValue(Enums.Place.backpack, _potionsInBackpack);
                            KeyboardControl.PressKey(_firstSpellKey);
                            KeyboardControl.PressKey(_secondSpellKey);
                            Thread.Sleep(Randomization.GenerateWait());
                        }
                   
                    
                }
                
            }
               
        }

        

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
