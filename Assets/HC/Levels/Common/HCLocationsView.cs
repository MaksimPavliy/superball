using FriendsGamesTools.ECSGame.Locations;

namespace HC
{
    public class HCLocationsView : LocationsView<HCLocationView, ChangeLocationsWindow> {
        public static new HCLocationsView instance => (HCLocationsView)LocationsView.instance;
        public string ShownLocationName => shownLocationView.locationName;
        HCRoot root => HCRoot.instance;
        public override void ShowChangeLocationWindow()
        {
            if (root.levels.state == Level.State.lose)
                LoseLevelWindow.Show();
            else
                WinLevelWindow.Show();
        }
    }
}