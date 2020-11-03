using FriendsGamesTools;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace HC
{
    public class SkinsWindow : HCWindow
    {
        public static void Show() => Show<SkinsWindow>();
        [SerializeField] Button closeButton;
        [SerializeField] TextMeshProUGUI header;
        [SerializeField] TextMeshProUGUI hint;
        [SerializeField] ScrollRect scrollRect;
        [SerializeField] HCSkinsTabView[] tabs;
        int activeTabIndex = 0;
        HCSkinsTabView activeTab => activeTabIndex < tabs.Length ? tabs[activeTabIndex] : null;
        private void OnEnable() => UpdateView();
        private void Awake()
        {
            if (closeButton != null)
                closeButton.onClick.AddListener(MainMenuWindow.Show);
            if (scrollRect != null)
                scrollRect.onValueChanged.AddListener(scrollValue => UpdateView());
        }
        public void UpdateView()
        {
            activeTabIndex = Mathf.Clamp((int)Mathf.Round(scrollRect.horizontalNormalizedPosition), 0, 1);
            header.text = tabs[activeTabIndex].TabName;
            hint.text = activeTab.TabHint;
            tabs.ForEachWithInd((tab, ind) => tab.SetActiveTab(ind == activeTabIndex));
        }
    }
}