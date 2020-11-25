using FriendsGamesTools.ECSGame;
using FriendsGamesTools.PushNotifications;
using System;

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

        protected override void Awake()
        {
            base.Awake();

            var title = "";
            var desc = "We miss you! :(";
            var when = DateTime.Now.AddDays(1);

            PushNotificationsManager.Send(title, desc, when);
        }
    }
}