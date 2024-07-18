using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Unity.VisualScripting;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ClubLogisticsManager : MonoBehaviourPunCallbacks
{
    public int playerCount = 0;
    //public GameObject playerObject;
    private bool clubIsActive = false;
    //public bool collisionFound = false;
    public bool clubSpawnerTag1 = false;
    public bool clubSpawnerTag2 = false;
    public bool clubSpawnerTag3 = false;
    private int currentSpawner = -1;
    public Transform SpawnerLoc1;
    public Transform SpawnerLoc2;
    public Transform SpawnerLoc3;
    public GameObject myClub;
    public BallLogisticsManager BLM;
    [SerializeField] Material[] mats;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
            if (clubSpawnerTag1 && currentSpawner != 0)
            {
                if (!clubIsActive)
                {
                    ActivateClub(0);
                }
                else
                {
                    TeleportClub(0);
                }
                clubSpawnerTag1 = false;
                currentSpawner = 0;
            }
            if (clubSpawnerTag2 && currentSpawner != 1)
            {
                if (!clubIsActive)
                {
                    ActivateClub(1);
                }
                else
                {
                    TeleportClub(1);
                }
                clubSpawnerTag2 = false;
                currentSpawner = 1;
            }
            if (clubSpawnerTag3 && currentSpawner != 2)
            {
                if (!clubIsActive)
                {
                    ActivateClub(2);
                }
                else
                {
                    TeleportClub(2);
                }
                clubSpawnerTag3 = false;
                currentSpawner = 2;
            }
        
        if (myClub != null)
        {
            if (myClub.transform.position.y < -50.0f)
            {
                TeleportClub(currentSpawner);
            }
            
            if (BLM.myBall != null)
            {
                Rigidbody ballRB = BLM.myBall.GetComponent<Rigidbody>();

                if (!ballRB.includeLayers.Contains(myClub.layer))
                {
                    ballRB.includeLayers += myClub.layer;
                }
            }
        }
    }

    public void ActivateClub(int x)
    {
        if (x == 0) { myClub = PhotonNetwork.Instantiate("Putter", SpawnerLoc1.position, SpawnerLoc1.rotation); }
        if (x == 1) { myClub = PhotonNetwork.Instantiate("Putter", SpawnerLoc2.position, SpawnerLoc2.rotation); }
        if (x == 2) { myClub = PhotonNetwork.Instantiate("Putter", SpawnerLoc3.position, SpawnerLoc3.rotation); }
        Debug.Log("Club Spawned" + myClub.GetComponent<PhotonView>().ViewID);
        int layerIndex = LayerMask.NameToLayer("Club"+playerCount);
        myClub.layer = layerIndex;
        myClub.GetComponent<MeshRenderer>().material = mats[(playerCount - 1) % mats.Length];
        myClub.GetComponent<Rigidbody>().includeLayers += myClub.layer;
        BLM.mat = myClub.GetComponent<MeshRenderer>().material;
        BLM.playerLayer = layerIndex;        
        clubIsActive = true;
    }

    public void TeleportClub(int x) 
    {
        if (x == 0) { myClub.transform.position = SpawnerLoc1.position; myClub.transform.rotation = SpawnerLoc1.rotation; }
        if (x == 1) { myClub.transform.position = SpawnerLoc2.position; myClub.transform.rotation = SpawnerLoc2.rotation; }
        if (x == 2) { myClub.transform.position = SpawnerLoc3.position; myClub.transform.rotation = SpawnerLoc3.rotation; }
        
        myClub.GetComponent<Rigidbody>().velocity = Vector3.zero;
        myClub.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        Debug.Log("Club Teleported");
    }

    public void UpdatePlayerCount ()
    {
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("Player");
        playerCount = objectsWithTag.Length/2;
    }

    public void SpawnBall()
    {
        if (currentSpawner == 0) { BLM.spawnerTag1 = true; }
        if (currentSpawner == 1) { BLM.spawnerTag2 = true; }
        if (currentSpawner == 2) { BLM.spawnerTag3 = true; }
    }

    public void UpdateMaterial(int ID, int index)
    {
        PhotonView otherClub = PhotonView.Find(ID);
        otherClub.gameObject.GetComponent<MeshRenderer>().material = mats[index];
    }
}
