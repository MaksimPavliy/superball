using FriendsGamesTools;
using UnityEngine;

namespace HC
{
    public class CoreGameView : MonoBehaviourHasInstance<CoreGameView>
    {
        bool shown => gameObject.activeSelf;
        bool isPlaying => HCRoot.instance.levels.state == Level.State.playing;
        private void Update()
        {
            var shouldBeShown = isPlaying && !LevelBasedView.instance.skinsWindow.shown;
            if (shown != shouldBeShown)
                SetShown(shouldBeShown);
        }
        private void SetShown(bool shown) => gameObject.SetActive(shown);
    }
}