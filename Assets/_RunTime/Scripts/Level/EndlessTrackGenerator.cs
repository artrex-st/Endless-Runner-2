using System.Collections.Generic;
using UnityEngine;

public class EndlessTrackGenerator : MonoBehaviour
{
    [SerializeField] private PlayerControl player;
    [SerializeField] private TrackSegment firstTrackPrefab;
    [SerializeField] private TrackSegment[] easyTrackPrefabs;
    [SerializeField] private TrackSegment[] hardTrackPrefabs;
    [SerializeField] private TrackSegment[] rewardTrackPrefabs;

    [Header("Endless Generation Parameters")]
    [SerializeField] private int initialTrackCount = 5;
    [SerializeField] private int minTracksInFrontOfPlayer = 3;
    [SerializeField] private float minDistanceToConsiderInsideTrack = 3;
    
    [Header("Level Difficulty Parameters")]
    [SerializeField, Range(0, 1)] private float hardTrackChance = 0.2f;
    [SerializeField] private int minTracksBeforeReward = 10;
    [SerializeField] private int maxTracksBeforeReward = 20;
    [SerializeField] private int minRewardTrackCount = 1;
    [SerializeField] private int maxRewardTrackCount = 3;

    private List<TrackSegment> currentSegments = new List<TrackSegment>();
    private bool isSpawningRewardTracks = false;
    private int rewardTracksLeftToSpawn = 0;
    private int trackSpawnedAfterLastReward = 0;

    private void Start()
    {
        Initialize();
    }
    private void Update()
    {
        UpdateTracks();
    }

    private void Initialize()
    {
        PoolingSystem.Instance.ClearDictionary();
        TrackSegment previousTrack = SpawnTrackSegment(firstTrackPrefab, null);
        SpawnTracks(initialTrackCount);
    }
    private void UpdateTracks()
    {
        var playerTrackIndex = FindTrackIndexWithPlayer();
        if (playerTrackIndex < 0)
        {
            return;
        }
        //Spawn more track is needed
        int tracksInFrontOfPlayer = currentSegments.Count - (playerTrackIndex + 1);
        if (tracksInFrontOfPlayer < minTracksInFrontOfPlayer)
        {
            SpawnTracks(minTracksInFrontOfPlayer - tracksInFrontOfPlayer);
        }
        //Despawn tracks behind player
        for (int i = 0; i < playerTrackIndex; i++)
        {
            PoolingSystem.Instance.ReturnGameObject(currentSegments[i].gameObject);
        }
        currentSegments.RemoveRange(0, playerTrackIndex);
    }

    private int FindTrackIndexWithPlayer()
    {
        for (int i = 0; i < currentSegments.Count; i++)
        {
            var track = currentSegments[i];
            if (player.transform.position.z >= track.StartPoint.position.z + minDistanceToConsiderInsideTrack &&
                player.transform.position.z <= track.EndPoint.position.z)
            {
                return i;
            }
        }
        return -1;
    }
    private void SpawnTracks(int trackCount)
    {
        TrackSegment previousTrack = currentSegments.Count > 0 ? currentSegments[currentSegments.Count - 1] : null;
        for (int i = 0; i < trackCount; i++)
        {
            TrackSegment track = GetRandomTrack();
            previousTrack = SpawnTrackSegment(track, previousTrack);
        }
    }
    private TrackSegment GetRandomTrack()
    {
        TrackSegment[] trackList = null;
        if (isSpawningRewardTracks)
        {
            trackList = rewardTrackPrefabs;
        }
        else
        {
            trackList = Random.value <= hardTrackChance ? hardTrackPrefabs : easyTrackPrefabs;
        }
        return trackList[Random.Range(0, trackList.Length)];
    }
    private TrackSegment SpawnTrackSegment(TrackSegment track, TrackSegment previousTrack)
    {
        GameObject trackInstanceObj = PoolingSystem.Instance.GetObject(track.gameObject);    // TODO: OPTIMIZE THIS!!!!
        trackInstanceObj.transform.parent = this.transform;                         // TODO: OPTIMIZE THIS!!!!
        TrackSegment trackInstance = trackInstanceObj.GetComponent<TrackSegment>(); // TODO: OPTIMIZE THIS!!!!

        if (previousTrack != null)
        {
            trackInstance.transform.position = previousTrack.EndPoint.position + (trackInstance.transform.position - trackInstance.StartPoint.position);
        }
        else
        {
            trackInstance.transform.position = Vector3.zero;
        }
        //spawners
        trackInstance.TrackSpawObstacles();
        trackInstance.TrackSpawnPicUps();
        trackInstance.TrackSpawnDecorations();

        currentSegments.Add(trackInstance);
        UpdateTrackDifficultyParameters();

        return trackInstance;
    }
    private void UpdateTrackDifficultyParameters()
    {
        if (isSpawningRewardTracks)
        {
            rewardTracksLeftToSpawn--;
            if (rewardTracksLeftToSpawn <= 0)
            {
                isSpawningRewardTracks = false;
                trackSpawnedAfterLastReward = 0;
            }
        }
        else
        {
            trackSpawnedAfterLastReward++;
            int requiredTracksBeforeReward = Random.Range(minTracksBeforeReward, maxTracksBeforeReward + 1);
            if (trackSpawnedAfterLastReward >= requiredTracksBeforeReward)
            {
                isSpawningRewardTracks = true;
                rewardTracksLeftToSpawn = Random.Range(minRewardTrackCount, maxRewardTrackCount + 1);
            }
        }
    }
}