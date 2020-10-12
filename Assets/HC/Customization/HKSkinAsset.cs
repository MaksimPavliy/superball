using UnityEngine;

namespace HC
{
    public class HKSkinAsset : ScriptableObject
    {
        public string Name = "%BASE_SKIN%";
        public int Index = 0;
        public Sprite SkinImage;
        public bool Locked = true;
        public int price = 100;
    }
}