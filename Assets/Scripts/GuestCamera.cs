using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GuestCamera : MonoBehaviourPunCallbacks, IPunObservable
{
    PhotonView pv;
    public Camera lookCamera;

    [Range(10, 90)]
    public float maxCameraAngle = 90;
    public float sensitivity = 1;
    public Transform head;
    Vector3 originalHeadEulerAngle;

    float x;
    float y;
    // Start is called before the first frame update
    void Start()
    {
        if(head != null)
        {
            originalHeadEulerAngle = head.eulerAngles;
        }
        pv = GetComponent<PhotonView>();
        if(pv.IsMine == false)
            return;

        if(lookCamera == null)
            return;

        PresentationCameraController camController = FindObjectOfType<PresentationCameraController>();
        camController?.AddCamera(lookCamera, true);

        // Disable all camera object except guest camera
        foreach(Camera camera in Camera.allCameras)
        {
            if(camera != Camera.main)
            {
                camera.gameObject.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        lookCamera.transform.eulerAngles = new Vector3(x, y, 0);

        if(head != null)
        {
            head.eulerAngles = originalHeadEulerAngle + new Vector3(0, y, -x);
        }
        
        if(pv.IsMine == false)
            return;

        // Rotate works while clicking right mouse button
        if(Input.GetMouseButton(1) == false)
            return;

        // Rotation.Y: Left/Right, Rotation.X: Up/Down
        x = lookCamera.transform.eulerAngles.x - Input.GetAxis("Mouse Y") * sensitivity;
        if(x > 180)
        {
            x -= 360;
        }
        x = Mathf.Clamp(x, -maxCameraAngle, maxCameraAngle);

        y = lookCamera.transform.eulerAngles.y + Input.GetAxis("Mouse X") * sensitivity;
        if(y > 180)
        {
            y -= 360;
        }
        y = Mathf.Clamp(y, -maxCameraAngle, maxCameraAngle);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if(stream.IsWriting)
        {
            stream.SendNext(x);
            stream.SendNext(y);
        }
        else
        {
            Debug.Log("OnSerializeView Receive");
            this.x = (float)stream.ReceiveNext();
            this.y = (float)stream.ReceiveNext();
        }
    }
}
