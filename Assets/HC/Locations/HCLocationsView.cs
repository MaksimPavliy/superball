using FriendsGamesTools.ECSGame.Locations;

namespace HC
{
    public class HCLocationsView : LocationsView<HCLocationView, ChangeLocationsWindow>
    {
        public new void Reset()
        {
            base.Reset();
            Clear();
        }
        public void Clear()
        {
            if (shownLocationView) Destroy(shownLocationView.gameObject);
        }

        public string ShownLocationName => shownLocationView.LocationName;
    }


}