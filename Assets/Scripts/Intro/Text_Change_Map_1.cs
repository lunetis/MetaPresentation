using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Text_Change_Map_1 : MonoBehaviour
{
    public Text textObject;

    private void Start()
    {
        textObject = GetComponent<Text>();
    }
    public void TextChange()
    {
        textObject = GetComponent<Text>();
        //textObject.text = "Map (no.1)";
        textObject.text = "<color=#22222>" + "Map (no.1)" +"</color>";
    }
}
