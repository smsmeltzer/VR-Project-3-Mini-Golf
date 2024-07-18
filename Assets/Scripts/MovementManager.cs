using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using Photon.Pun;
using Unity.XR.CoreUtils;
using UnityEditor.XR.LegacyInputHelpers;
using UnityEngine.XR.Interaction.Toolkit;
using Unity.VisualScripting;

public class MovementManager : MonoBehaviour
{
    private CollisionDetection col;
    private BallLogisticsManager BLM;
    private ClubLogisticsManager CLM;

    private PhotonView view;
    private GameObject child;
    private float xInput;
    private float yInput;
    private float movementSpeed = 5.0f;
    private float jumpSpeed = 10.0f;
    private bool grounded = false;
    private Vector3 moveAmount;
    private Vector3 smoothMoveVelocity;

    private InputData inputData;
    private Rigidbody rb;
    private Transform XRrig;


    private XRController rightController;
    private XRController leftController;
    private XRRayInteractor rayInteractor;
    private GameObject leftRayInteractor;
    private GameObject rightRayInteractor;

    public InputHelpers.Button button;

    public GameObject tp0;
    public GameObject tp1;
    public GameObject tp2;
    public GameObject tp3;

    private string[] layerNames = { "Ground", "Wall", "OutBounds" };

    private Camera cam;

    private bool setPlayer = false;


    // Start is called before the first frame update
    void Start()
    {
        view = GetComponent<PhotonView>();

        child = transform.GetChild(0).gameObject;
        rb = child.GetComponent<Rigidbody>();
        GameObject XrOrigin = GameObject.Find("XR Origin");

        XRrig = XrOrigin.transform;
        inputData = XrOrigin.GetComponent<InputData>();

        rightController = XrOrigin.transform.Find("Camera Offset").Find("Right Controller").GetComponent<XRController>();
        leftController = XrOrigin.transform.Find("Camera Offset").Find("Left Controller").GetComponent<XRController>();
        rayInteractor = XrOrigin.transform.Find("Camera Offset").Find("Main Camera").GetComponent<XRRayInteractor>();
        leftRayInteractor = XrOrigin.transform.Find("Camera Offset").Find("Left Controller").Find("Ray Interactor").gameObject;
        rightRayInteractor = XrOrigin.transform.Find("Camera Offset").Find("Right Controller").Find("Ray Interactor").gameObject;

        tp0 = GameObject.Find("tp0");
        tp1 = GameObject.Find("tp1");
        tp2 = GameObject.Find("tp2");
        tp3 = GameObject.Find("tp3");

        cam = Camera.main;

        GameObject logistics = GameObject.Find("SpawnerLogistics");
        BLM = logistics.GetComponent<BallLogisticsManager>();
        col = child.GetComponent<CollisionDetection>();

        GameObject clubLogistics = GameObject.Find("ClubLogistics");
        CLM = clubLogistics.GetComponent<ClubLogisticsManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (view.IsMine)
        {
            if (setPlayer == false)
            {
                //BLM.playerObject = this.gameObject;
                setPlayer = true;
                CLM.playerCount = view.ViewID / 1000;
                // CLM.UpdatePlayerCount();
                // view.RPC("IncrementPlayerCount", RpcTarget.Others);
            }
            XRrig.position = child.transform.position;
            // Rotate the player to face the direction the user is looking
            Vector3 lookDirection = cam.transform.forward;
            lookDirection.y = 0; // Keep the player upright
            Quaternion rot = Quaternion.LookRotation(lookDirection);
            child.transform.rotation = rot;

            if (inputData.rightController.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 movement))
            {
                Vector3 moveDir = new Vector3(movement.x, 0, movement.y).normalized;
                Vector3 targetMoveAmount = moveDir * movementSpeed;
                moveAmount = Vector3.SmoothDamp(moveAmount, targetMoveAmount, ref smoothMoveVelocity, .15f);
            }

            if (inputData.rightController.TryGetFeatureValue(CommonUsages.primaryButton, out bool pressed))
            {
                if (pressed)
                {
                    Ray ray = new Ray(child.transform.position, -child.transform.up);
                    RaycastHit hit;

                    if (Physics.Raycast(ray, out hit, 1 + .1f, LayerMask.GetMask(layerNames)))
                    {
                        grounded = true;
                    }
                    else
                    {
                        grounded = false;
                    }
                }
            }

            if (col != null)
            {
                // if (col.collided)
                // {
                //     col.collided = false;
                //     if (col.platformLocation == 0) { BLM.spawnerTag1 = true; }
                //     if (col.platformLocation == 1) { BLM.spawnerTag2 = true; }
                //     if (col.platformLocation == 2) { BLM.spawnerTag3 = true; }
                // }
                if (col.clubSpawnCollision)
                {
                    col.clubSpawnCollision = false;
                    if (col.clubPlatformLocation == 0) { CLM.clubSpawnerTag1 = true; }
                    if (col.clubPlatformLocation == 1) { CLM.clubSpawnerTag2 = true; }
                    if (col.clubPlatformLocation == 2) { CLM.clubSpawnerTag3 = true; }
                }
            }


            // Teleportation Stuff
            RaycastHit hit2;
            rayInteractor.TryGetCurrent3DRaycastHit(out hit2);
            pressed = false;

            // check what is non-dominant hand
            if (leftRayInteractor.activeSelf)
            {
                rightController.inputDevice.IsPressed(button, out pressed);

            }
            else if (rightRayInteractor.activeSelf)
            {
                leftController.inputDevice.IsPressed(button, out pressed);
            }


            if (hit2.transform.tag.Contains("tp") && pressed)
            {
                // Teleport Player
                if (hit2.transform.tag == "tp0")
                {
                    rb.position = tp0.transform.position;
                }
                else if (hit2.transform.tag == "tp1")
                {
                    rb.position = tp1.transform.position;
                    CLM.clubSpawnerTag1 = true;
                }
                else if (hit2.transform.tag == "tp2")
                {
                    rb.position = tp2.transform.position;
                    CLM.clubSpawnerTag2 = true;

                }
                else if (hit2.transform.tag == "tp3")
                {
                    rb.position = tp3.transform.position;
                    CLM.clubSpawnerTag3 = true;
                    
                }
            }
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + child.transform.TransformVector(moveAmount) * Time.fixedDeltaTime);
        if (grounded)
        {
            rb.AddForce(0, jumpSpeed, 0, ForceMode.Impulse);
            grounded = false;
        }
    }
}
