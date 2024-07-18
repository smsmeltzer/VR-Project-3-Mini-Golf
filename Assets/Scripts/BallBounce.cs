using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBounce : MonoBehaviour
{
    Rigidbody rb;
    public string[] layerNames = { "Wall" };
    SphereCollider sc;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        sc = GetComponent<SphereCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(transform.position, rb.velocity);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, (sc.radius * transform.lossyScale.x) + .01f, LayerMask.GetMask(layerNames))) 
        {
            Vector3 incomingVec = hit.point - transform.position;

            // Use the point's normal to calculate the reflection vector.
            Vector3 reflectVec = Vector3.Reflect(incomingVec, hit.normal);

            rb.velocity = reflectVec.normalized * rb.velocity.magnitude;
        }
    }
}
