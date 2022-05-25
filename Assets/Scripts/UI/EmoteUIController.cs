using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmoteUIController : MonoBehaviour
{
    FaceController faceController;
    public GameObject emoteButtonPrefab;
    public Transform emoteButtonPanel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void InitEmoteButtonPanel(FaceController faceController)
    {
        this.faceController = faceController;

        for(int i = 0; i < faceController.animations.Length; i++)
        {
            GameObject button = Instantiate(emoteButtonPrefab);
            button.transform.SetParent(emoteButtonPanel);
            button.GetComponent<RectTransform>().localScale = Vector3.one;
            button.GetComponent<EmoteButton>().Init(faceController, i + 1, faceController.animations[i].name);
        }
    }
}
