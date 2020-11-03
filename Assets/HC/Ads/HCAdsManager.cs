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
    }
}