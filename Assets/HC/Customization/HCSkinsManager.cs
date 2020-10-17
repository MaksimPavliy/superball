using FriendsGamesTools;
using FriendsGamesTools.ECSGame;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

namespace HC
{

    public abstract class HCSkinsManager : MonoBehaviour 
    {
        public HCSkinAsset[] SkinAssets;
        List<HCSkinView> skinViews; //all skinnable objects should be set manually
        protected virtual HCSkinsController controller => null;
        int lastIndex = -1;
        public void SetSkin(int index)  
        {
            if (skinViews==null) return;
            foreach (var skin in skinViews)
            {
                skin.SetActiveSkin(index);
            }
        }
        private void Update()
        {
            var index = controller.skinsData.activeIndex;
            if (index != lastIndex)
            {
                SetSkin(index);
                lastIndex = index;
            }
        }
    }
    public class HCSkinsManager<T> : HCSkinsManager where T : struct, IComponentData
    {
        protected override HCSkinsController controller => HCRoot.instance.Get<HCSkinsController<T>>();
    }
}