using UnityEngine;

public class RotateLight : MonoBehaviour
{
    public float rotationSpeed = 10.0f;  // degrees per second

    void Update()
    {
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }
}