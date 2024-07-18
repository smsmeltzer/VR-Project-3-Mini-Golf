using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class BallSpawner : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    private bool ballSpawned = false;
    private GameObject myBall;
    public PhotonView playerView;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Player" && !ballSpawned)
        {
            myBall = PhotonNetwork.Instantiate("GolfBall", transform.position + new Vector3(0, 2.0f, 0), transform.rotation);
            playerView = collision.gameObject.GetComponent<PhotonView>();
            ballSpawned = true;
        }
        else if (collision.gameObject.GetComponent<PhotonView>() == playerView)
        {

        }
    }
}
