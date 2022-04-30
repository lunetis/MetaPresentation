using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button_Activate : MonoBehaviour
{
    public Button input_btn;
    public void Button_off()
    { 
        input_btn.interactable = false;
    }

    public void Button_on()
    {
        input_btn.interactable = true;
    }
}
