using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class QuitPanel : MonoBehaviourPunCallbacks
{
    public GameObject panel;
    bool leaveEnabled;
    bool forceQuit;
    
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

        forceQuit = Input.GetKey(KeyCode.LeftControl);
    }

    public void OnQuit()
    {
        leaveEnabled = true;
        PhotonNetwork.LeaveRoom();

        // Force quit when pressing left ctrl
        if(forceQuit == true)
        {
            Debug.Log("Force Quit");
            SceneManager.LoadScene(0);
        }
    }

    public override void OnLeftRoom()
    {
        // Prevent quit by network disconnect error
        if(leaveEnabled == true)
        {
            // 0 must be lobby
            SceneManager.LoadScene(0);
        }
    }
}
