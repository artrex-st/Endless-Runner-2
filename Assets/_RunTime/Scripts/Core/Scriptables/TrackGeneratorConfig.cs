using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New TrackGenerator Configuration", menuName = "Config/TrackGenerator")]
public class TrackGeneratorConfig : ScriptableObject
{
    public TrackSegment firstTrackPrefab;
    public TrackSegment[] easyTrackPrefabs, hardTrackPrefabs, rewardTrackPrefabs;
    [Header("Endless Generation Parameters")]
    public int initialTrackCount = 5, minTracksInFrontOfPlayer = 3;
    public float minDistanceToConsiderInsideTrack = 3;
    
    [Header("Level Difficulty Parameters")]
    [Range(0, 1)] public float hardTrackChance = 0.2f;
    public int minTracksBeforeReward = 10, maxTracksBeforeReward = 20, minRewardTrackCount = 1, maxRewardTrackCount = 3;

    public TrackGeneratorConfig(TrackSegment firstTrackPrefab, TrackSegment[] easyTrackPrefabs, TrackSegment[] hardTrackPrefabs, TrackSegment[] rewardTrackPrefabs, int initialTrackCount, int minTracksInFrontOfPlayer, float minDistanceToConsiderInsideTrack, float hardTrackChance, int minTracksBeforeReward)
    {
        this.firstTrackPrefab = firstTrackPrefab;
        this.easyTrackPrefabs = easyTrackPrefabs;
        this.hardTrackPrefabs = hardTrackPrefabs;
        this.rewardTrackPrefabs = rewardTrackPrefabs;
        this.initialTrackCount = initialTrackCount;
        this.minTracksInFrontOfPlayer = minTracksInFrontOfPlayer;
        this.minDistanceToConsiderInsideTrack = minDistanceToConsiderInsideTrack;
        this.hardTrackChance = hardTrackChance;
        this.minTracksBeforeReward = minTracksBeforeReward;
    }
}
