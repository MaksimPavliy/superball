using System;
using System.Collections.Generic;
using FriendsGamesTools;
using UnityEngine;

namespace HC
{
    public class ProgressSkinsViewConfig : MonoBehaviourHasInstance<ProgressSkinsViewConfig>
    {
        public List<ProgressSkinViewConfig> items = new List<ProgressSkinViewConfig>();
    }
    [Serializable] public class ProgressSkinViewConfig : SkinViewConfig
    {
        public Sprite icoFilled;
    }
}