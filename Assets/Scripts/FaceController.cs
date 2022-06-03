using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceController : MonoBehaviour
{
    public AnimationClip[] animations;

    Animator anim;

    [Tooltip("If not 0, the face will automatically change to idle after faceShowTime seconds.")]
    public float faceShowTime = 0;
    public float crossFade = 0.2f;
    float remainTime = 0;

    public Camera lookCamera;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        anim?.SetLayerWeight(1, 1);

        // Auto detect emoteUI
        var emoteUIController = FindObjectOfType<EmoteUIController>();
        emoteUIController?.InitEmoteButtonPanel(this);

        var cameraController = FindObjectOfType<PresentationCameraController>();
        cameraController?.SetVCamLookAt(lookCamera.transform);
        
        var presentationController = FindObjectOfType<PresentationController>();
        if(presentationController != null)
        {
            presentationController.faceController = this;
        }
        

        // EnlistLookCamera();


        // Unity-Chan only
        if(PresentationController.IsHost() == false)
        {
            Sit();
        }
    }

    void Sit()
    {
        anim.SetLayerWeight(3, 1);
    }

    protected void EnlistLookCamera()
    {
        if(lookCamera == null)
            return;

        PresentationCameraController camController = FindObjectOfType<PresentationCameraController>();
        camController?.AddCamera(lookCamera, true);
    }

    // Index must be 0 - animations.Length - 1
    public virtual void PlayFaceAnim(int index)
    {
        if(index < 0 || index >= animations.Length)
            return;

        anim.CrossFade (animations[index].name, crossFade);
        // Index 0 should be default animation
        if(index != 0)
        {
            remainTime = faceShowTime;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            PlayFaceAnim(0);
        }
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            PlayFaceAnim(1);
        }
        if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            PlayFaceAnim(2);
        }
        if(Input.GetKeyDown(KeyCode.Alpha4))
        {
            PlayFaceAnim(3);
        }
        if(Input.GetKeyDown(KeyCode.Alpha5))
        {
            PlayFaceAnim(4);
        }
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            PlayFaceAnim(5);
        }

        if(faceShowTime > 0 && remainTime > 0)
        {
            remainTime -= Time.deltaTime;

            if(remainTime <= 0)
            {
                remainTime = 0;
                anim.CrossFade (animations[0].name, crossFade);
            }
        }
    }
}
