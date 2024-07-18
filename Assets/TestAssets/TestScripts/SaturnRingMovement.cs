using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaturnRingMovement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.Rotate(Vector3.up * 10 * Time.deltaTime, Space.Self);
    }

}
