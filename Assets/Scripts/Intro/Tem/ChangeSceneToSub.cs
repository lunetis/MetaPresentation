using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangeSceneToSub : MonoBehaviour
{
    public int flag;
    public void OnRetry()
    {
        if (flag == 1)
        {
            SceneManager.LoadScene("Arena");
        }
        else
        {
            SceneManager.LoadScene("SampleScene");
        }
    }

    public void FlagSetOne()
    {
        flag = 1;
    }
    public void FlagSetTwo()
    {
        flag = 2;
    }
}
