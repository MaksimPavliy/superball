using System;
using System.Collections.Generic;
using FriendsGamesTools;

namespace HC
{
    public class MoneySkinsViewConfig : MonoBehaviourHasInstance<MoneySkinsViewConfig>
    {
        public List<MoneySkinViewConfig> items = new List<MoneySkinViewConfig>();
    }
    [Serializable] public class MoneySkinViewConfig : SkinViewConfig
    {

    }
}