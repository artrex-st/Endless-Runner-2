using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private Obstacle[] obstaclePrefabOptions;
    private Obstacle currentObstacle;

    public void SpawnObstacle()
    {
        if (transform.childCount <= 0) // ToDo: Improve this with Object Pooling
        {
            Obstacle prefab = obstaclePrefabOptions[Random.Range(0, obstaclePrefabOptions.Length)];
            currentObstacle = Instantiate(prefab, transform);
            currentObstacle.transform.localPosition = Vector3.zero;
            currentObstacle.transform.rotation = Quaternion.identity;
            currentObstacle.SpawnDecorations();
        }
    }
    private void OnDrawGizmos() // preview OBSTACLE
    {
        Vector3 currentSpawnPosition = transform.position + Vector3.up;
        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(currentSpawnPosition, new Vector3(2,2,1));
    }
}
