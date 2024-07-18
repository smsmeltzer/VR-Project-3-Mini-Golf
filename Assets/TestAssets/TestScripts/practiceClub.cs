using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class practiceClub : MonoBehaviour
{
    public float moveSpeed = 5f; // Adjust this to control the speed of the player

    void Update()
    {
        // Detect arrow key inputs for forward and backward movement
        float verticalInput = Input.GetAxis("Vertical");

        // Calculate movement direction based on input
        Vector3 moveDirection = transform.forward * verticalInput;

        // Move the player object based on input
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);
    }
}
