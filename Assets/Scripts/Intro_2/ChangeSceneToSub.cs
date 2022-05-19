using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangeSceneToSub : MonoBehaviour
{
    public int flag;

    private void Start()
    {
        flag = 1;
    }

    public void QuitApplication()
    {
        Debug.Log("QUIT !!!");
        Application.Quit();
    }
    public void OnRetry()
    {
        if(flag == 1)
        {
            SceneManager.LoadScene("3DTestScene");
        }
        else if (flag == 2)
        {
            SceneManager.LoadScene("LectureRoom");
        }
        else
        {
            SceneManager.LoadScene("Office");
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
    public void FlagSetThree()
    {
        flag = 3;
    }
}
