using UnityEngine;

public class PlayerGridMovement : MonoBehaviour
{
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;
    public float speed = 5.0f;

    private void Update()
    {
        // Get the player's input for movement
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calculate the new position based on input
        float newX = transform.position.x + horizontalInput * speed * Time.deltaTime;
        float newY = transform.position.y + verticalInput * speed * Time.deltaTime;

        // Clamp the new position within the defined boundaries
        newX = Mathf.Clamp(newX, minX, maxX);
        newY = Mathf.Clamp(newY, minY, maxY);

        // Update the player's position
        transform.position = new Vector3(newX, newY, transform.position.z);
    }
}