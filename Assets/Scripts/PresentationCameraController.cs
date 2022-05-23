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

    public GameObject subCameraView;

    Camera currentCamera;

    // Start is called before the first frame update
    void Start()
    {
        showPresenterWithKeynote = false;
        showKeynote = false;

        ChangeCamera(0);
        subCameraView.SetActive(showPresenterWithKeynote);
        uiCamera.enabled = false;
    }

    void ChangeCamera(int index)
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

    // Update is called once per frame
    void Update()
    {
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
            if(showKeynote == false)
                return;

            showPresenterWithKeynote = !showPresenterWithKeynote;

            subCameraView.SetActive(showPresenterWithKeynote);
            currentCamera.targetTexture = (showPresenterWithKeynote == true) ? subCameraRenderTexture : null;
        }

        // Full view
        if(Input.GetKeyDown(KeyCode.P))
        {
            showKeynote = !showKeynote;
            uiCamera.enabled = showKeynote;
            currentCamera.targetTexture =  (showPresenterWithKeynote == true && showKeynote == true) ? subCameraRenderTexture : null;
        }
    }
}
