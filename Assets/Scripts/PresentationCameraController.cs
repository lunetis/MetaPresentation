using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // Start is called before the first frame update
    void Start()
    {
        showPresenterWithKeynote = false;
        showKeynote = false;

        ChangeCamera(0);
        subCameraView.SetActive(showPresenterWithKeynote);

        InitCameraButtonPanel();
        uiCamera.enabled = false;
    }

    void InitCameraButtonPanel()
    {
        for(int i = 0; i < cameras.Count; i++)
        {
            GameObject button = Instantiate(cameraButtonPrefab);
            button.transform.SetParent(cameraButtonPanel);
            button.GetComponent<RectTransform>().localScale = Vector3.one;
            button.GetComponent<CameraButton>().Init(this, i + 1, cameras[i].name);
        }
    }


    public void EnableSubKeynoteView()
    {
        uiCamera.enabled = true;
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

    void ToggleSubCameraView()
    {
        if(showKeynote == false)
            return;

        showPresenterWithKeynote = !showPresenterWithKeynote;

        subCameraView.SetActive(showPresenterWithKeynote == true && showKeynote == true);
        currentCamera.targetTexture = (showPresenterWithKeynote == true) ? subCameraRenderTexture : null;
    }

    void ToggleFullKeynoteView()
    {
        showKeynote = !showKeynote;

        // Changing target texture will change view
        uiCamera.targetTexture = (showKeynote == false) ? keynoteTexture : null;
        currentCamera.targetTexture =  (showPresenterWithKeynote == true && showKeynote == true) ? subCameraRenderTexture : null;

        subCameraView.SetActive(showPresenterWithKeynote == true && showKeynote == true);
        subKeynoteView.SetActive(showKeynote == false);
    }

    // Update is called once per frame
    void Update()
    {
        if(isPresenter == true && slideSettingsUIController != null && slideSettingsUIController.gameObject.activeSelf == true)
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
    }
}
