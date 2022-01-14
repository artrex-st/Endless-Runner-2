using UnityEngine;

public class PickUpSpawner : MonoBehaviour //TODO: Object Pooling
{
    [SerializeField] private PicUp[] PicUpsPrefabOptions;
    [SerializeField, Range(0.01f, 10)] private float PicUpsDistanceZ = 1;
    [SerializeField] private Transform picUpStartSpawn;
    [SerializeField] private Transform picUpEndSpawn;

    public void SpawnPicUps(Vector3[] _SkipPositions) // instantiate PicUps/Boots
    {
        Vector3 currentSpawnPosition = picUpStartSpawn.position;
        while (currentSpawnPosition.z < picUpEndSpawn.position.z)
        {
            if (!NeedSkipPosition(currentSpawnPosition, _SkipPositions))
            {
                PicUp pickup = Instantiate(PicUpsPrefabOptions[0], currentSpawnPosition, Quaternion.identity, transform); //TODO: remove Hard Code, update to get randown PicUp
            }
            currentSpawnPosition.z += PicUpsDistanceZ;
        }
    }
    public bool NeedSkipPosition(Vector3 _CurrentSpawnPosition, Vector3[] _SkipsPositions) // check obstacle spot
    {
        foreach (var skipPosition in _SkipsPositions)
        {
            float skipStart = skipPosition.z - (PicUpsDistanceZ * 0.5f);
            float skipEnd = skipPosition.z + (PicUpsDistanceZ * 0.5f);
            if (_CurrentSpawnPosition.z >= skipStart && _CurrentSpawnPosition.z <= skipEnd)
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
