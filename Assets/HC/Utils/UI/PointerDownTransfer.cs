using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PointerDownTransfer : MonoBehaviour, IPointerDownHandler
{
    [SerializeField]
    private GameObject playButton;

    public void OnPointerDown(PointerEventData eventData)
    {
        ExecuteEvents.Execute(playButton.gameObject, eventData, ExecuteEvents.pointerDownHandler);
    }
}
