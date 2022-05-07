using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Map_img_change : MonoBehaviour
{
    public Sprite map_1;
    public Sprite map_2;
    public Sprite map_3;
    Image thisImg;
    private void Start()
    {
        thisImg = GetComponent<Image>();
        thisImg.sprite = map_1;
    }

    public void Select_Map1()
    {
        thisImg = GetComponent<Image>();
        thisImg.sprite = map_1;
    }

    public void Select_Map2()
    {
        thisImg = GetComponent<Image>();
        thisImg.sprite = map_2;
    }

    public void Select_Map3()
    {
        thisImg = GetComponent<Image>();
        thisImg.sprite = map_3;
    }
}
