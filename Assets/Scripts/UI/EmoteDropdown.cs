using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EmoteDropdown : MonoBehaviour
{
    public SlideSettingsUIController slideSettingsUIController;
    public EmoteUIController emoteUIController;
    public TMP_Dropdown dropdown;
    // Start is called before the first frame update
    void Start()
    {
        if(dropdown == null)
        {
            dropdown = GetComponent<TMP_Dropdown>();
        }
        
        if(slideSettingsUIController == null)
        {
            slideSettingsUIController = FindObjectOfType<SlideSettingsUIController>();
        }
    }

    public void Init(EmoteUIController emoteUIController)
    {
        this.emoteUIController = emoteUIController;
    
        dropdown.ClearOptions();
        dropdown.AddOptions(emoteUIController.emoteNames);
    }

    public void SetSelectedDropdown(PresentationData data)
    {
        dropdown.value = data.slideEmoteIndex;
    }

    public void OnSelect(int index)
    {
        slideSettingsUIController.selectedData.slideEmoteIndex = index;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
