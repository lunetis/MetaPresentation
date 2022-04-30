using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Text_Change_Map_2 : MonoBehaviour
{
    public Text textObject;

    private void Start()
    {
        textObject = GetComponent<Text>();
    }
    public void TextChange()
    {
        textObject = GetComponent<Text>();
        //textObject.text = "Map (no.2)";
        textObject.text = "<color=#22222>" + "Map (no.2)" + "</color>";
    }
}
