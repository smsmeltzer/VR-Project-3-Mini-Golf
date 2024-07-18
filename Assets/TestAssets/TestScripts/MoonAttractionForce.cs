using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonAttractionForce : MonoBehaviour
{
    public float pullRadius = 25;
    public float pullForce = 20;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void FixedUpdate()
    {
        foreach (Collider collider in Physics.OverlapSphere(transform.position, pullRadius))
        {
            Vector3 forceDirection = transform.position - collider.transform.position;

            if (collider.tag == "Ball")
            {
                collider.attachedRigidbody.AddForce(forceDirection.normalized * pullForce, ForceMode.VelocityChange);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
