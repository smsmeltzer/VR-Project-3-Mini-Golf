using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using Photon.Pun;
using UnityEngine.Animations;

public class MeshDeactivate : MonoBehaviourPunCallbacks
{
    private PhotonView view;
    private GameObject body;
    private GameObject helmet;
    private GameObject antenna;
    void Start()
    {
        view = GetComponent<PhotonView>();
        body = transform.GetChild(0).gameObject;
        helmet = body.transform.GetChild(0).gameObject;
        antenna = body.transform.GetChild(1).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (view.IsMine)
        {
            body.GetComponent<MeshRenderer>().enabled = false;
            helmet.GetComponent<MeshRenderer>().enabled = false;
            antenna.GetComponent<MeshRenderer>().enabled = false;
        }
    }
}
