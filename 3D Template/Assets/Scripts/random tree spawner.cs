using UnityEngine;

[RequireComponent (typeof(SphereCollider))]
public class randomtreespawner : MonoBehaviour
{
    [Header ("Spawn Settings")]
    public GameObject resourcePrefab;
    public float spawnChance;

    [Header("Raycast Settings")]
    [Range(min: 5, max: 100)] public float distanceBetweenCheck;
    public float heightOfCheck = 10f, rangeOfCheck = 30f;
    public LayerMask layerMask;
    public Vector2 Size;

    public bool _hasSpawned = false;

    void SpawnResources()
    {
        if (_hasSpawned)
        {
            return;
        }

        for(float x = 0; x < Size.x; x += distanceBetweenCheck)
        {
            for (float z = 0; z < Size.y; z += distanceBetweenCheck)
            {
                RaycastHit hit;
                
                if(Physics.Raycast(transform.position + new Vector3(x, heightOfCheck, z), Vector3.down, out hit, rangeOfCheck, layerMask))
                {
                    Instantiate(resourcePrefab, hit.point, Quaternion.Euler(new Vector3(0, Random.Range(0, 360), 0)), transform);
                    print(resourcePrefab.gameObject.name);
                }
            }
        }

        _hasSpawned = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        print(other.gameObject.name);
        SpawnResources();
    }

    private void OnDrawGizmos()
    {
        Vector3 center = transform.position + ((Vector3)Size / 2);
        center.y = heightOfCheck;

        Vector3 size = Size;
        size.z = size.y;
        size.y = rangeOfCheck;

        Gizmos.DrawWireCube(center, size);

        for (float x = 0; x < Size.x; x += distanceBetweenCheck)
        {
            for (float z = 0; z < Size.y; z += distanceBetweenCheck)
            {
                Gizmos.DrawWireSphere(transform.position + new Vector3(x, heightOfCheck, z), 1);
            }
        }
    }
}
