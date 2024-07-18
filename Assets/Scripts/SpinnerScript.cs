using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinnerScript : MonoBehaviour
{
    private Transform trans;
    public float rotationSpeed = 2.0f;
    // Start is called before the first frame update
    void Start()
    {
        trans = GetComponent<Transform>();
    }

    void FixedUpdate()
    {
        trans.Rotate(0, rotationSpeed, 0);
    }
}
