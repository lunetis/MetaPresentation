using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using SFB;
using System.IO;
using TMPro;
using System.Linq;

public class Folder_Controller : MonoBehaviour
{
    [SerializeField]
    UI_Controller slideSettingsUI;

    string[] slidePaths;

    List<PresentationData> originalDataList;
    List<PresentationData> presentationDataList;

    int index = 0;
    int maxIndex = -1;

    public HostSettingsController hostSettingsController;
    public GameObject slidePreviewButton;
    public GameObject slidePreviewUI;


    public void ImportFolder()
    {
        string[] paths = StandaloneFileBrowser.OpenFolderPanel("Select Folder", "", true);
        string path = "";
        foreach (string str in paths)
        {
            path += str;
        }

        // Not selected (ex. cancel)
        if (path == "")
        {
            return;
        }

        slidePaths = PresentationController.GetSlidesFromFolder(path).ToArray<string>();
        Debug.Log(slidePaths.Length + " png/jpg/mp4 files were found.");

        // No images found
        if (slidePaths.Length == 0)
        {
            maxIndex = -1;
            return;
        }

        maxIndex = slidePaths.Length - 1;
        index = 0;

        // Load and store slides
        originalDataList = new List<PresentationData>();
        originalDataList.Clear();

        foreach (var slidePath in slidePaths)
        {
            var data = new PresentationData();
            // Is it video?
            if (slidePath.EndsWith(".mp4") == true)
            {
                data.isVideo = true;
                data.videoUrl = slidePath;
            }
            // Else
            else
            {
                data.isVideo = false;
                data.slideTexture = GetTextureFromImage(slidePath);
            }

            originalDataList.Add(data);
        }

        // Go to slide settings
        slideSettingsUI.Init(originalDataList);

        PresentationDataObject.data = originalDataList;
        hostSettingsController.HasSelectedFolder = true;
        slidePreviewButton?.SetActive(true);
    }


    Texture2D GetTextureFromImage(string imagePath)
    {
        Texture2D texture;
        if (File.Exists(imagePath))
        {
            texture = new Texture2D(2, 2);
            texture.LoadImage(File.ReadAllBytes(imagePath));
            return texture;
        }
        else
        {
            return null;
        }
    }

    public void OnPreviewClick()
    {
        slidePreviewUI.SetActive(!slidePreviewUI.activeSelf);
    }
}
