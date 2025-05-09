using UnityEngine;

public class Spawm : MonoBehaviour
{
    [Header("Spawn Settings")]
    public GameObject itemPrefab; // The item to spawn
    public int itemCount = 10; // Number of items to spawn
    public Vector2 spawnAreaSize = new Vector2(10, 10); // Size of the spawn area
    public float spawnHeight = 1f; // Height at which items will spawn

    [Header("Raycast Settings")]
    public LayerMask groundLayer; // Layer to check for valid spawn positions
    public float raycastHeight = 10f; // Height from which the raycast starts
    public float raycastRange = 20f; // Maximum range of the raycast

    void Start()
    {
        SpawnItems();
    }

    void SpawnItems()
    {
        for (int i = 0; i < itemCount; i++)
        {
            Vector3 randomPosition = GetRandomPosition();
            RaycastHit hit;

            // Perform a raycast to ensure the item spawns on valid ground
            if (Physics.Raycast(randomPosition + Vector3.up * raycastHeight, Vector3.down, out hit, raycastRange, groundLayer))
            {
                Instantiate(itemPrefab, hit.point, Quaternion.identity);
            }
        }
    }

    Vector3 GetRandomPosition()
    {
        float randomX = Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2);
        float randomZ = Random.Range(-spawnAreaSize.y / 2, spawnAreaSize.y / 2);
        return new Vector3(randomX, spawnHeight, randomZ) + transform.position;
    }

    private void OnDrawGizmos()
    {
        // Draw the spawn area in the Scene view for visualization
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, new Vector3(spawnAreaSize.x, 0, spawnAreaSize.y));
    }
}