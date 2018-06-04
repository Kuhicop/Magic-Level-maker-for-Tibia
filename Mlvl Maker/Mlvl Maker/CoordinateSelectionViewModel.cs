using Caliburn.Micro;


namespace Mlvl_Maker
{
        
    public delegate void CatchCoordinate(POINT coordinate);

    public class CoordinateSelectionViewModel : Screen
    {

        public static SetCoordinate SendCoordinate;
        private Enums.Place _coordinatesOfPlace;

        public CoordinateSelectionViewModel()
        {
            _coordinatesOfPlace = Enums.Place.backpack;
            CoordinateSelectionView.CatchCoordinate += SendMouseCoordinate;
            coordinateSelectorText = "Set cursor above first place in your backpack and press spacebar";
        }


        private string _coordinateSelectorText;
        public string coordinateSelectorText
        {
            get { return _coordinateSelectorText; }
            private set
            {
                _coordinateSelectorText = value;
                NotifyOfPropertyChange("coordinateSelectorText");
            }
        }

        private void SendMouseCoordinate(POINT point)
        {
            SendCoordinate(point, _coordinatesOfPlace);           
            
            switch(_coordinatesOfPlace)
            {
                case Enums.Place.backpack:
                    _coordinatesOfPlace = Enums.Place.potionStack;
                    coordinateSelectorText = "Set cursor above your potions stack and press spacebar";
                    break;

                case Enums.Place.potionStack:
                    _coordinatesOfPlace = Enums.Place.vialStack;
                    coordinateSelectorText = "Set cursor above your place to throw empty vials and press spacebar";
                    break;

                case Enums.Place.vialStack:
                    this.TryClose();
                    break;
            }
        }



    }
}
