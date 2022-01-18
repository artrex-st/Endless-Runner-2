using UnityEngine;

public class PickUpSpawner : MonoBehaviour
{
    [SerializeField] private PicUp[] PicUpsPrefabOptions;
    [SerializeField, Range(0.01f, 10)] private float PicUpsDistanceZ = 1;
    [SerializeField] private Transform picUpStartSpawn, picUpEndSpawn;

    public void SpawnPicUps(Vector3[] skipPositions)
    {
        Vector3 currentSpawnPosition = picUpStartSpawn.position;
        while (currentSpawnPosition.z < picUpEndSpawn.position.z)
        {
            if (!NeedSkipPosition(currentSpawnPosition, skipPositions))
            {
                int randomPicUp = Random.Range(0, PicUpsPrefabOptions.Length);
                GameObject pickup = PoolingSystem.Instance.GetObject(PicUpsPrefabOptions[randomPicUp].gameObject); 
                pickup.transform.position = currentSpawnPosition;
                pickup.transform.rotation = Quaternion.identity;
                pickup.transform.parent = transform;
            }
            currentSpawnPosition.z += PicUpsDistanceZ;
        }
    }
    public bool NeedSkipPosition(Vector3 currentSpawnPosition, Vector3[] skipsPositions) // check obstacle spot
    {
        foreach (var skipPosition in skipsPositions)
        {
            float skipStart = skipPosition.z - (PicUpsDistanceZ * 0.5f);
            float skipEnd = skipPosition.z + (PicUpsDistanceZ * 0.5f);
            if (currentSpawnPosition.z >= skipStart && currentSpawnPosition.z <= skipEnd)
            {
                return true;
            }
        }
        return false;
    }
    private void OnDrawGizmos() // preview in tracks
    {
        Vector3 currentSpawnPosition = picUpStartSpawn.position;
        if (picUpStartSpawn != null && PicUpsDistanceZ > 0)
        {
            while (currentSpawnPosition.z < picUpEndSpawn.position.z)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawCube(currentSpawnPosition, Vector3.one);
                currentSpawnPosition.z += PicUpsDistanceZ;
            }
        }
    }
}
