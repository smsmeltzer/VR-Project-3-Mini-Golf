using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpeedZoneScript : MonoBehaviour
{
    List<GameObject> gameObjects;
    public float speedForce = 3.0f;

    private void Start()
    {
        gameObjects = new List<GameObject>();
    }
    void FixedUpdate()
    {
        for (int i = 0; i < gameObjects.Count; i++)
        {
            Rigidbody rb = gameObjects[i].GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * speedForce, ForceMode.Force);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ball")
        {
            gameObjects.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Ball")
        {
            gameObjects.Remove(other.gameObject);
        }
    }
}
