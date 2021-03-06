using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HostSettingsController : MonoBehaviour
{
    bool hasSelectedMap = false;
    bool hasSelectedFolder = false;
    bool hasSelectedCharacter = false;

    public Button startButton;

    public bool HasSelectedMap
    {
        get { return hasSelectedMap; }
        set { hasSelectedMap = value; CheckStartConditions(); }
    }
    public bool HasSelectedFolder
    {
        get { return hasSelectedFolder; }
        set { hasSelectedFolder = value; CheckStartConditions(); }
    }
    public bool HasSelectedCharacter
    {
        get { return hasSelectedCharacter; }
        set { hasSelectedCharacter = value; CheckStartConditions(); }
    }
    
    void CheckStartConditions()
    {
        if(hasSelectedMap == true && hasSelectedFolder == true && hasSelectedCharacter == true)
        {
            startButton.interactable = true;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // Clear data object
        if(PresentationDataObject.data != null)
        {
            PresentationDataObject.data.Clear();
        }
        
        PresentationDataObject.hostObject = null;
    }
}
