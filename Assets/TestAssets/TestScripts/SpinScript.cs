using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SpinScript : MonoBehaviour
{
    public int speed = 10;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, Mathf.Sin(Time.deltaTime) * speed, 0));
        
    }
}
