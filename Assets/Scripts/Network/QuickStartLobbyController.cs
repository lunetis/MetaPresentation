using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace lobby{
    public class QuickStartLobbyController : MonoBehaviourPunCallbacks
    {
        [SerializeField]
        private GameObject quickStartButton;
        [SerializeField]
        private GameObject quickCancelButton;
        [SerializeField]
        private int RoomSize;
        
        public static int host, guest;
        public int map;
        public int[] room = new int[] { 0,0,0,0 };

        // Start is called before the first frame update
        void Start(){
            host=0;
            guest=0;
        }
        public override void OnConnectedToMaster()
        {
            PhotonNetwork.AutomaticallySyncScene=true;
            quickStartButton.SetActive(true);
        }
        public void QuickStart()
        {
            quickStartButton.SetActive(false);
            quickCancelButton.SetActive(true);
            //PhotonNetwork.JoinRandomRoom();
            if (host==1){
                CreateRoom();
                print("Creating room");
            }
            else if(guest==1){
                PhotonNetwork.JoinRoom("Room"+map);
                print("Joining Room"+map);
            }
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
            //int randomRoomNumber=Random.Range(0,10000);
            RoomOptions roomOps = new RoomOptions() { IsVisible = true, IsOpen=true,MaxPlayers=(byte)RoomSize};
            //PhotonNetwork.CreateRoom("Room" + randomRoomNumber, roomOps);
            PhotonNetwork.CreateRoom("Room" + map, roomOps);
            //room[map]=randomRoomNumber;
            //print(randomRoomNumber);
            print("Created Room" + map);
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
        public void SetHost()
        {
            print("Host Set");
            host=1;
        }
        public void SetGuest()
        {
            print("Guest Set");
            guest=1;
        }
        public void SetMap1()
        {
            print("Map1 Set");
            map=1;
        }
        public void SetMap2()
        {
            print("Map2 Set");
            map=2;
        }
        public void SetMap3()
        {
            print("Map3 Set");
            map=3;
        }
    }
}
