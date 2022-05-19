using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickStartLobbyController : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private GameObject quickStartButton;
    [SerializeField]
    private GameObject quickCancelButton;
    [SerializeField]
    private int RoomSize;
    
    // Start is called before the first frame update
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.AutomaticallySyncScene=true;
        quickStartButton.SetActive(true);
    }
    public void QuickStart()
    {
        quickStartButton.SetActive(false);
        quickCancelButton.SetActive(true);
        PhotonNetwork.JoinRandomRoom();
        print("Quick start");
    }
    public override void OnJoinRandomFailed(short returnCode,string message)
    {
        print("Failed to join a room");
        CreateRoom();
    }
    void CreateRoom()
    {
        print("Creating room now");
        int randomRoomNumber=Random.Range(0,10000);
        RoomOptions roomOps = new RoomOptions() { IsVisible = true, IsOpen=true,MaxPlayers=(byte)RoomSize};
        PhotonNetwork.CreateRoom("Room" + randomRoomNumber, roomOps);
        //print("%d",randomRoomNumber);
    }
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        print("Failed to create room... trying again");
        CreateRoom();
    }
    public void QuickCancel()
    {
        quickCancelButton.SetActive(false); 
        quickCancelButton.SetActive(false); 
        PhotonNetwork.LeaveRoom();
    }
}
