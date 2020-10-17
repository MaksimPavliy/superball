using UnityEngine;

namespace HC
{
    public class HCSkinView : MonoBehaviour
    {
        public HCSkinPartView[] Parts;
        public void SetActiveSkin(int index)
        {
            for (var i = 0; i < Parts.Length; i++)
            {
                Parts[i].SetActiveSkin(index);
            }
        }
    }
}