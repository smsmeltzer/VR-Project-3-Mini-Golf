using System.Collections;
using UnityEngine;

public class onCollisionBallScript : MonoBehaviour
{
    public bool collided = false;
    private bool cooldown = false;

    void OnCollisionEnter(Collision collision)
    {
        if (!cooldown && collision.transform.CompareTag("Putter"))
        {
            collided = true;
            StartCoroutine(CooldownCoroutine());
        }
    }


    IEnumerator CooldownCoroutine()
    {
        cooldown = true;
        yield return new WaitForSeconds(1f); // 1 second cooldown
        cooldown = false;
    }
}
