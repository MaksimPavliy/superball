using FriendsGamesTools;
using FriendsGamesTools.ECSGame;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

namespace HC
{
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
    public interface IFixedUpdateble
    {
        void OnFixedUpdate(float fixedDeltaTIme);
    }

    public class HCRoot : GameRoot<HCRoot>
    {
        public HCLocationsController locations;
        public HCMoneyController money;
        public HCSkinsManager skin1Manager;
        public GameState State = GameState.INIT;

        protected override void Awake()
        {
            base.Awake();
            fixedControllers = new List<Controller>();
            foreach (var c in controllers)
            {
                if (c is IFixedUpdateble f) fixedControllers.Add(c);

            }
        }

        #region MainLogic

        GameState lastState;
        bool stateChanged => State != lastState;
        void SetState(GameState state) => State = state;

        public void StartLevel()
        {
            SetState(GameState.IN_GAME);
            inGameTimer = 0;
        }
        public void Win() => SetState(GameState.WIN);
        public void ShowItemProgress() => SetState(GameState.REWARD);
        public void Lose() => SetState(GameState.LOSE);
        public void RestartLevel()
        {
            HCLocationsView.instance.Clear();
            locations.RestartLocation();
            StartLevel();
        }
        public void OpenCustomization() => SetState(GameState.CUSTOMIZATION);
        public void GoToMain() => SetState(GameState.MAIN_MENU);
        public void Continue() => RestartLevel();
        public void NextLevel()
        {
            (HCLocationsView.instance).Clear();
            locations.ChangeLocation();
            GoToMain();
        }

        protected override void OnWorldInited()
        {
            base.OnWorldInited();
            SetState(GameState.MAIN_MENU);
        }

        float inGameTimer = -1;
        void Dummycore()
        {
            if (inGameTimer > 1.5f)
            {
                if (Utils.Random(0, 1f) > 0.5f)
                {
                    Win();
                }
                else
                {
                    Lose();
                }
            }
        }
        protected override void Update()
        {
            if (stateChanged)
            {
                lastState = State;
                switch (State)
                {
                    case GameState.INIT:
                        SetState(GameState.MAIN_MENU);
                        break;
                    case GameState.MAIN_MENU:
                        break;
                    case GameState.IN_GAME:
                        break;
                    case GameState.CUSTOMIZATION:
                        break;
                    case GameState.WIN:
                        break;
                    case GameState.LOSE:
                        break;
                }

            }

            switch (State)
            {
                case GameState.IN_GAME:
                    inGameTimer += Time.deltaTime;
                    Dummycore();
                    break;
            }

            base.Update();
        }
        #endregion

        #region FixedUpdateLogic
        List<Controller> fixedControllers;

        private void FixedUpdate()
        {
            var fixedDeltaTIme = Time.fixedDeltaTime;
            foreach (var c in fixedControllers)
            {
                ((IFixedUpdateble)c).OnFixedUpdate(fixedDeltaTIme);
            }
        }
        #endregion



    }
}