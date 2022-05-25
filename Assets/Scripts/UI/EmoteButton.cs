using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class EmoteButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TextMeshProUGUI buttonText;
    public TextMeshProUGUI infoText;
    public GameObject infoUI;

    int index;

    FaceController faceController;

    void Start()
    {
        infoUI.SetActive(false);
    }


    public void Init(FaceController controller, int index, string emoteName = "")
    {
        this.index = index;
        faceController = controller;
        buttonText.text = "Emote\n" + index;
        infoText.text = (emoteName == "") ? buttonText.text : emoteName;
    }
    

    public void OnPointerEnter(PointerEventData eventData)
    {
        infoUI.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        infoUI.SetActive(false);
    }

    public void OnClick()
    {
        faceController.PlayFaceAnim(index - 1);
    }
}
