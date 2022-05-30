using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotFaceController : FaceController
{
    Animator animator;
    Renderer[] characterMaterials;

    public Texture2D[] albedoList;
    [ColorUsage(true,true)]
    public Color[] eyeColors;
    public enum EyePosition { normal, happy, angry, dead}
    public EyePosition eyeState;


    string[] animationNames = {"Normal", "Angry", "Happy", "Scared", "Expect"};

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        characterMaterials = GetComponentsInChildren<Renderer>();
        
        // Auto detect emoteUI
        var emoteUIController = FindObjectOfType<EmoteUIController>();
        emoteUIController?.InitEmoteButtonPanel(this, animationNames);

        var cameraController = FindObjectOfType<PresentationCameraController>();
        cameraController?.SetVCamLookAt(lookCamera.transform);
        
        var presentationController = FindObjectOfType<PresentationController>();
        presentationController.faceController = this;

        EnlistLookCamera();
    }

    public override void PlayFaceAnim(int index)
    {
        switch(index)
        {
        case 0:
            ChangeEyeOffset(EyePosition.normal);
            ChangeAnimatorIdle("normal");
            break;

        case 1:
            ChangeEyeOffset(EyePosition.angry);
            ChangeAnimatorIdle("angry");
            break;

        case 2:
            ChangeEyeOffset(EyePosition.happy);
            ChangeAnimatorIdle("happy");
            break;

        case 3:
            ChangeEyeOffset(EyePosition.dead);
            ChangeAnimatorIdle("dead");
            break;
            
        case 4:
            ChangeEyeOffset(EyePosition.normal);
            ChangeAnimatorIdle("dead");
            break;

        default:
            break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            PlayFaceAnim(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            PlayFaceAnim(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            PlayFaceAnim(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            PlayFaceAnim(3);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            PlayFaceAnim(4);
        }
    }

    void ChangeAnimatorIdle(string trigger)
    {
        animator.SetTrigger(trigger);
    }

    void ChangeMaterialSettings(int index)
    {
        for (int i = 0; i < characterMaterials.Length; i++)
        {
            if (characterMaterials[i].transform.CompareTag("PlayerEyes"))
                characterMaterials[i].material.SetColor("_EmissionColor", eyeColors[index]);
            else
                characterMaterials[i].material.SetTexture("_MainTex",albedoList[index]);
        }
    }

    void ChangeEyeOffset(EyePosition pos)
    {
        Vector2 offset = Vector2.zero;

        switch (pos)
        {
            case EyePosition.normal:
                offset = new Vector2(0, 0);
                break;
            case EyePosition.happy:
                offset = new Vector2(.33f, 0);
                break;
            case EyePosition.angry:
                offset = new Vector2(.66f, 0);
                break;
            case EyePosition.dead:
                offset = new Vector2(.33f, .66f);
                break;
            default:
                break;
        }

        for (int i = 0; i < characterMaterials.Length; i++)
        {
            if (characterMaterials[i].transform.CompareTag("PlayerEyes"))
                characterMaterials[i].material.SetTextureOffset("_MainTex", offset);
        }
    }
}
