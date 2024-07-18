using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class BallLogisticsManager : MonoBehaviourPunCallbacks
{
    private int ballID;
    public GameObject playerObject;
    private bool ballIsActive = false;
    public bool collisionFound = false;
    public bool spawnerTag1 = false;
    public bool spawnerTag2 = false;
    public bool spawnerTag3 = false;
    private int currentSpawner = -1;
    public Transform SpawnerLoc1;
    public Transform SpawnerLoc2;
    public Transform SpawnerLoc3;
    public GameObject myBall;
    public GameObject opponentBall;
    private Transform currentPosition;

    private bool course1done = false;
    private bool course2done = false;
    private bool course3done = false;

    [SerializeField] UIManager myUI;
    [SerializeField] LeaderBoardScript myLeaderBoard;

    public int timesHit = 0;
    private int currentCourse = 0;
    private bool is_playing = true;

    private Vector3 respawnPoint;
    private string[] layerNames = { "OutBounds" };
    public int playerLayer;
    public Material mat;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (!is_playing) { return; }    // player has played all courses

        if (collisionFound)
        {
            // will be used when ball gets hit by its player
            timesHit++;
            myUI.set_num_strokes(timesHit);
            myBall.GetComponent<onCollisionBallScript>().collided = false;

        }
        else
        {
            if (spawnerTag1)
            {
                if (!ballIsActive)
                {
                    ActivateBall(0);
                }
                else if (!course1done)
                {
                    TeleportBall(0);
                }
                spawnerTag1 = false;
                currentSpawner = 0;
            }
            if (spawnerTag2)
            {
                if (!ballIsActive)
                {
                    ActivateBall(1);
                }
                else if (!course2done)
                {
                    TeleportBall(1);
                }
                spawnerTag2 = false;
                currentSpawner = 1;
            }
            if (spawnerTag3)
            {
                if (!ballIsActive)
                {
                    ActivateBall(2);
                }
                else if (!course3done)
                {
                    TeleportBall(2);
                }
                spawnerTag3 = false;
                currentSpawner = 2;
            }
            currentCourse = currentSpawner + 1;
        }

        
        if (myBall != null)
        {
            collisionFound = myBall.GetComponent<onCollisionBallScript>().collided;

            if (myBall.transform.position.y < -50.0f)
            {
                //timesHit++;
                //myUI.set_num_strokes(timesHit);

                // myBall.transform.rotation = currentPosition.rotation;
                // myBall.transform.position = currentPosition.position;
                TeleportBall();
                timesHit++;
                myUI.set_num_strokes(timesHit);
            }

            Rigidbody ballRB = myBall.GetComponent<Rigidbody>();
            if(ballRB.velocity.magnitude < 0.2f && ballRB.velocity.magnitude > 0.05f) 
            {
                
                myBall.GetComponent<Rigidbody>().velocity = Vector3.zero;
                myBall.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                // if (!checkOOB())
                // {
                respawnPoint = myBall.transform.position;
                // }
            }
        }
    }

    public void ActivateBall(int x)
    {
        if (x == 0) { myBall = PhotonNetwork.Instantiate("GolfBall", SpawnerLoc1.position, SpawnerLoc1.rotation); }
        if (x == 1) { myBall = PhotonNetwork.Instantiate("GolfBall", SpawnerLoc2.position, SpawnerLoc2.rotation); }
        if (x == 2) { myBall = PhotonNetwork.Instantiate("GolfBall", SpawnerLoc3.position, SpawnerLoc3.rotation); }
        Debug.Log("Ball Spawned" + myBall.GetComponent<PhotonView>().ViewID);
        // currentPosition.position = myBall.transform.position;
        // currentPosition.rotation = myBall.transform.rotation;
        myBall.GetComponent<MeshRenderer>().material = mat;
        myBall.GetComponent<Rigidbody>().includeLayers += playerLayer;
        ballIsActive = true;
    }

    public void TeleportBall(int x) 
    {
        if (x == 0) { myBall.transform.position = SpawnerLoc1.position; myBall.transform.rotation = SpawnerLoc1.rotation; }
        if (x == 1) { myBall.transform.position = SpawnerLoc2.position; myBall.transform.rotation = SpawnerLoc2.rotation; }
        if (x == 2) { myBall.transform.position = SpawnerLoc3.position; myBall.transform.rotation = SpawnerLoc3.rotation; }
        myBall.GetComponent<Rigidbody>().velocity = Vector3.zero;
        myBall.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        // respawnPoint.position = myBall.transform.position;
        // myBall.SetActive(true);
        // currentPosition.position = myBall.transform.position;
        // currentPosition.rotation = myBall.transform.rotation;
        Debug.Log("Ball Teleported");
    }
    public void course_finished()
    {
        if (currentCourse == 1)
        {
            myUI.set_course_score(1, timesHit);
            course1done = true;
        }
        else if (currentCourse == 2)
        {
            myUI.set_course_score(2, timesHit);
            course2done = true;
        }
        else if (currentCourse == 3)
        {
            myUI.set_course_score(3, timesHit);
            course3done = true;
        }
        timesHit = 0;
        // myBall.SetActive(false);

        is_playing = myUI.is_finished();
        if (is_playing == false)
        {
            System.Object totalScore = (System.Object) myUI.get_total_score();
            photonView.RPC("updateLeaderBoard", RpcTarget.All, totalScore as System.Object);
        }
    }

    public void TeleportBall()
    {
        myBall.transform.position = respawnPoint;
        myBall.GetComponent<Rigidbody>().velocity = Vector3.zero;
        myBall.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        respawnPoint = myBall.transform.position;
        // currentPosition.position = myBall.transform.position;
        // currentPosition.rotation = myBall.transform.rotation;
        Debug.Log("Ball Teleported");
    }

    public bool checkOOB()
    {
        Ray ray = new Ray(myBall.transform.position, Vector3.down);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, (myBall.GetComponent<SphereCollider>().radius * myBall.transform.lossyScale.x) + .01f, LayerMask.GetMask(layerNames)))
        {
            timesHit++;
            TeleportBall(currentSpawner);
            return true;
        }

        return false;
    }

    public void changeCurrCourse(int courseId)
    {
        currentCourse = courseId;
        timesHit = 0;
        // myBall.SetActive(false);
        myUI.set_num_strokes(0);
    }

    [PunRPC]
    void updateLeaderBoard(int score)
    {
        myLeaderBoard.add_player_score(PhotonNetwork.LocalPlayer.ActorNumber, score);
    }
    

}
