using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class CameraButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TextMeshProUGUI buttonText;
    public TextMeshProUGUI infoText;
    public GameObject infoUI;

    int index;

    PresentationCameraController cameraController;

    void Start()
    {
        infoUI.SetActive(false);
    }


    public void Init(PresentationCameraController controller, int index, string cameraName = "")
    {
        this.index = index;
        cameraController = controller;
        buttonText.text = "Cam " + index;
        infoText.text = (cameraName == "") ? buttonText.text : cameraName;

        if(index == 1)
        {
            GetComponent<Button>().Select();
        }
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
        cameraController.ChangeCamera(index - 1);
    }
}
