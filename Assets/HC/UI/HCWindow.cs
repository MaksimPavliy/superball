using FriendsGamesTools.UI;

namespace HC
{
    public abstract class HCWindow : Window 
    {
        public HCRoot root => HCRoot.instance;
        public static async void ShowWithDelay<T>(float delay) where T : Window
        {
            await Awaiters.SecondsRealtime(delay);
            Show<T>();
        }
    }
}