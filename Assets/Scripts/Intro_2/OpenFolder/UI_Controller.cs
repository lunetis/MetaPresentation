using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class UI_Controller : MonoBehaviour
{
    public GameObject slideSlotPrefab;
    public GameObject scrollContent;
   // public RawImage slidePreviewImage;

   // public RenderTexture videoRenderTexture;

   // public Transform selectedSlot;
    public List<GameObject> slideSlots;

    //public PresentationController presentationController;

   // public VideoPlayer videoPlayer;


    // Used in init, resize
    float slotHeight = 0;
    float contentsHeight = 0;
    public void Init(List<PresentationData> dataList)
    {
        SlideSlot slideSlotScript;
        int slideNo = 1;

        foreach (var data in dataList)
        {
            // Create slide slot
            GameObject slideSlot = Instantiate(slideSlotPrefab, Vector3.zero, Quaternion.identity);
            slideSlot.transform.SetParent(scrollContent.transform);
            slideSlots.Add(slideSlot);

            // If the slot height is undefined, get slot height
            if (slotHeight == 0)
            {
                slotHeight = slideSlot.GetComponent<RectTransform>().rect.height;
            }

            slideSlotScript = slideSlot.GetComponent<SlideSlot>();

            if (slideSlotScript == null)
            {
                Debug.LogWarning("No SlideSlot script!");
                continue;
            }

            slideSlotScript.Init(slideNo++, data);
        }

        // Refresh scroll contents height
        ResizeScrollContent();
    }

    void ResizeScrollContent()
    {
        int slideCount = scrollContent.transform.childCount;
        RectTransform scrollRectTransform = scrollContent.GetComponent<RectTransform>();
        VerticalLayoutGroup contentLayout = scrollContent.GetComponent<VerticalLayoutGroup>();

        contentsHeight = slotHeight * (slideCount) + (contentLayout.spacing * (slideCount + 1));

        scrollRectTransform.sizeDelta = new Vector2(scrollRectTransform.sizeDelta.x, contentsHeight);
    }
}
