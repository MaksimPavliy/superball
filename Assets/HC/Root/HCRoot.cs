using FriendsGamesTools.ECSGame;

namespace HC
{
    public class HCRoot : GameRoot<HCRoot>
    {
        public HCLocationsController levels;
        public HCMoneyController money;
        public MoneySkinController moneySkinManager;
        public ProgressSkinController progressSkinManager;

        protected override void OnWorldInited()
        {
            base.OnWorldInited();
            MainMenuWindow.Show();
        }
    }
}