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

        public GameState State = GameState.INIT;
        GameState lastState;
        bool stateChanged => State != lastState;

        protected override void Update()
        {
            if (stateChanged)
            {
                lastState = State;
                switch (State)
                {
                    case GameState.INIT:
                        break;
                    case GameState.MAIN_MENU:
                        break;
                    case GameState.IN_GAME:
                        HCAnalyticsManager.LevelStart(levels.currLocationInd.ToString());
                        break;
                    case GameState.CUSTOMIZATION:
                        break;
                    case GameState.WIN:
                        HCAnalyticsManager.LevelFinish(levels.currLocationInd.ToString());
                        break;
                    case GameState.LOSE:
                        HCAnalyticsManager.LevelFailed(levels.currLocationInd.ToString());
                        break;
                }

            }
        }
    }

    public enum GameState
    {
        INIT,
        MAIN_MENU,
        IN_GAME,
        CUSTOMIZATION,
        WIN,
        REWARD,
        LOSE,
    }
}