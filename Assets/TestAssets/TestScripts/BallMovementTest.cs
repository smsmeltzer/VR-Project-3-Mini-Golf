using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovementTest : MonoBehaviour
{
    private Vector3 myStartPosition;

    public Camera playerCamera;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 45.0f;
    float rotationX = 0;
    // Start is called before the first frame update
    void Start()
    {
        myStartPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
        rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);

        float moveSpeed = 0;
        if (Input.GetKey("left shift"))
        {
            moveSpeed = 15;
        }
        else
        {
            moveSpeed = 10;
        }

        if (Input.GetKeyDown("space"))
        {
            GetComponent<Rigidbody>().velocity = new Vector3(GetComponent<Rigidbody>().velocity.x, 10, GetComponent<Rigidbody>().velocity.z);
        }

        if (Input.GetKey("up") || Input.GetKey("w"))
        {
            transform.position += Camera.main.transform.forward * moveSpeed * Time.deltaTime;
        }

        if (Input.GetKey("down") || Input.GetKey("s"))
        {
            transform.position -= Camera.main.transform.forward * moveSpeed * Time.deltaTime;
        }

        if (Input.GetKey("r") || (transform.position.y < -50))
        {
            transform.position = myStartPosition;
            GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        }

    }
}
