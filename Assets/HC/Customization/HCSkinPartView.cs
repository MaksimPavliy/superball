using UnityEngine;

namespace HC
{
    public abstract class HCSkinPartView : MonoBehaviour//remove
    {
        public GameObject[] Parts;
        public void SetActiveSkin(int index)
        {
            for (var i = 0; i < Parts.Length; i++)
            {
                Parts[i].SetActive(i == index);
            }
        }
    }
}