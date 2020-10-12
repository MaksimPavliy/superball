using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HC
{
    public class ScrollArrowsView : MonoBehaviour
    {
        [SerializeField] ScrollRect scrollRect;
        [SerializeField] Button arrowLeft;
        [SerializeField] Button arrowRight;
        private void Update()
        {
            arrowLeft.interactable = scrollRect.horizontalNormalizedPosition >= 0.5f;
            arrowRight.interactable = scrollRect.horizontalNormalizedPosition <= 0.5f;
        }
    }
}