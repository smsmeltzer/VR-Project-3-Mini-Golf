using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class HollBehavior : MonoBehaviour
{
    private BallLogisticsManager BLM;

    // Start is called before the first frame update
    void Start()
    {
        GameObject logistics = GameObject.Find("SpawnerLogistics");
        BLM = logistics.GetComponent<BallLogisticsManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            PhotonView myView = collision.gameObject.GetComponent<PhotonView>();
            Debug.Log("Ball collided with hole ID: " + myView.ViewID);

            // Check if collided ball is associated with this instance of the game
            if (BLM.myBall.GetComponent<PhotonView>().ViewID == myView.ViewID)
            {
                BLM.course_finished();
            }

        }
    }
}
