using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace HC
{
    public class RewardProgressWindow : EndLevelWindow
    {
        [SerializeField] private Button _nextLevelButton;
        [SerializeField] private Button _doubleRewardButton;
        [SerializeField] Image progressBar;
        [SerializeField] TextMeshProUGUI progressText;
        float progressValue => 0.5f;
        private void Awake()
        {
            _nextLevelButton?.onClick.AddListener(NextLevel);
            _doubleRewardButton?.onClick.AddListener(delegate {/*double reward*/});
        }
        public void UpdateView()
        {
            progressBar.fillAmount = progressValue;
            progressText.text = $"{progressValue}%";
        }
        public override void Show(bool show = true)
        {
            base.Show(show);
            UpdateView();
        }
    }
}