using UnityEngine;

public class TrackSegment : MonoBehaviour
{
    [SerializeField] private Transform _startPoint, _endPoint;
    [SerializeField] private ObstacleSpawner[] _obstacleSpawners;
    [SerializeField] private DecorationSpawner _decorationSpawner;
    [Header("PicUps")]
    [SerializeField] private PickUpSpawner[] _picUpSpawners;
    [SerializeField, Range(0,1f)] private float _pickupSpawnChance;

    public Transform StartPoint => _startPoint;
    public Transform EndPoint => _endPoint;

    public float Length => Vector3.Distance(EndPoint.position, StartPoint.position);
    public float SqrLength => (EndPoint.position - StartPoint.position).sqrMagnitude;

    public ObstacleSpawner[] ObstacleSpawners => _obstacleSpawners;
    public PickUpSpawner[] PicUpSpawners => _picUpSpawners;
    public DecorationSpawner DecorationSpawner => _decorationSpawner;

    public TrackSegment(Transform startPoint, Transform endPoint, ObstacleSpawner[] obstacleSpawners, DecorationSpawner decorationSpawner, PickUpSpawner[] picUpSpawners, float pickupSpawnChance)
    {
        _startPoint = startPoint;
        _endPoint = endPoint;
        _obstacleSpawners = obstacleSpawners;
        _decorationSpawner = decorationSpawner;
        _picUpSpawners = picUpSpawners;
        _pickupSpawnChance = pickupSpawnChance;
    }
    public void TrackSpawObstacles()
    {
        foreach (var obstacleSpawner in ObstacleSpawners)
        {
            obstacleSpawner.SpawnObstacle();
        }
    }
    public void TrackSpawnPicUps()
    {
        if (_picUpSpawners.Length > 0 && Random.value <= _pickupSpawnChance)
        {
            Vector3[] skipPositions = new Vector3[_obstacleSpawners.Length];
            
            for (int i = 0; i < skipPositions.Length; i++)
            {
                skipPositions[i] = ObstacleSpawners[i].transform.position;
            }

            int randomIndex = Random.Range(0, _picUpSpawners.Length);
            PickUpSpawner pickupSpawner = _picUpSpawners[randomIndex];
            pickupSpawner.SpawnPicUps(skipPositions);
        }
    }
    public void TrackSpawnDecorations()
    {
        DecorationSpawner.gameObject.SetActive(true);
    }
}
