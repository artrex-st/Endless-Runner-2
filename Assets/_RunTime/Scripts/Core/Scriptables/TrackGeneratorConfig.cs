using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New TrackGenerator Configuration", menuName = "TrackGenerator Configuration")]

public class TrackGeneratorConfig : ScriptableObject
{
    public TrackSegment firstTrackPrefab;
    public TrackSegment[] easyTrackPrefabs, mediumTrackPrefabs, hardTrackPrefabs, rewardTrackPrefabs;
    [Header("Endless Generation Parameters")]
    public int initialTrackCount = 5, minTracksInFrontOfPlayer = 3;
    public float minDistanceToConsiderInsideTrack = 3;
    
    [Header("Level Difficulty Parameters")]
    [Range(0, 1)] public float mediumTrackChance = 0.5f;
    [Range(0, 1)] public float hardTrackChance = 0.2f;
    public int minTracksBeforeReward = 10, maxTracksBeforeReward = 20, minRewardTrackCount = 1, maxRewardTrackCount = 3;
    
    // private TrackSegment _FirstTrackPrefab => firstTrackPrefab;
    // private TrackSegment[] _EasyTrackPrefabs => easyTrackPrefabs;
    // private TrackSegment[] _MediumTrackPrefabs => mediumTrackPrefabs;
    // private TrackSegment[] _HardTrackPrefabs => hardTrackPrefabs;
    // private TrackSegment[] _RewardTrackPrefabs => rewardTrackPrefabs;
    // private int _InitialTrackCount => initialTrackCount;
    // private int _MinTracksInFrontOfPlayer => minTracksInFrontOfPlayer;
    // private int _MinTracksBeforeReward => minTracksBeforeReward;
    // private int _MaxTracksBeforeReward => maxTracksBeforeReward;
    // private int _MinRewardTrackCount => minRewardTrackCount;
    // private int _MaxRewardTrackCount => maxRewardTrackCount;
    // private float _MinDistanceToConsiderInsideTrack => minDistanceToConsiderInsideTrack;
    // private float _HardTrackChance => hardTrackChance;
}
