using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    public Camera cam;
    public Transform target;

    // Starting position for the parallax game object
    Vector2 startPosition;

    // Start Z value of the parallax game object
    float startZ;

    // Distance that the camera has movede from the start position

    Vector2 moveSinceStart => (Vector2)cam.transform.position - startPosition;

    // Futher the distance, the greater the parallax effect
    float distanceFromSubject => transform.position.z - target.position.z;

    float clippingPlane => (cam.transform.position.z + (distanceFromSubject > 0 ? cam.farClipPlane : cam.nearClipPlane));

    // The futher the object from the player, the greater the parallax factor is. Drag it closer to reduce parallax effect and futher to make it more
    float parallaxFactor => Mathf.Abs(distanceFromSubject) / clippingPlane;

    // Start is called before the first frame update
    void Start()
    {
        // Initial position of the parallax object
        startPosition = transform.position;

        // Unchanging Z position cached at the start of the script
        startZ = transform.position.z;
    }

    void Update()
    {
        // When the target moves, move the parallax object the same distance times a multiplier
        Vector2 newPos = startPosition + moveSinceStart * parallaxFactor;

        // The X/Y position changes based on target travel speed times the parallax factor, but Z stays consistent
        transform.position = new Vector3(newPos.x, newPos.y, startZ);
    }
}
