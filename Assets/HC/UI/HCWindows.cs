using FriendsGamesTools.ECSGame;
using FriendsGamesTools.UI;

namespace HC
{
    public class HCWindows : Windows
    {
        protected override bool backShown => base.backShown && !Windows.Get<MainMenuWindow>().shown;
    }
}