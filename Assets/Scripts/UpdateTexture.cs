using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class UpdateTexture : MonoBehaviour
{
    PhotonView pv;
    MeshRenderer meshRenderer;
    Material mat;
    Texture prevTexture;
 
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        mat = meshRenderer.material;
        pv = GetComponent<PhotonView>();
    }

    void Update()
    {
        if (PhotonNetwork.IsMasterClient && prevTexture != mat.mainTexture)
        {
            Debug.Log("SendTexture...");
            pv.RPC("ReceiveTexture", RpcTarget.Others, ((Texture2D)mat.mainTexture).EncodeToPNG());
        }
        prevTexture = mat.mainTexture;
    }

    [PunRPC]
    void ReceiveTexture(byte[] receivedByte)
    {
        Debug.Log("Receiving texture...");
        var receivedTexture = new Texture2D(1, 1);
        receivedTexture.LoadImage(receivedByte);
        mat.mainTexture = receivedTexture;
    }
}
