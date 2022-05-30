using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using lobby;

public class GameSetupController : MonoBehaviour
{
    public float SpawnTime = 1;
    float timer;
    bool HasPlayerSpawned = false;
    public Transform hostSpawnTransform;
    public List<Transform> guestSpawnTransformList;
    int guestCount;
    // Start is called before the first frame update
    void Start()
    {
        CreatePlayer();
        guestCount = 0;

        // If not initialized, use hostSpawnTransform instead.
        if(guestSpawnTransformList == null)
        {
            guestSpawnTransformList = new List<Transform>();
            guestSpawnTransformList.Add(hostSpawnTransform);
        }
    }

    private void CreatePlayer()
    {
        print("Creating Player");
        //PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs","PhotonPlayer"),Vector3.zero,Quaternion.identity);
    }
    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= SpawnTime)
        {
            if (!HasPlayerSpawned)
            {
                if(QuickStartLobbyController.host==1){
                    PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", PresentationDataObject.characterObject.name), hostSpawnTransform.position, hostSpawnTransform.rotation);
                }
                else if(QuickStartLobbyController.guest==1){
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
            timer = 0;

            // Disable after spawn
            this.enabled = false;
        }
    }
}
