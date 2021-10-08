using FriendsGamesTools;
using FriendsGamesTools.ECSGame;
using UnityEngine;
using UnityEngine.Events;

namespace Superball
{
    public class GameManager: MonoBehaviourHasInstance<GameManager>
    {
        [HideInInspector] public UnityEvent PlayPressed = new UnityEvent();
        [HideInInspector] public UnityEvent LevelComplete = new UnityEvent();
        private bool isPlaying;
        private int _score => ScoreManager.instance.Score;

        public bool IsPlaying { get => isPlaying; set => isPlaying = value; }

        public void OnPlay()
        {
            IsPlaying = true;
            PlayPressed?.Invoke();
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