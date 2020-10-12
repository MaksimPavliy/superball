using FriendsGamesTools;
using FriendsGamesTools.ECSGame.Player.Money;
using FriendsGamesTools.Haptic;
using FriendsGamesTools.Share;
using FriendsGamesTools.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace HC
{   public class UIView : MonoBehaviourHasInstance<UIView>
    {
        public CoreGameUI coreGame;
        public MainMenuWindow mainMenu => Windows.Get<MainMenuWindow>();
        public WinLevelWindow winWindow => Windows.Get<WinLevelWindow>();
        public RewardProgressWindow rewardWindow => Windows.Get<RewardProgressWindow>();
        public LoseLevelWindow loseWindow => Windows.Get<LoseLevelWindow>();
        public CustomizationWindow skinsWindow => Windows.Get<CustomizationWindow>();
        GameState State => HCRoot.instance.State;
        GameState lastState = GameState.INIT;
        bool stateChanged => State != lastState;
     
        public void Share(string text) => ShareManager.Share(text);

        private void Update()
        {
            if (!stateChanged) return;
            lastState = State;
            switch (State)
            {
                case GameState.INIT:
            
                    break;
                case GameState.MAIN_MENU:
                    coreGame.Show(true);
                    mainMenu.Show();
                    PlayerMoneyView.instance.gameObject.SetActive(true);
                    coreGame.SetLevelName(HCLocationsView.instance.shownLocationView.LocationName);
                    break;
                case GameState.CUSTOMIZATION:
                    coreGame.Show(false);
                    skinsWindow.Show(true);
                    PlayerMoneyView.instance.gameObject.SetActive(true);
                    break;
                case GameState.IN_GAME:
                    coreGame.Show(true);
                    mainMenu.Show(false);
                    winWindow.Show(false);
                    loseWindow.Show(false);
                    rewardWindow.Show(false);
                    coreGame.SetLevelName(HCLocationsView.instance.shownLocationView.LocationName);
                    PlayerMoneyView.instance.gameObject.SetActive(true);
                    break;
                case GameState.REWARD:
                    rewardWindow.Show(true);
                    PlayerMoneyView.instance.gameObject.SetActive(true);
                    break;
                case GameState.WIN:
                    winWindow.Show(true);
                    break;
                case GameState.LOSE:
                    loseWindow.Show(true);
                    break;
            }

        }



        #region UITap Check
        [Space(10)]
        public GraphicRaycaster raycaster;
        public EventSystem eventSystem;
        PointerEventData eventData;
        public bool OverUITap()
        {
            if (!raycaster || !eventSystem) return false;
            eventData = new PointerEventData(eventSystem);
            eventData.position = Input.mousePosition;
            List<RaycastResult> results = new List<RaycastResult>();
            raycaster.Raycast(eventData, results);
            if (results.Count > 0) return true;
            return false;
        }
        #endregion
    }
}