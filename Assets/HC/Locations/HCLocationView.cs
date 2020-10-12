using FriendsGamesTools.ECSGame.Locations;

namespace HC
{
    public class HCLocationView: LocationView
    {
        public string LocationName = "";
        public double winMoney => 52;
        public override void OnLocationShown()
        {
            base.OnLocationShown();
    
            HCRoot.instance.locations.CallLocationShown();
            LocationName = LocationName == "" ? $"Level {HCRoot.instance.locations.currLocationInd+1}" : LocationName;
        }
    }
}