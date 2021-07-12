using FriendsGamesTools;
using System;
using System.Collections;
using System.Collections.Generic;
namespace HcUtils
{
    public class ThemeSet : MonoBehaviourHasInstance<ThemeSet>
    {
        public static Action<int> ThemeSetChanged;
        public static int activeThemeIndex = 0;
        public int startThemeIndex = 0;
        protected override void Awake()
        {
            base.Awake();
            ThemeSetChanged += OnThemeChanged;
            activeThemeIndex = startThemeIndex;
        }

        private void OnThemeChanged(int ind) => activeThemeIndex = ind;
    }
}