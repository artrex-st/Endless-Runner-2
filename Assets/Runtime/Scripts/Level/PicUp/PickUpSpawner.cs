using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpSpawner : MonoBehaviour
{
    [SerializeField] private PicUp[] PicUpsPrefabOptions;
    [SerializeField] private float PicUpsDistanceZ = 1;
    [SerializeField] private Transform picUpStartSpawn;
    [SerializeField] private Transform picUpEndSpawn;

    public void SpawnPicUps(Vector3[] _SkipPositions)
    {
        Vector3 currentSpawnPosition = picUpStartSpawn.position;
        while (currentSpawnPosition.z < picUpEndSpawn.position.z)
        {
            if (!NeedSkipPosition(currentSpawnPosition, _SkipPositions))
            {
                PicUp pickup = Instantiate(PicUpsPrefabOptions[0], currentSpawnPosition, Quaternion.identity, transform);
            }
            currentSpawnPosition.z += PicUpsDistanceZ;
        }
    }
    public bool NeedSkipPosition(Vector3 _CurrentSpawnPosition, Vector3[] _SkipsPositions)
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
    private void OnDrawGizmos()
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
