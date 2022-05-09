using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using SFB;
using System.IO;
using TMPro;
using System.Linq;

public class PresentationData
{
    public bool isVideo;
    public Texture2D slideTexture;
    public string videoUrl;
}

public class PresentationController : MonoBehaviour
{
    [SerializeField]
    MeshRenderer screenRenderer;
    [SerializeField]
    TextMeshProUGUI slideText;

    [SerializeField]
    SlideSettingsUIController slideSettingsUI;

    public VideoPlayer videoPlayer;


    // Don't use preload textures: Load when showing other slides
    string[] slidePaths;

    // Use preload textures: Load when import folder
    List<PresentationData> originalDataList;
    List<PresentationData> presentationDataList;

    int index = 0;
    int maxIndex = -1;

    // Start is called before the first frame update
    void Start()
    {
        slideText.text = "Press \"Open Folder\" to import";
        // videoPlayer = screenRenderer.GetComponent<VideoPlayer>();
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

        slidePaths = GetSlidesFromFolder(path).ToArray<string>();
        Debug.Log(slidePaths.Length + " png/jpg/mp4 files were found.");

        // No images found
        if(slidePaths.Length == 0)
        {
            maxIndex = -1;
            return;
        }

        maxIndex = slidePaths.Length - 1;
        index = 0;

        // Load and store slides
        originalDataList = new List<PresentationData>();
        originalDataList.Clear();

        foreach(var slidePath in slidePaths)
        {
            var data = new PresentationData();
            // Is it video?
            if(slidePath.EndsWith(".mp4") == true)
            {
                Debug.Log("video detected! : " + slidePath);
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

    public void ApplyNewSlideList(List<int> indices)
    {
        presentationDataList = new List<PresentationData>(indices.Count);
        int i = 0;
        foreach(int index in indices)
        {
            Debug.Log("Accessing index " + (index - 1));
            presentationDataList.Add(originalDataList[index - 1]);
        }
        maxIndex = indices.Count - 1;

        // Show First Slide
        index = 0;
        ShowSlide();
    }

    void ShowSlide()
    {
        if(maxIndex == -1)
        {
            return;
        }

        var data = presentationDataList[index];
        if(data.isVideo == true)
        {
            videoPlayer.url = data.videoUrl;
            StartCoroutine(PlayVideo());
        }
        else
        {
            if(videoPlayer.isPlaying == true)
            {
                videoPlayer.Stop();
            }
            screenRenderer.material.mainTexture = data.slideTexture;
        }
        
        slideText.text = string.Format("Slide {0} / {1}", index + 1, maxIndex + 1);
    }

    IEnumerable<string> GetSlidesFromFolder(string folderPath)
    {
        // Filter: png, jpg
        var paths = Directory.EnumerateFiles(folderPath, "*.*", SearchOption.AllDirectories);
        return paths.Where(s => s.EndsWith(".png") || s.EndsWith(".jpg") || s.EndsWith(".mp4"));
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

    

    IEnumerator PlayVideo()
    {
        videoPlayer.Prepare();

        while(videoPlayer.isPrepared == false)
        {
            yield return null;
        }

        videoPlayer.Play();
        Debug.Log("Play start");
    }
}
