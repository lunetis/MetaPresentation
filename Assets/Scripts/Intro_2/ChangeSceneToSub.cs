using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangeSceneToSub : MonoBehaviour
{
    [Space(10)]
    [Header("Note: If the sceneName is empty, LoadScene will use sceneIndex.")]
    public int sceneIndex;

    public int SceneIndex
    {
        get { return sceneIndex; }
        set { sceneIndex = value; }
    }
    public string sceneName;

    private void Start()
    {
        
    }

    public void QuitApplication()
    {
        Debug.Log("QUIT !!!");
        Application.Quit();
    }
    public void OnRetry()
    {
        if(sceneName != "")
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            SceneManager.LoadScene(sceneIndex);
        }
    }
}