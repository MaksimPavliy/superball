using FriendsGamesTools.DebugTools;
using UnityEngine;
using UnityEngine.UI;

namespace HC
{
    public class HCDebugPanelTab : DebugPanelItemView
    {
        [SerializeField] Image[] bgColors;
        [SerializeField] Text cameraIndex;

        public override (string tab, string name) whereToShow => ("HC","HC");

        protected override void AwakePlaying()
        {
            base.AwakePlaying();
        }
        private void Start()
        {
            ChangeCameraIndex(HCGeneralConfig.instance.CameraIndex);
        }
        public void ChangeCameraIndex(int index)
        {
            var selector = HCLevelsView.instance.shownLocationView.GetComponent<HcUtils.CameraSelector>();
            if (!selector) return;

            var newInd = (HCGeneralConfig.instance.CameraIndex + Mathf.Clamp(index, -1, 1) + selector.Count) % selector.Count;

            HCGeneralConfig.instance.CameraIndex = newInd;

            selector.SetActiveCamera(newInd);
            cameraIndex.text = newInd.ToString();

        }

        public void SetBackgroundColor(int index)
        {
            Camera.main.backgroundColor = bgColors[index].color;
        }
    }
}