using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TitleText : MonoBehaviour
{
    public TextMeshProUGUI titleText;
    // Start is called before the first frame update
    void Start()
    {
        if(titleText == null)
        {
            titleText = GetComponent<TextMeshProUGUI>();
        }
    }
}
