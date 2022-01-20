using UnityEngine;

public sealed class PickUpSpawner : MonoBehaviour
{
    [SerializeField] private PicUp[] _picUpsPrefabOptions;
    [SerializeField, Range(0.01f, 10)] private float _picUpsDistanceZ = 1;
    [SerializeField] private Transform _picUpStartSpawn, _picUpEndSpawn;

    public PickUpSpawner(PicUp[] picUpsPrefabOptions, float picUpsDistanceZ, Transform picUpStartSpawn, Transform picUpEndSpawn)
    {
        _picUpsPrefabOptions = picUpsPrefabOptions;
        _picUpsDistanceZ = picUpsDistanceZ;
        _picUpStartSpawn = picUpStartSpawn;
        _picUpEndSpawn = picUpEndSpawn;
    }

    public void SpawnPicUps(Vector3[] skipPositions)
    {
        Vector3 currentSpawnPosition = _picUpStartSpawn.position;
        while (currentSpawnPosition.z < _picUpEndSpawn.position.z)
        {
            if (!NeedSkipPosition(currentSpawnPosition, skipPositions))
            {
                int randomPicUp = Random.Range(0, _picUpsPrefabOptions.Length);
                GameObject pickup = PoolingSystem.Instance.GetObject(_picUpsPrefabOptions[randomPicUp].gameObject); 
                pickup.transform.position = currentSpawnPosition;
                pickup.transform.rotation = Quaternion.identity;
                pickup.transform.parent = transform;
            }
            currentSpawnPosition.z += _picUpsDistanceZ;
        }
    }
    public bool NeedSkipPosition(Vector3 currentSpawnPosition, Vector3[] skipsPositions) // check obstacle spot
    {
        foreach (var skipPosition in skipsPositions)
        {
            float skipStart = skipPosition.z - (_picUpsDistanceZ * 0.5f);
            float skipEnd = skipPosition.z + (_picUpsDistanceZ * 0.5f);
            if (currentSpawnPosition.z >= skipStart && currentSpawnPosition.z <= skipEnd)
            {
                return true;
            }
        }
        return false;
    }
    private void OnDrawGizmos() // preview in tracks
    {
        Vector3 currentSpawnPosition = _picUpStartSpawn.position;
        if (_picUpStartSpawn != null && _picUpsDistanceZ > 0)
        {
            while (currentSpawnPosition.z < _picUpEndSpawn.position.z)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawCube(currentSpawnPosition, Vector3.one);
                currentSpawnPosition.z += _picUpsDistanceZ;
            }
        }
    }
}
