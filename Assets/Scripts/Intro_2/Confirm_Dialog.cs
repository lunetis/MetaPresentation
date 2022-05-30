using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Confirm_Dialog : MonoBehaviour
{
    public Transform box;
    public CanvasGroup background;

    public Text mapText;
    public Text characterText;

    private void OnEnable()
    {
        background.alpha = 0;
        background.LeanAlpha(1, 0.5f);

        box.localPosition = new Vector2(0, -Screen.height);
        box.LeanMoveLocalY(0, 0.5f).setEaseOutExpo().delay = 0.1f;
    }

    public void CloseDialog()
    {
        background.LeanAlpha(0, 0.5f);
        box.LeanMoveLocalY(-Screen.height, 0.5f).setEaseInExpo().setOnComplete(OnComplete);
    }

    void OnComplete()
    {
        gameObject.SetActive(false);
    }

    
    public void ChangeMapText(string mapName)
    {
        mapText.text = "Map : " + mapName;
    }

    public void ChangeCharacterText(string characterName)
    {
        characterText.text = "Character : " + characterName;
    }
}
