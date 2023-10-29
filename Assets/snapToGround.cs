using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class snapToGround : MonoBehaviour
{
    public float displacement = 1.0f; // Public variable to adjust the distance from the nearest object below
    public float smoothTime = 0.2f;   // Smoothing time for smooth transition

    private Rigidbody rb;
    private Vector3 currentVelocity;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            // Calculate the target position
            Vector3 targetPosition = hit.point + Vector3.up * displacement;

            // Move the GameObject to the target position smoothly
            rb.MovePosition(Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, smoothTime));
        }
    }
}