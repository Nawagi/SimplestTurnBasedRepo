using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ActivateChildIfOnPointerEnter : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    GameObject childInfo;

    void Awake()
    {
        childInfo = transform.GetChild(0).gameObject;
        childInfo.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        childInfo.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        childInfo.SetActive(false);
    }
}
