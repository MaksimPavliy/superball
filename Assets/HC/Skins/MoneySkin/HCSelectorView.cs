using FriendsGamesTools;
using FriendsGamesTools.ECSGame;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace HC
{
    public class HCSelectorView:MonoBehaviourHasInstance<HCSelectorView>
    {
        public static List<HCSkinView> ActiveSkins = new List<HCSkinView>();
        public static void AddSkin(HCSkinView skin) => ActiveSkins.Add(skin);
        public void CleanUp() => ActiveSkins = ActiveSkins.Where(item => item != null).ToList();
        int lastSkinIndex = -1;
        private void Update()
        {
            var skinInd = GameRoot.instance.Get<HCMoneySkinController>().activeSkinInd;
            if (skinInd != lastSkinIndex)
            {
                lastSkinIndex = skinInd;
                UpdateSkins();
            }
        }
        public void UpdateSkins()
        {
            CleanUp();
            foreach (var skin in ActiveSkins)
            {
                skin.SetActiveSkin(GameRoot.instance.Get<HCMoneySkinController>().activeSkinInd);
            }
        }
    }
}