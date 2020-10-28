using System;
using System.Collections.Generic;
using FriendsGamesTools;
using FriendsGamesTools.DebugTools;
using FriendsGamesTools.ECSGame;
using Unity.Entities;
using UnityEngine;

namespace HC
{
    public struct ProgressSkin : IComponentData { bool _; }
    public class ProgressSkinController : HCSkinsController<ProgressSkin>
    {
        public bool lockedSkinExists => throw new System.Exception();
        protected override IReadOnlyList<SkinViewConfig> viewConfigs => ProgressSkinsViewConfig.instance.items;
    }
    [Serializable]
    public class SkinViewConfig
    {
        public Sprite ico;
        public bool startUnlocked;
    }
    [Serializable]
    public class ProgressSkinViewConfig : SkinViewConfig
    {

    }
    public class ProgressSkinsViewConfig : MonoBehaviourHasInstance<ProgressSkinsViewConfig>
    {
        public List<ProgressSkinViewConfig> items = new List<ProgressSkinViewConfig>();
    }

    public class ProgressSkinConfig : BalanceSettings<ProgressSkinConfig>
    {
        
    }
}