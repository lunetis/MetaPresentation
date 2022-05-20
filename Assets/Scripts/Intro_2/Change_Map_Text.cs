using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Change_Map_Text : MonoBehaviour
{
    public Text textObject;
    //public TextMesh textMesh;

    private void Start()
    {
        textObject = GetComponent<Text>();
    }
    public void Map1_Selected()
    {
        textObject = GetComponent<Text>();
        //textObject.text = "<color=#22222>" + "( Map 1 Selected)" + " </color>";
        textObject.text = "(Map 1)";

    }
    public void Map2_Selected()
    {
        textObject = GetComponent<Text>();
        //textObject.text = "<color=#22222>" + "( Map 2 Selected)" + " </color>";
        textObject.text = "(Map: Office)";

    }
    public void Map3_Selected()
    {
        textObject = GetComponent<Text>();
        //textObject.text = "<color=#22222>" + "( Map 3 Selected)" + " </color>";
        textObject.text = "(Map: Lecture Room)";

    }
}
