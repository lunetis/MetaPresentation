using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmoteUIController : MonoBehaviour
{
    public FaceController faceController;
    public EmoteDropdown emoteDropdown;
    public GameObject emoteButtonPrefab;
    public Transform emoteButtonPanel;

    public List<string> emoteNames;

    public void InitEmoteButtonPanel(FaceController faceController)
    {
        this.faceController = faceController;
        emoteNames = new List<string>();

        for(int i = 0; i < faceController.animations.Length; i++)
        {
            GameObject button = Instantiate(emoteButtonPrefab);
            button.transform.SetParent(emoteButtonPanel);
            button.GetComponent<RectTransform>().localScale = Vector3.one;
            button.GetComponent<EmoteButton>().Init(faceController, i + 1, faceController.animations[i].name);

            emoteNames.Add(faceController.animations[i].name);
        }

        emoteDropdown.Init(this);
    }

    
    public void InitEmoteButtonPanel(FaceController faceController, string[] animationList)
    {
        this.faceController = faceController;
        emoteNames = new List<string>();

        for(int i = 0; i < animationList.Length; i++)
        {
            GameObject button = Instantiate(emoteButtonPrefab);
            button.transform.SetParent(emoteButtonPanel);
            button.GetComponent<RectTransform>().localScale = Vector3.one;
            button.GetComponent<EmoteButton>().Init(faceController, i + 1, animationList[i]);
            
            emoteNames.Add(animationList[i]);
        }
        
        emoteDropdown.Init(this);
    }
}
