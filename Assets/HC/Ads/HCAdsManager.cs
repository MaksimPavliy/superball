using FriendsGamesTools.Ads;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HCAdsManager
#if ADS
    : AdsManager
#else
    : MonoBehaviour
#endif
{
}
