using FriendsGamesTools;
using UnityEngine;

namespace HC
{
    public class CoreGameView : MonoBehaviourHasInstance<CoreGameView>
    {
        bool shown => shownParent.activeSelf;
        bool isPlaying => HCRoot.instance.levels.state == Level.State.playing;
        [SerializeField] GameObject shownParent;
        private void Update()
        {
            var shouldBeShown = isPlaying && !LevelBasedView.instance.skinsWindow.shown;
            if (shown != shouldBeShown)
                SetShown(shouldBeShown);
        }
        protected virtual void SetShown(bool shown)
        {
            shownParent.SetActive(shown);
            if (shown)
                OnCoreGameShown();
        }
        protected virtual void OnCoreGameShown()
        {
            LevelBasedView.SetLevelText(string.Empty);
        }
    }
}