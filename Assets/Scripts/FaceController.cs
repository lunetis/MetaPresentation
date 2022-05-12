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
        anim = GetComponent<Animator> ();
        anim.SetLayerWeight(1, 1);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            anim.CrossFade (animations[0].name, crossFade);
        }
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            anim.CrossFade (animations[1].name, crossFade);
            remainTime = faceShowTime;
        }
        if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            anim.CrossFade (animations[2].name, crossFade);
            remainTime = faceShowTime;
        }
        if(Input.GetKeyDown(KeyCode.Alpha4))
        {
            anim.CrossFade (animations[3].name, crossFade);
            remainTime = faceShowTime;
        }
        if(Input.GetKeyDown(KeyCode.Alpha5))
        {
            anim.CrossFade (animations[4].name, crossFade);
            remainTime = faceShowTime;
        }
        if(Input.GetKeyDown(KeyCode.Alpha6))
        {
            anim.CrossFade (animations[5].name, crossFade);
            remainTime = faceShowTime;
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
