using System.Threading.Tasks;
using FriendsGamesTools.Ads;
using UnityEngine;

namespace HC
{
    public class UnlockProgressSkinWindow : HCWindow
    {
        public static async Task<bool> Showing() => await Show<UnlockProgressSkinWindow>().AskingForUnlock();
        [SerializeField] WatchAdButtonView unlockButton;
        private void Awake()
        {
            unlockButton.SubscribeAdWatched(OnAdWatched);
        }
        public override void OnClosePressed() => ReceiveAnswer(false);
        void OnAdWatched() => ReceiveAnswer(true);
        void ReceiveAnswer(bool unlock)
        {
            this.unlock = unlock;
            answerReceived = true;
        }
        bool answerReceived;
        bool unlock;
        async Task<bool> AskingForUnlock()
        {
            answerReceived = false;
            await Awaiters.Until(() => answerReceived);
            shown = false;
            return unlock;
        }
    }
}