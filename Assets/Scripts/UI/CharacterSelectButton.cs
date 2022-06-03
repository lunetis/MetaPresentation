using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectButton : MonoBehaviour
{
    public GameObject characterObject;
    public RawImage characterImage;

    public Texture characterTexture;

    public void OnClick()
    {
        if(characterObject == null)
        {
            Debug.LogWarning("No character object in the button");
            return;
        }
        PresentationDataObject.hostObject = characterObject;
        characterImage.texture = characterTexture;
        characterImage.transform.parent.gameObject.SetActive(true);
    }
}
