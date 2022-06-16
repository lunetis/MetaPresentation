using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class QuitPanel : MonoBehaviourPunCallbacks
{
    public GameObject panel;
    bool leaveEnabled;
    // Start is called before the first frame update
    void Start()
    {
        leaveEnabled = false;
        panel.SetActive(false);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            panel.SetActive(!panel.activeSelf);
        }
    }

    public void OnQuit()
    {
        leaveEnabled = true;
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        if(leaveEnabled == true)
        {
            // 0 must be lobby
            SceneManager.LoadScene(0);
        }
    }
}
