using System;
using System.Collections.Generic;
using FriendsGamesTools;
using FriendsGamesTools.DebugTools;
using FriendsGamesTools.ECSGame;
using Unity.Entities;
using UnityEngine;

namespace HC
{
    public struct MoneySkin : IComponentData { bool _; }
    public class MoneySkinController : HCSkinsController<MoneySkin>
    {
        protected override IReadOnlyList<SkinViewConfig> viewConfigs => MoneySkinsViewConfig.instance.items;
        MoneySkinConfig config => MoneySkinConfig.instance;
        public int nextSkinPrice => config.priceBase * Utils.Pow(config.priceBase, unlockedSkinsCount - 1);
        public bool enoughMoney => nextSkinPrice <= root.money.amount;
        public bool buySkinAvailable => anySkinLocked && enoughMoney;
        public void BuyRandomSkin()
        {
            if (!buySkinAvailable) return;
            root.money.PayMoney(nextSkinPrice);
            var skinInd = Utils.GetIndList(skins.Length).Filter(ind => skins[ind].locked).RandomElement();
            UnlockSkin(skinInd);
            ActivateSkin(skinInd);
        }
    }

    [Serializable] public class MoneySkinViewConfig : SkinViewConfig
    {

    }
    public class MoneySkinsViewConfig : MonoBehaviourHasInstance<MoneySkinsViewConfig>
    {
        public List<MoneySkinViewConfig> items = new List<MoneySkinViewConfig>();
    }
    public class MoneySkinConfig : BalanceSettings<MoneySkinConfig>
    {
        public int priceBase = 200;
        public int priceMul = 2;
    }
}