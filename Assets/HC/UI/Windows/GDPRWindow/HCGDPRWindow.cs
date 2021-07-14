using FriendsGamesTools;
using UnityEngine;

namespace HC
{
    public class HCGDPRWindow : GDPRWindow
    {
#if GDPR
        public override void OnDenyPressed()
        {
            shown = false;

            Application.Quit();
        }

        public override void OnClosePressed()
        {
            base.OnClosePressed();

            FriendsGamesTools.ECSGame.MainMenuWindow.Show();
        }
#endif
    }
}