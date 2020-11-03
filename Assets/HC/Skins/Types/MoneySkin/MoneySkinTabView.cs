using System.Collections.Generic;
using FriendsGamesTools;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace HC
{
    public class MoneySkinTabView: HCSkinsTabView<MoneySkin>
    {
        public override string TabName => "Skins";
        public override string TabHint => "Unlock random skin.";
        public MoneySkinConfig config => MoneySkinConfig.instance;
        new MoneySkinController controller => (MoneySkinController)base.controller;
        public bool enoughMoney => HCRoot.instance.money.amount >= controller.nextSkinPrice;
        protected override IReadOnlyList<SkinViewConfig> viewConfigs => MoneySkinsViewConfig.instance.items;
        [SerializeField] Button unlockButton;
        [SerializeField] TextMeshProUGUI unlockPriceText;
        protected override void Awake()
        {
            base.Awake();
            unlockButton.Safe(() => unlockButton.onClick.AddListener(OnUnlockRandomSkinPressed));
        }
        public override void UpdateView()
        {
            base.UpdateView();
            unlockButton.gameObject.SetActive(isActiveTab && controller.anySkinLocked);
            unlockPriceText.text = ((double)controller.nextSkinPrice).ToStringWithSuffixes();
        }
        public void OnUnlockRandomSkinPressed()
        {
            controller.BuyRandomSkin();
            UpdateView();
        }
        protected override void Update() => unlockButton.interactable = controller.buySkinAvailable;
    }
}