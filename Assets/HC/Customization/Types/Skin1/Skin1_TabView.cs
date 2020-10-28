using System.Collections.Generic;
using Unity.Entities;


namespace HC
{
    public class Skin1_TabView: HCSkinsTabView<MoneySkin>
    {
        public override string TabName => "Skins";
        public override string TabHint => "Unlock random skin.";
        public MoneySkinConfig config => MoneySkinConfig.instance;
        new MoneySkinController controller => (MoneySkinController)base.controller;
        public bool enoughMoney => HCRoot.instance.money.amount >= controller.nextSkinPrice;

        protected override IReadOnlyList<SkinViewConfig> viewConfigs => MoneySkinsViewConfig.instance.items;

        public void OnUnlockRandomSkinPressed()
        {
            controller.BuyRandomSkin();
            UpdateView();
        }
    }
}
  
