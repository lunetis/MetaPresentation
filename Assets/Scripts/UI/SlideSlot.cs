using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SlideSlot : MonoBehaviour
{
    PresentationData data;
    public RawImage slideImage;
    public TextMeshProUGUI slideNumberText;
    public int slideNumber;

    // Start is called before the first frame update
    public void Init(int number, PresentationData data)
    {
        this.data = data;

        // Default slide image texture: movie clip
        if(data.isVideo == false)
        {
            slideImage.texture = data.slideTexture;
        }
        
        slideNumber = number;
        slideNumberText.text = "Slide " + number;
    }

    public void OnClick()
    {
        SlideSettingsUIController slideController = null;
        Transform parentTransform = transform.parent;
        while(parentTransform != null)
        {
            slideController = parentTransform.GetComponent<SlideSettingsUIController>();

            if(slideController != null)
                break;

            parentTransform = parentTransform.parent;
        }

        if(slideController == null)
            return;

        slideController.ChangeSlidePreviewImage(data, transform);
    }
}
