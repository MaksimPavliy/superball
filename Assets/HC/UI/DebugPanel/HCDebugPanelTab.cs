using FriendsGamesTools.DebugTools;
using UnityEngine;
using UnityEngine.UI;

namespace HC
{
    public class HCDebugPanelTab : DebugPanelItemView
    {
        [SerializeField] Toggle is60FPStoggle;
        [SerializeField] Image[] bgColors;

        public override (string tab, string name) whereToShow => ("HC","HC");

        protected override void AwakePlaying()
        {
            base.AwakePlaying();

            is60FPStoggle.onValueChanged.AddListener((is60fps) => SetTargetFPS(is60fps));
        }

        void SetTargetFPS(bool is60fps)
        {
            Application.targetFrameRate = is60fps ? 60 : 30;
        }

        public void SetBackgroundColor(int index)
        {
            Camera.main.backgroundColor = bgColors[index].color;
        }
    }
}