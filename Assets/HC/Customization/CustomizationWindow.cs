using FriendsGamesTools;
using FriendsGamesTools.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace HC
{
    public class CustomizationWindow : Window
    {
        [SerializeField] Button closeButton;
        [SerializeField] Button unlockButton;
        [SerializeField] TextMeshProUGUI unlockPriceText;
        [SerializeField] TextMeshProUGUI header;
        [SerializeField] TextMeshProUGUI hint;
        [SerializeField] ScrollRect scrollRect;
        [SerializeField] HCSkinsTabView[] tabs;
        bool unlockAvailable => activeTabIndex == 0;
        int activeTabIndex = 0;
        HCSkinsTabView activeTab => activeTabIndex < tabs.Length ? tabs[activeTabIndex] : null;
        bool enoughMoney => activeTab.enoughMoney;
        bool allUnlocked => activeTab.allUnlocked;
        private void Awake()
        {
            closeButton?.onClick.AddListener(delegate { HCRoot.instance.GoToMain(); });
            unlockButton?.onClick.AddListener(delegate { activeTab.UnlockRandom(); });
            scrollRect.onValueChanged.AddListener(delegate { ScrollValueChanged(); });
        }
        void ScrollValueChanged()
        {
            activeTabIndex = Mathf.Clamp((int)Mathf.Round(scrollRect.horizontalNormalizedPosition), 0, 1);
            header.text = tabs[activeTabIndex].TabName;
            unlockButton?.gameObject.SetActive(unlockAvailable);
            hint.text = activeTab.TabHint;
        }

        void UpdateView()
        {
            foreach (var tab in tabs)
            {
                tab.UpdateView();
            }
            ScrollValueChanged();
            unlockPriceText.text = ((double)activeTab.unlockPrice).ToStringWithSuffixes();
            unlockButton.interactable = !allUnlocked && enoughMoney;
        }
        private void OnEnable() => UpdateView();
        public void Show(bool show = true)
        {
            shown = show;
        }
    }
}

