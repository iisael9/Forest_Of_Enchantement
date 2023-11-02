using UnityEngine;

public class FloatingObject : MonoBehaviour
{
    public float floatSpeed = 1.0f; // Adjust this value to control the floating speed
    public float floatAmplitude = 0.2f; // Adjust this value for a smaller floating radius

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        // Calculate the new Y position based on a sine wave
        float newY = startPosition.y + Mathf.Sin(Time.time * floatSpeed) * floatAmplitude;

        // Update the object's position
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}