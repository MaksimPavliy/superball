using FriendsGamesTools.DebugTools;
using HcUtils;
using UnityEngine;
using UnityEngine.UI;

namespace HC
{
    public class HCDebugPanelTab : DebugPanelItemView
    {
        [SerializeField] Image[] bgColors;
        [SerializeField] Text cameraIndex;
        [SerializeField] Button[] skinSetButtons;

        public override (string tab, string name) whereToShow => ("HC","HC");

        protected override void AwakePlaying()
        {
            base.AwakePlaying();

            for (int i = 0; i < skinSetButtons.Length; i++)
            {
                int index = i;
                skinSetButtons[i].onClick.AddListener(delegate { SetSkinSet(index); });
            }
        }
        private void Start()
        {
            ChangeCameraIndex(HCGeneralConfig.instance.CameraIndex);
        }
        protected override void OnEnable()
        {
            base.OnEnable();
            for (int i = 0; i < skinSetButtons.Length; i++)
            {
                SetButtonSelected(skinSetButtons[i], i == SkinSet.activeSet);
            }
        }
        public void SetSkinSet(int index)
        {
            SkinSet.SkinSetChanged?.Invoke(index);

            for (int i = 0; i < skinSetButtons.Length; i++)
            {
                SetButtonSelected(skinSetButtons[i], i == index);
            }

        }
        void SetButtonSelected(Button button, bool selected)
        {
            var colors = button.colors;

            colors.normalColor = selected ? Color.green : Color.white;
            colors.selectedColor = selected ? Color.green : Color.white;
            button.colors = colors;
        }

        public void ChangeCameraIndex(int index)
        {
            int camerasCount = 5;

            var newInd = (HCGeneralConfig.instance.CameraIndex + Mathf.Clamp(index, -1, 1) + camerasCount) % camerasCount;

            HCGeneralConfig.instance.CameraIndex = newInd;
            cameraIndex.text = newInd.ToString();
            // CameraSwitch.Instance.CameraActivation(HCGeneralConfig.instance.CameraIndex);
        }

        public void SetBackgroundColor(int index)
        {
            Camera.main.backgroundColor = bgColors[index].color;
        }
    }
}