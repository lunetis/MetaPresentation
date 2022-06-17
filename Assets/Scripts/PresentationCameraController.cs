using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PresentationCameraController : MonoBehaviour
{
    public RenderTexture subCameraRenderTexture;
    public List<Camera> cameras;

    bool showKeynote = false;
    bool showPresenterWithKeynote = false;
    public Camera uiCamera;
    public RenderTexture keynoteTexture;

    public GameObject subCameraView;
    public GameObject subKeynoteView;

    Camera currentCamera;

    public GameObject cameraButtonPrefab;
    public Transform cameraButtonPanel;

    public bool isPresenter = false;
    public SlideSettingsUIController slideSettingsUIController;

    public CinemachineVirtualCamera vCam;

    public GameObject keynoteCameraViewButton;
    int cameraCount;
    
    bool isVisibleCanvas = true;

    // Start is called before the first frame update
    void Awake()
    {
        uiCamera.enabled = false;
    }
    void Start()
    {
        showPresenterWithKeynote = false;
        showKeynote = false;

        ChangeCamera(0);
        subCameraView.SetActive(showPresenterWithKeynote);

        InitCameraButtonPanel();
    }

    void InitCameraButtonPanel()
    {
        cameraCount = 0;
        for(int i = 0; i < cameras.Count; i++)
        {
            cameraCount++;
            AddCameraButton(cameras[i]);
        }
    }

    public void AddCamera(Camera camera, bool addToList = false)
    {
        if(addToList == true)
        {
            cameras.Add(camera);
        }
        cameraCount++;
        AddCameraButton(camera);
    }

    void AddCameraButton(Camera camera)
    {
        GameObject button = Instantiate(cameraButtonPrefab);
        button.transform.SetParent(cameraButtonPanel);
        button.GetComponent<RectTransform>().localScale = Vector3.one;
        button.GetComponent<CameraButton>().Init(this, cameraCount, camera.name);
    }

    public void SetVCamLookAt(Transform t)
    {
        vCam.LookAt = t;
    }


    public void EnableSubKeynoteView()
    {
        uiCamera.enabled = true;
    }
    
    void SetUIVisible(bool visible)
    {
        var canvases = FindObjectsOfType<Canvas>();
        foreach(Canvas canvas in canvases)
        {
            // If the renderMode is ScreenSpaceCamera, this canvas is Keynote view canvas. (Must be always enabled)
            if(canvas.renderMode == RenderMode.ScreenSpaceCamera)
                continue;

            canvas.enabled = visible;
        }
    }



    public void ChangeCamera(int index)
    {
        if(index >= cameras.Count)
            return;

        for(int i = 0; i < cameras.Count; i++)
        {
            cameras[i].gameObject.SetActive(i == index);
            cameras[i].targetTexture = null;
        }

        currentCamera = cameras[index];
        currentCamera.targetTexture = (showPresenterWithKeynote == true && showKeynote == true) ? subCameraRenderTexture : null;
    }

    public void ToggleSubCameraView()
    {
        if(showKeynote == false)
            return;

        showPresenterWithKeynote = !showPresenterWithKeynote;

        subCameraView.SetActive(showPresenterWithKeynote == true && showKeynote == true);
        
        currentCamera.targetTexture = (showPresenterWithKeynote == true) ? subCameraRenderTexture : null;
    }

    public void ToggleFullKeynoteView()
    {
        showKeynote = !showKeynote;

        // Changing target texture will change view
        //uiCamera.enabled = showKeynote;
        uiCamera.targetTexture = (showKeynote == false) ? keynoteTexture : null;
        currentCamera.targetTexture =  (showPresenterWithKeynote == true && showKeynote == true) ? subCameraRenderTexture : null;

        subCameraView.SetActive(showPresenterWithKeynote == true && showKeynote == true);
        subKeynoteView.SetActive(showKeynote == false);

        keynoteCameraViewButton.SetActive(showKeynote);
    }

    // Update is called once per frame
    void Update()
    {
        // Do not change camera when settings panel is enabled
        if(slideSettingsUIController.gameObject.activeSelf == true)
        {
            return;
        }

        // Camera Switch
        if(Input.GetKeyDown(KeyCode.F1))
        {
            ChangeCamera(0);
        }
        if(Input.GetKeyDown(KeyCode.F2))
        {
            ChangeCamera(1);
        }
        if(Input.GetKeyDown(KeyCode.F3))
        {
            ChangeCamera(2);
        }
        if(Input.GetKeyDown(KeyCode.F4))
        {
            ChangeCamera(3);
        }
        if(Input.GetKeyDown(KeyCode.F5))
        {
            ChangeCamera(4);
        }

        // Sub camera view
        if(Input.GetKeyDown(KeyCode.O))
        {
            ToggleSubCameraView();
        }

        // Full view
        if(Input.GetKeyDown(KeyCode.P))
        {
            ToggleFullKeynoteView();
        }
        

        if(Input.GetKeyDown(KeyCode.Backspace))
        {
            isVisibleCanvas = !isVisibleCanvas;
            SetUIVisible(isVisibleCanvas);
        }


        // Mute voice
        if(Input.GetKeyDown(KeyCode.M) && PresentationController.IsHost() == true)
        {
            var voiceNetwork = FindObjectOfType<Photon.Voice.PUN.PhotonVoiceNetwork>();
            var audioSources = FindObjectsOfType<AudioSource>();
            if(voiceNetwork != null)
            {
                bool enableStatus = !voiceNetwork.enabled;
                voiceNetwork.enabled = enableStatus;
                
                foreach(var audioSource in audioSources)
                {
                    audioSource.enabled = enableStatus;
                }
            }
        }
    }
}
