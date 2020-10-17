using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HC
{
    public class StarView : MonoBehaviour
    {
        public GameObject slotParent;
        public GameObject starParent;

        public void SetState(bool collected)=> starParent.SetActive(collected);
    }
}