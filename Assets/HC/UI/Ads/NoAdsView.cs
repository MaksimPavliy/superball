using FriendsGamesTools.IAP;

namespace HC
{
    public class NoAdsView : PurchasableProductView
    {
#if IAP
        private void Update()
        {
            this.gameObject.SetActive(!IAPManager.instance.interstitialsRemoved);
        }
#endif
    }
}