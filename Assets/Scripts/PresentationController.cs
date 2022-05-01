using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SFB;
using System.IO;
using TMPro;
using System.Linq;

public class PresentationController : MonoBehaviour
{
    [SerializeField]
    MeshRenderer meshRenderer;
    [SerializeField]
    TextMeshProUGUI slideText;

    [SerializeField]    
    bool preloadTextures = false;

    // Don't use preload textures: Load when showing other slides
    string[] imagePaths;
    // Use preload textures: Load when import folder
    Texture2D[] textures;

    int index = 0;
    int maxIndex = -1;

    // Start is called before the first frame update
    void Start()
    {
        slideText.text = "Press \"Open Folder\" to import";
    }


    public void ImportFolder()
    {
        string[] paths = StandaloneFileBrowser.OpenFolderPanel("Select Folder", "", true);
        string path = "";
        foreach(string str in paths)
        {
            path += str;
        }

        // Not selected (ex. cancel)
        if(path == "")
        {
            return;
        }

        imagePaths = GetImagesFromFolder(path).ToArray<string>();
        Debug.Log(imagePaths.Length + " png images were found.");

        // No images found
        if(imagePaths.Length == 0)
        {
            maxIndex = -1;
            return;
        }

        maxIndex = imagePaths.Length - 1;
        index = 0;

        if(preloadTextures == true)
        {
            textures = new Texture2D[maxIndex + 1];
            for(int i = 0; i < imagePaths.Length; i++)
            {
                textures[i] = GetTextureFromImage(imagePaths[i]);
            }
        }

        ShowSlide();
    }

    public void ShowNextSlide()
    {
        index = Mathf.Clamp(index + 1, 0, maxIndex);
        ShowSlide();
    }

    public void ShowPrevSlide()
    {
        index = Mathf.Clamp(index - 1, 0, maxIndex);
        ShowSlide();
    }

    void ShowSlide()
    {
        if(maxIndex == -1)
        {
            return;
        }

        if(preloadTextures == true)
        {
            meshRenderer.material.mainTexture = textures[index];
        }
        else
        {
            meshRenderer.material.mainTexture = GetTextureFromImage(imagePaths[index]);
        }
        slideText.text = string.Format("Slide {0} / {1}", index + 1, maxIndex + 1);
    }

    IEnumerable<string> GetImagesFromFolder(string folderPath)
    {
        // Filter: png, jpg
        var paths = Directory.EnumerateFiles(folderPath, "*.*", SearchOption.AllDirectories);
        return paths.Where(s => s.EndsWith(".png") || s.EndsWith(".jpg"));
    }

    Texture2D GetTextureFromImage(string imagePath)
    {
        Texture2D texture;
        if(File.Exists(imagePath))
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
}
