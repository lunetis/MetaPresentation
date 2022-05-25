using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using lobby;

public class GameSetupController : MonoBehaviour
{
    public float SpawnTime;
    float timer;
    bool HasPlayerSpawned = false;
    Vector3 VectorSpawnArena=new Vector3(2,2,1);
    // Start is called before the first frame update
    void Start()
    {
        CreatePlayer();
        SpawnTime=1;
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
                    PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs","PhotonPlayer"), VectorSpawnArena, Quaternion.identity);
                }
                else if(QuickStartLobbyController.guest==1){
                    PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs","Cow"), VectorSpawnArena, Quaternion.identity);

                }
                HasPlayerSpawned = true;
            }
            timer = 0;
        }
    }
}
