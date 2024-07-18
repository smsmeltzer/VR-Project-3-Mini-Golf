using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonVRMovement : MonoBehaviour
{
    private CollisionDetection col;
    private LogisticsManager LM;

    private PhotonView view;
    private GameObject child;
    private Rigidbody rb;

    private float xInput;
    private float yInput;
    private float moveSpeed = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        view = GetComponent<PhotonView>();
        child = transform.GetChild(0).gameObject;
        rb = child.GetComponent<Rigidbody>();
        col = child.GetComponent<CollisionDetection>();

        GameObject logistics = GameObject.Find("LogisticsManager");
        LM = logistics.GetComponent<LogisticsManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (view.IsMine)
        {
            xInput = Input.GetAxis("Horizontal");
            yInput = Input.GetAxis("Vertical");

            if (col.collided)
            {
                col.collided = false;
                LM.collisionFound = true;
            }

            if (LM.lost)
            {
                view.RPC("communicateLoss", RpcTarget.Others);
                LM.resetGameplay();
            }
        }
    }

    private void FixedUpdate()
    {
        rb.AddForce(xInput * moveSpeed, 0, yInput * moveSpeed);
    }

    [PunRPC]
    void communicateLoss()
    {
        LM.otherLost();
    }
}
