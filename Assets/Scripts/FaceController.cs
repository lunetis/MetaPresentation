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
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetLayerWeight(1, 1);

        // Auto detect emoteUI
        var emoteUIController = FindObjectOfType<EmoteUIController>();
        emoteUIController?.InitEmoteButtonPanel(this);
    }

    public void PlayFaceAnim(int index)
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
