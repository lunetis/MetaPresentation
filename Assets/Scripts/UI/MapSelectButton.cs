using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapSelectButton : MonoBehaviour
{
    public RawImage mapImage;
    public RawImage guestMapImage;

    public Texture mapTexture;

    public void OnClick()
    {
        mapImage.texture = mapTexture;
        mapImage.transform.parent.gameObject.SetActive(true);
        guestMapImage.texture = mapTexture;
        guestMapImage.transform.parent.gameObject.SetActive(true);
    }
}
