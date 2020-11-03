using FriendsGamesTools.ECSGame;

namespace HC
{
    public class HCRoot : GameRoot<HCRoot>
    {
        public HCLocationsController levels;
        public HCMoneyController money;
        public MoneySkinController moneySkin;
        public ProgressSkinController progressSkin;

        protected override void OnWorldInited()
        {
            base.OnWorldInited();
            MainMenuWindow.Show();
        }
    }
}