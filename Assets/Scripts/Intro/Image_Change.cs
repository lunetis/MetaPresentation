using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Image_Change : MonoBehaviour
{
    public Sprite change_img;
    Image thisImg;


   
    public void ChangeImage()
    {
        thisImg = GetComponent<Image>();
        thisImg.sprite = change_img;
    }

}
