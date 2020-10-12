using UnityEngine;

namespace HC
{
    public class HKSkinView : MonoBehaviour
    {
        public HKSkinPartView[] Parts;
        public void SetActiveSkin(int index)
        {
            for (var i = 0; i < Parts.Length; i++)
            {
                Parts[i].SetActiveSkin(index);
            }
        }
    }
}