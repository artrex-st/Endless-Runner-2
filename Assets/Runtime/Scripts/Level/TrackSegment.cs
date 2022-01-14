using UnityEngine;

public class TrackSegment : MonoBehaviour
{
    [SerializeField] private Transform start;
    [SerializeField] private Transform end;

    [SerializeField] private ObstacleSpawner[] obstacleSpawners;
    [SerializeField] DecorationSpawner decorationSpawner;
    [Header("PicUps")]
    [SerializeField] private PickUpSpawner[] picUpSpawners;
    [SerializeField, Range(0,1f)] private float pickupSpawnChance;
    //end
    public Transform Start => start;
    public Transform End => end;

    public float Length => Vector3.Distance(End.position, Start.position);
    public float SqrLength => (End.position - Start.position).sqrMagnitude;

    public ObstacleSpawner[] ObstacleSpawners => obstacleSpawners;
    public PickUpSpawner[] PicUpSpawners => picUpSpawners;
    public DecorationSpawner DecorationSpawner => decorationSpawner;

    public void TrackSpawObstacles()
    {
        foreach (var obstacleSpawner in ObstacleSpawners)
        {
            obstacleSpawner.SpawnObstacle();
        }
    }
    public void TrackSpawnPicUps()
    {
        if (picUpSpawners.Length > 0 && Random.value <= pickupSpawnChance)
        {
            Vector3[] skipPositions = new Vector3[obstacleSpawners.Length];
            for (int i = 0; i < skipPositions.Length; i++)
            {
                skipPositions[i] = ObstacleSpawners[i].transform.position;
            }

            int randomIndex = Random.Range(0, picUpSpawners.Length);
            PickUpSpawner pickupSpawner = picUpSpawners[randomIndex];
            pickupSpawner.SpawnPicUps(skipPositions);
        }
    }
    public void TrackSpawnDecorations()
    {
        DecorationSpawner.SpawnDecorations();
    }
}
