using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onCollisionApplyForce : MonoBehaviour
{
    public Vector3 direction;
    public int force_strength = 200;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            collision.gameObject.GetComponent<Rigidbody>().AddForce(direction.normalized * force_strength, ForceMode.Force);
        }
    }
}
