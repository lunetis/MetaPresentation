using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SFB;
using System.IO;

public class PresentationController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ImportFolder()
    {
        string[] paths = StandaloneFileBrowser.OpenFolderPanel("Select Folder", "", true);
        string path = "";
        foreach(string str in paths)
        {
            path += str;
        }

        Debug.Log(path);
        GetImageFromFolder(path);
    }

    void GetImageFromFolder(string folderPath)
    {
        var imagesToLoad = Directory.GetFiles(folderPath, "*.png");

        foreach(string imagePath in imagesToLoad)
        {
            Debug.Log(imagePath);
        }
    }
}
