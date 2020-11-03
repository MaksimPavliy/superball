using FriendsGamesTools.UI;

namespace HC
{
    public class HCWindows : Windows
    {
        protected override bool backShown => base.backShown && !LevelBasedView.instance.mainMenu.shown;
    }
}