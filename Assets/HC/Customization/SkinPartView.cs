using Unity.Entities;
using UnityEngine;

namespace HC
{
    public abstract class HKSkinPartView : MonoBehaviour
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
    public class SkinPartView<T> : HKSkinPartView where T : struct, IComponentData
    {

    }
}