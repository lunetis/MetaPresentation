using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Map_btn : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject ipt;
    private void Start()
    {
        ipt.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ipt.SetActive(true);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        ipt.SetActive(false);
    }
}
