using System.Threading.Tasks;
using FriendsGamesTools.Ads;
using UnityEngine;
using UnityEngine.UI;

namespace HC
{
    public class UnlockProgressSkinWindow : HCWindow
    {
        public static async Task<bool> Showing() => await Show<UnlockProgressSkinWindow>().AskingForUnlock();
        [SerializeField] Image ico, icoFilled;
        [SerializeField] WatchAdButtonView unlockButton;
        private void Awake() => unlockButton.SubscribeAdWatched(OnAdWatched);
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
            ProgressSkinItemView.Show(ico, icoFilled, HCRoot.instance.progressSkin.skinIndToUnlock, 1f);
            await Awaiters.Until(() => answerReceived);
            shown = false;
            return unlock;
        }
    }
}