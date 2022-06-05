using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;
using lobby;

public class TitleText : MonoBehaviourPunCallbacks
{
    public TextMeshProUGUI titleText;
    public PhotonView pv;

    // Start is called before the first frame update
    void Start()
    {
        pv = GetComponent<PhotonView>();
        if(titleText == null)
        {
            Debug.Log("TitleText Null");
            titleText = GetComponent<TextMeshProUGUI>();
        }
        titleText.text = "";
        if(pv == null)
        {
            pv = GetComponent<PhotonView>();
        }
        if(PresentationController.IsHost() == false)
        {
            StartCoroutine(RequestTextData());
            return ;
        }
        // Clear at init
    }
    IEnumerator RequestTextData()
    {
        while(PhotonNetwork.NetworkClientState != ClientState.Joined)
        {
            yield return null;
        }
        pv.RPC("SendTextToClient", RpcTarget.MasterClient);
    }    

    [PunRPC]
    public void SendTextToClient()
    {
        if(IsHost() == true)
        {
            Debug.Log("Send2");
            pv.RPC("ReceiveText", RpcTarget.Others, titleText.text);
        }
    }

    [PunRPC]
    void ReceiveText(string text)
    {
        Debug.Log("Received"+text+"a");
        titleText.text=text;
    }
    public static bool IsHost()
    {
        return QuickStartLobbyController.host == 1;
    }

    public void RText(string text){
            pv.RPC("ReceiveText", RpcTarget.All, text);
    }
}
