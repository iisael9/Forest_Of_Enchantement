using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public Transform[] spawnPoints; // Array of spawn points
    public PlayerController player; // Reference to the player character

    private void Start()
    {
        // Spawn the player at the initial spawn point (e.g., SpawnPoint1)
        Respawn(0);
    }

    // Call this function to respawn the player at a specific spawn point
    public void Respawn(int spawnPointIndex)
    {
        if (spawnPointIndex >= 0 && spawnPointIndex < spawnPoints.Length)
        {
            player.transform.position = spawnPoints[spawnPointIndex].position;
        }
        else
        {
            Debug.LogError("Invalid spawn point index: " + spawnPointIndex);
        }
    }
}