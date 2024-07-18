using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    public bool collided = false;
    public bool clubSpawnCollision = false;
    public int platformLocation = -1;
    public int clubPlatformLocation = -1;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "BallSpawner1") { collided = true; platformLocation = 0;}
        if (collision.tag == "BallSpawner2") { collided = true; platformLocation = 1;}
        if (collision.tag == "BallSpawner3") { collided = true; platformLocation = 2;}
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "ClubSpawner1") { clubSpawnCollision = true; clubPlatformLocation = 0;}
        if (collision.gameObject.tag == "ClubSpawner2") { clubSpawnCollision = true; clubPlatformLocation = 1;}
        if (collision.gameObject.tag == "ClubSpawner3") { clubSpawnCollision = true; clubPlatformLocation = 2;}
    }
}
