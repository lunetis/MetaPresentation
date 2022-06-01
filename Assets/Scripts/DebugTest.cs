using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class DebugTest : MonoBehaviour
{
    public PresentationController con;
    public Text debugText;

    // Start is called before the first frame update
    void Start()
    {
        //  StartCoroutine(UpdateText());
    }

    // Update is called once per frame
    void Update()
    {
        debugText.text = PhotonNetwork.NetworkClientState.ToString();
        // Debug.Log(PhotonNetwork.NetworkClientState.ToString());
    }

    IEnumerator UpdateText()
    {
        debugText.text = "Trying to check...";
        if(con == null)
        {
            debugText.text = "Con null";
        }
        while(con.isReady == false)
        {
            yield return null;
        }
        if(con.originalDataList == null)
        {
            debugText.text = "originalDataList null";
        }
        else if(con.presentationDataList == null)
        {
            debugText.text = "presentationDataList null";
        }
        else
        {
            debugText.text = con.originalDataList.Count + " // " + con.presentationDataList.Count;
        }
    }
}
