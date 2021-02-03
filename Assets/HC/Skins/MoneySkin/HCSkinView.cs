using System.Collections.Generic;
using UnityEngine;

namespace HC
{
    public class HCSkinView : MonoBehaviour
    {
        [SerializeField]
        private List<GameObject> skins;
      public void SetActiveSkin(int index)
        {
            for (int i = 0; i < skins.Count; i++)
            {
                skins[i].SetActive(i == index);
            }
        }
        private void Start()
        {
            HCSelectorView.AddSkin(this);
        }
    }
}