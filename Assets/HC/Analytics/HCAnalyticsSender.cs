using FriendsGamesTools.Ads;
using System;
using UnityEngine;

namespace HC
{
    public class HCAnalyticsSender: MonoBehaviour
    {
        public WatchAdButtonView AdButton;
        public Action<bool> onFinished;
        public Action onPressed;
        void Start()
        {
            if(onFinished!=null) AdButton.SubscribeAdWatched(onFinished);
            if(onPressed!=null) AdButton.SubscribeWatchAdPressed(onPressed);
        }
    }
}
