using UnityEngine;

public class BallBehavior : MonoBehaviour
{
    public float maxForce = 50f; // Maximum force to be applied
    public float forceMultiplier = 1f; // Multiplier for adjusting force

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnCollisionEnter(Collision collision)
    {
        // Check if the collision is with the club
        if (collision.gameObject.CompareTag("Club"))
        {
            // Calculate the force based on relative velocity and contact point
            Vector3 relativeVelocity = collision.relativeVelocity;
            Vector3 forceDirection = collision.contacts[0].point - transform.position;
            float forceMagnitude = Mathf.Min(relativeVelocity.magnitude * forceMultiplier, maxForce);

            // Apply force to the golf ball in the calculated direction
            rb.AddForce(forceDirection.normalized * forceMagnitude, ForceMode.Impulse);
        }
    }
}

