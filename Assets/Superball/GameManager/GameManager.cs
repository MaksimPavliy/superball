using FriendsGamesTools;
using FriendsGamesTools.ECSGame;
using UnityEngine;
using UnityEngine.Events;

namespace Superball
{
    public class GameManager: MonoBehaviourHasInstance<GameManager>
    {
        [HideInInspector] public UnityEvent PlayPressed = new UnityEvent();
        [HideInInspector] public UnityEvent Won = new UnityEvent();
        [HideInInspector] public UnityEvent Lose = new UnityEvent();
        private bool isPlaying;

        public bool IsPlaying { get => isPlaying; set => isPlaying = value; }

        public void OnPlay()
        {
            IsPlaying = true;
            PlayPressed?.Invoke();
        }

        public void DoWin()
        {
            GameRoot.instance.Get<SuperballLevelsController>().DoWin();
            Won?.Invoke();
            IsPlaying = false;
        }

        public void DoLose()
        {
            GameRoot.instance.Get<SuperballLevelsController>().DoLose();
            Lose?.Invoke();
            IsPlaying = false;
        }
    }
}