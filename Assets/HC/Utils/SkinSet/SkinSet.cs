using FriendsGamesTools;
using System;
using System.Collections;
using System.Collections.Generic;
namespace HcUtils
{
    public class SkinSet : MonoBehaviourHasInstance<SkinSet>
    {
        public static Action<int> SkinSetChanged;
        public static int activeSet = 0;
        public int StartSet = 0;
        protected override void Awake()
        {
            base.Awake();
            SkinSetChanged += OnSkinSetChanged;
            activeSet = StartSet;
        }

        private void OnSkinSetChanged(int ind) => activeSet = ind;
    }
}