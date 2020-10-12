using FriendsGamesTools.ECSGame.Locations;

namespace HC
{
    public interface ILocationShown
    {
        void OnLocationShown();
    }
    public class HCLocationsController : LocationsController
    {
        public override int locationsCount => 5 ;

        protected override bool canChangePrivate => true;

        public void CallLocationShown()
        {
            foreach (var c in root.controllers)
            {
                if (c is ILocationShown l) l.OnLocationShown();
            }
        }
    }
}