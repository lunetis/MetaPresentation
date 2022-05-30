using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelectButton : MonoBehaviour
{
    public GameObject characterObject;

    public void OnClick()
    {
        if(characterObject == null)
        {
            Debug.LogWarning("No character object in the button");
            return;
        }
        PresentationDataObject.characterObject = characterObject;
    }
}
