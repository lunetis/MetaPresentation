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
    int guestCount;
    // Start is called before the first frame update
    void Start()
    {
        // CreatePlayer();
        guestCount = 0;

        // If not initialized, use hostSpawnTransform instead.
        if(guestSpawnTransformList == null)
        {
            guestSpawnTransformList = new List<Transform>();
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
                PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", PresentationDataObject.characterObject.name), hostSpawnTransform.position, hostSpawnTransform.rotation);
            }
            else if(QuickStartLobbyController.guest==1){
                print("Creating Guest...");
                Transform spawnTransform;
                if(guestCount > guestSpawnTransformList.Count)
                {
                    Debug.LogWarning("Not enough guest spawn transforms!");
                    spawnTransform = guestSpawnTransformList[guestSpawnTransformList.Count - 1];
                }
                else
                {
                    spawnTransform = guestSpawnTransformList[guestCount];
                }
                PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs","Cow"), spawnTransform.position, spawnTransform.rotation);
                guestCount++;
            }
            HasPlayerSpawned = true;
        }
    }
}
