using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuestSettingsController : MonoBehaviour
{
    bool hasSelectedMap = false;
    bool hasSelectedCharacter = true;

    public Button startButton;

    public bool HasSelectedMap
    {
        get { return hasSelectedMap; }
        set { hasSelectedMap = value; CheckStartConditions(); }
    }

    public bool HasSelectedCharacter
    {
        get { return hasSelectedCharacter; }
        set { hasSelectedCharacter = value; CheckStartConditions(); }
    }
    
    void CheckStartConditions()
    {
        if(hasSelectedMap == true && hasSelectedCharacter == true)
        {
            startButton.interactable = true;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        PresentationDataObject.guestObject = null;
    }
}
