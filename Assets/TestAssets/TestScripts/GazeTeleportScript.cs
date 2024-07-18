using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using Photon.Pun;
using Unity.VisualScripting;

public class GazeTeleportScript : MonoBehaviour
{
    [SerializeField] XROrigin myXR;
    [SerializeField] XRController rightController;
    [SerializeField] XRController leftController;
    [SerializeField] XRRayInteractor rayInteractor;
    public InputHelpers.Button button;

    public GameObject tp0;
    public GameObject tp1;
    public GameObject tp2;
    public GameObject tp3;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        rayInteractor.TryGetCurrent3DRaycastHit(out hit);
        Debug.Log(hit.transform.tag);

        bool pressed;
        rightController.inputDevice.IsPressed(button, out pressed);
        Debug.Log(pressed);

        if (hit.transform.tag.Contains("tp") && true)
        {
            // Teleport Player
            if (hit.transform.tag == "tp0")
            {

            }
            else if (hit.transform.tag == "tp1")
            {
            }
            else if (hit.transform.tag == "tp2")
            {
            }
            else if (hit.transform.tag == "tp3")
            {
            }

            // Reset Game/UI
        }
    }

}
