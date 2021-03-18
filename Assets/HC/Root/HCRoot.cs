using FriendsGamesTools.ECSGame;
using FriendsGamesTools.PushNotifications;
using System;

namespace HC
{
    public class HCRoot : GameRoot<HCRoot>
    {
        public HCLevelsController levels;
        public HCMoneyController money;
        public MoneySkinController moneySkin;
        public ProgressSkinController progressSkin;

        protected override void OnWorldInited()
        {
            base.OnWorldInited();

            MainMenuWindow.Show();
        }

        protected override void Start()
        {
            base.Start();

            SetPushNotification();
        }

        private void SetPushNotification()
        {
            PushNotificationsManager.AddNotificationConfig(
                "after_game_time",
               () => true,
               () => "",
               () => "We miss you! :(",
               () => DateTime.Now.AddDays(1)
                );
        }
    }
}