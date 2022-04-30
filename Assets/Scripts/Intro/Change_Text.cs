using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Change_Text : MonoBehaviour
{
    public Text textObject; 
    public TextMesh textMesh;

    private void Start()
    {
        textObject = GetComponent<Text>();
    }
    public void File_Selected()
    {
        textObject = GetComponent<Text>();
        textObject.text = "<color=#22222>" + "Select Folder(Yes Selected)" + " </color>";


        //  textMesh = GetComponent<TextMesh>();
        //  textMesh.text = "Select Folder(Yes Selected)";

    }
}
