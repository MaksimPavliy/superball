using FriendsGamesTools;
using FriendsGamesTools.ECSGame;
using FriendsGamesTools.ECSGame.Player.Money;
using System;
using UnityEngine;
using UnityEngine.Events;

namespace Superball
{
    public class GameManager: MonoBehaviourHasInstance<GameManager>
    {
        [HideInInspector] public UnityEvent PlayPressed = new UnityEvent();
        [HideInInspector] public UnityEvent LevelComplete = new UnityEvent();
        [SerializeField] private Ball ball;
        [SerializeField] private LevelSelector selector;
        [SerializeField] private Pipe pipe;
        [SerializeField] private new Camera camera;
        private bool isPlaying;
        private int _score => ScoreManager.instance.Score;

        private int jumpCounter = 0;
        private int themeIndex = 0;
        public int CoinsAmount { private set; get; } = 0;
        public bool IsPlaying { get => isPlaying; set => isPlaying = value; }
        private SuperballGeneralConfig Config => SuperballGeneralConfig.instance;
        private bool ChangeSpline => Config.randomSpline;

        private void Start()
        {
            ball.JumpSucceded += OnJumpSucceded;
            float ratio = camera.aspect;
            float maxBounds = ratio * camera.orthographicSize;
            Debug.Log(maxBounds);
            pipe.maxOffset = maxBounds*1.2f;
        }
        private void OnJumpSucceded()
        {
            jumpCounter++;
            if(jumpCounter % Config.jumpsPerTheme == 0)
            {
                if (ChangeSpline)
                {
                    themeIndex++;
                    selector.OnThemeChanged(themeIndex);
                    pipe.OnThemeChanged(themeIndex);
                }
            }
            AddCoins();
        }
        public void OnPlay()
        {
            IsPlaying = true;
            PlayPressed?.Invoke();
        }

        private void AddCoins()
        {
            CoinsAmount = ball.jumpCounter % 5 == 0 ? CoinsAmount += 3 : CoinsAmount += 1;
          //  PlayerPrefs.SetInt("Coins", CoinsAmount);
        }
        public void OnLevelComplete()
        {
            GameRoot.instance.Get<SuperballLevelsController>().DoLose();
            GameRoot.instance.Get<HightScoreController>().TrySaveScore(_score);
            //FinishScore.instance.UpdateFinishScore();
            LevelComplete?.Invoke();
            IsPlaying = false;
        }

        public void OnReset()
        {
            ScoreManager.instance.ClearScore();
        }
    }
}