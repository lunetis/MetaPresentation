using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using lobby;

public class GameSetupController : MonoBehaviour
{
    bool HasPlayerSpawned = false;
    public Transform hostSpawnTransform;
    public List<Transform> guestSpawnTransformList;
    // Start is called before the first frame update
    void Start()
    {
        // CreatePlayer();

        // If not initialized, use hostSpawnTransform instead.
        if(guestSpawnTransformList == null)
        {
            guestSpawnTransformList = new List<Transform>();
        }

        if(guestSpawnTransformList.Count == 0)
        {
            guestSpawnTransformList.Add(hostSpawnTransform);
        }

        StartCoroutine(CreatePlayerCoroutine());
    }
    

    IEnumerator CreatePlayerCoroutine()
    {
        while(PhotonNetwork.NetworkClientState != ClientState.Joined)
        {
            yield return null;
        }

        CreatePlayer();
    }

    private void CreatePlayer()
    {
        if (!HasPlayerSpawned)
        {
            if(QuickStartLobbyController.host==1){
                print("Creating Host...");
                PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", PresentationDataObject.hostObject.name), hostSpawnTransform.position, hostSpawnTransform.rotation);
            }
            else if(QuickStartLobbyController.guest==1){
                print("Creating Guest...");
                Transform spawnTransform;

                /*if(guestCount > guestSpawnTransformList.Count || guestSpawnTransformList.Count == 0)
                {
                    Debug.LogWarning("Not enough guest spawn transforms!");
                    spawnTransform = guestSpawnTransformList[0];
                }
                else
                {
                    spawnTransform = guestSpawnTransformList[guestCount];
                }*/
                spawnTransform = guestSpawnTransformList[PhotonNetwork.CurrentRoom.PlayerCount-2];
                Debug.Log(PhotonNetwork.CurrentRoom.PlayerCount);
                Debug.Log(spawnTransform.position);
                PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs","Cow"), spawnTransform.position, spawnTransform.rotation);

            }
            HasPlayerSpawned = true;
        }
    }
}
