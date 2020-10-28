using FriendsGamesTools.Ads;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HC
{
    public class HCAdsManager
#if ADS
    : AdsManager<HCAdsManager>
#else
    : MonoBehaviour
#endif
    {
        public static bool AdsProposingEnabled =>
#if ADS
    true;
#else
    false;
#endif
    }
}