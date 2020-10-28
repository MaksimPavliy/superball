using System;
using FriendsGamesTools;
using FriendsGamesTools.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace HC
{
    public class CustomizationWindow : HCWindow
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
        private void Awake()
        {
            if (closeButton != null)
                closeButton.onClick.AddListener(root.GoToMain);
            if (scrollRect != null)
                scrollRect.onValueChanged.AddListener(scrollValue => ScrollValueChanged());
            throw new Exception("whats with commented?");
            //if (unlockButton != null)
            //    unlockButton.onClick.AddListener(delegate { activeTab.UnlockRandom(); });
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
            throw new Exception("whats with commented?");
            //unlockPriceText.text = ((double)activeTab.unlockPrice).ToStringWithSuffixes();
            //unlockButton.interactable = !allUnlocked && enoughMoney;
        }
        private void OnEnable() => UpdateView();
        public void Show(bool show = true)
        {
            shown = show;
        }
    }
}

