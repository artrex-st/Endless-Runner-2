using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private Obstacle[] _obstaclePrefabOptions;
    private GameObject _currentObstacle;

    public ObstacleSpawner(Obstacle[] obstaclePrefabOptions)
    {
        this._obstaclePrefabOptions = obstaclePrefabOptions;
    }

    public void SpawnObstacle()
    {
        if (transform.childCount <= 0 || !transform.GetChild(0).gameObject.activeInHierarchy)
        {
            Obstacle prefab = _obstaclePrefabOptions[Random.Range(0, _obstaclePrefabOptions.Length)];
            
            _currentObstacle = PoolingSystem.Instance.GetObject(prefab.gameObject);
            _currentObstacle.transform.parent = transform;
            _currentObstacle.transform.localPosition = Vector3.zero;
            _currentObstacle.transform.rotation = Quaternion.identity;
        }
    }
    private void OnDrawGizmos()
    {
        Vector3 currentSpawnPosition = transform.position + Vector3.up;
        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(currentSpawnPosition, new Vector3(2,2,1));
    }
}
