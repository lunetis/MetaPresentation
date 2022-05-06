using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlideSettingsUIController : MonoBehaviour
{
    public GameObject slideSlotPrefab;
    public GameObject scrollContent;
    public RawImage slidePreviewImage;
    
    public Transform selectedSlot;
    public List<GameObject> slideSlots;

    public PresentationController presentationController;
    
    // Used in init, resize
    float slotHeight = 0;
    float contentsHeight = 0;

    public void Init(Texture2D[] textures)
    {
        SlideSlot slideSlotScript;
        int slideNo = 1;

        foreach(Texture2D tex in textures)
        {
            // Create slide slot
            GameObject slideSlot = Instantiate(slideSlotPrefab, Vector3.zero, Quaternion.identity);
            slideSlot.transform.SetParent(scrollContent.transform);
            slideSlots.Add(slideSlot);

            // If the slot height is undefined, get slot height
            if(slotHeight == 0)
            {
                slotHeight = slideSlot.GetComponent<RectTransform>().rect.height;
            }

            slideSlotScript = slideSlot.GetComponent<SlideSlot>();

            if(slideSlotScript == null)
            {
                Debug.LogWarning("No SlideSlot script!");
                continue;
            }

            slideSlotScript.Init(tex, slideNo++);
        }

        // Refresh scroll contents height
        ResizeScrollContent();
    }

    void ResizeScrollContent()
    {
        int slideCount = scrollContent.transform.childCount;
        RectTransform scrollRectTransform = scrollContent.GetComponent<RectTransform>();
        VerticalLayoutGroup contentLayout = scrollContent.GetComponent<VerticalLayoutGroup>();

        contentsHeight = slotHeight * (slideCount - 1) + (contentLayout.spacing * (slideCount));
        
        scrollRectTransform.sizeDelta = new Vector2(scrollRectTransform.sizeDelta.x, contentsHeight);
    }

    public void ChangeSlidePreviewImage(Texture2D texture, Transform slotTransform)
    {
        slidePreviewImage.texture = texture;
        selectedSlot = slotTransform;
    }

    // -1: down, 1: up
    public void ChangeOrder(int direction)
    {
        if(selectedSlot == null)
        {
            Debug.Log("no selected slot");
            return;
        }

        int siblingIndex = selectedSlot.GetSiblingIndex();
        int maxIndex = selectedSlot.parent.childCount - 1;

        selectedSlot.SetSiblingIndex(Mathf.Clamp(siblingIndex + direction, 0, maxIndex));
    }

    public void Apply()
    {
        // Create new index list with current slides
        List<int> currentIndices = new List<int>();
        int number;
        foreach(Transform child in scrollContent.transform)
        {
            number = child.GetComponent<SlideSlot>().slideNumber;
            currentIndices.Add(number);

            Debug.Log(number);
        }

        presentationController.ApplyNewSlideList(currentIndices);
        Toggle();
    }

    public void Remove()
    {
        Destroy(selectedSlot.gameObject);
        ResizeScrollContent();
    }

    public void Toggle()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }
}
