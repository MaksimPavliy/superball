using UnityEngine;

namespace HC
{
    public class StarView : MonoBehaviour
    {
        public GameObject starParent;
        public void SetState(bool collected)=> starParent.SetActive(collected);
    }
}