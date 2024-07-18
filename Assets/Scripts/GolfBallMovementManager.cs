using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolfBallMovementManager : MonoBehaviour
{

    private CollisionDetection col;//
    private BallLogisticsManager BLM;//
    private ClubLogisticsManager CLM;

    private PhotonView view;
    private GameObject child;
    private Rigidbody rb;

    private float xInput;
    private float yInput;
    private float moveSpeed = 10.0f;
    private bool setPlayer = false;

    // Start is called before the first frame update
    void Start()
    {
        view = GetComponent<PhotonView>();
        child = transform.GetChild(0).gameObject;
        rb = child.GetComponent<Rigidbody>();

        GameObject logistics = GameObject.Find("SpawnerLogistics");
        BLM = logistics.GetComponent<BallLogisticsManager>();
        col = child.GetComponent<CollisionDetection>();

        //
        GameObject clubLogistics = GameObject.Find("ClubLogistics");
        CLM = clubLogistics.GetComponent<ClubLogisticsManager>();
        //
    }

    // Update is called once per frame
    void Update()
    {
        if (view.IsMine)
        {
            if (setPlayer == false)
            {
                BLM.playerObject = this.gameObject;
                setPlayer = true;
                CLM.playerCount = view.ViewID / 1000;
                // CLM.UpdatePlayerCount();
                // view.RPC("IncrementPlayerCount", RpcTarget.Others);
            }
            xInput = Input.GetAxis("Horizontal");
            yInput = Input.GetAxis("Vertical");

            if (col != null)
            {
                if (col.collided )
                {
                    col.collided = false;
                    if (col.platformLocation == 0){ BLM.spawnerTag1 = true; }
                    if (col.platformLocation == 1){ BLM.spawnerTag2 = true; }
                    if (col.platformLocation == 2){ BLM.spawnerTag3 = true; }
                }
                if (col.clubSpawnCollision)
                {
                    col.clubSpawnCollision = false;
                    if (col.clubPlatformLocation == 0){ CLM.clubSpawnerTag1 = true; }
                    if (col.clubPlatformLocation == 1){ CLM.clubSpawnerTag2 = true; }
                    if (col.clubPlatformLocation == 2){ CLM.clubSpawnerTag3 = true; }
                }

            }
        }
    }

    private void FixedUpdate()
    {
        rb.AddForce(xInput * moveSpeed, 0, yInput * moveSpeed);
    }

    [PunRPC]
    private void IncrementPlayerCount ()
    {
        CLM.UpdatePlayerCount();
    }
}
