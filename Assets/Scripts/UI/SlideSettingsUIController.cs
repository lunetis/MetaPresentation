using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class SlideSettingsUIController : MonoBehaviour
{
    public GameObject slideSlotPrefab;
    public GameObject scrollContent;
    public RawImage slidePreviewImage;

    public RenderTexture videoRenderTexture;
    
    public List<GameObject> slideSlots;

    public PresentationController presentationController;

    public VideoPlayer videoPlayer;
    
    [HideInInspector]
    public Transform selectedSlot;
    
    
    // Used in init, resize
    float slotHeight = 0;
    float contentsHeight = 0;

    void Start()
    {
        // VideoPlayer Component must be in slidePreviewImage object
        // videoPlayer = slidePreviewImage.GetComponent<VideoPlayer>();
        selectedSlot = null;
        gameObject.SetActive(false);
    }

    public void Init(List<PresentationData> dataList)
    {
        gameObject.SetActive(true);
        SlideSlot slideSlotScript;
        int slideNo = 1;

        foreach(Transform child in scrollContent.transform)
        {
            Destroy(child.gameObject);
        }

        foreach(var data in dataList)
        {
            // Create slide slot
            GameObject slideSlot = Instantiate(slideSlotPrefab, Vector3.zero, Quaternion.identity);
            slideSlot.transform.SetParent(scrollContent.transform);
            slideSlots.Add(slideSlot);
            slideSlot.GetComponent<RectTransform>().localScale = Vector3.one;

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

            slideSlotScript.Init(slideNo++, data);

            // Set first slot to selectedSlot
            if(selectedSlot == null)
            {
                selectedSlot = slideSlot.transform;
            }
        }

        // Refresh scroll contents height
        ResizeScrollContent(dataList.Count);
        
        ChangeSlidePreviewImage(dataList[0], selectedSlot);
    }

    // If slideCount == -1, Counts child itself
    void ResizeScrollContent(int slideCount = -1)
    {
        if(slideCount == -1)
        {
            slideCount = scrollContent.transform.childCount;
        }
        RectTransform scrollRectTransform = scrollContent.GetComponent<RectTransform>();
        VerticalLayoutGroup contentLayout = scrollContent.GetComponent<VerticalLayoutGroup>();

        contentsHeight = slotHeight * (slideCount) + (contentLayout.spacing * (slideCount + 1));
        
        scrollRectTransform.sizeDelta = new Vector2(scrollRectTransform.sizeDelta.x, contentsHeight);
    }

    public void ChangeSlidePreviewImage(PresentationData data, Transform slotTransform)
    {
        if(data.isVideo == true)
        {
            videoPlayer.url = data.videoUrl;
            slidePreviewImage.texture = videoRenderTexture;
            StartCoroutine(PlayVideo());
        }
        else
        {
            if(videoPlayer.isPlaying == true)
            {
                videoPlayer.Stop();
                Debug.Log("Stopped");
            }
            slidePreviewImage.texture = data.slideTexture;
        }
        
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

    
    IEnumerator PlayVideo()
    {
        videoPlayer.Prepare();

        while(videoPlayer.isPrepared == false)
        {
            yield return null;
        }

        videoPlayer.Play();
        Debug.Log("Play start");
    }
}
