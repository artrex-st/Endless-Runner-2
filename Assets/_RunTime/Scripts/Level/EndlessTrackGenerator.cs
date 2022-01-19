using System.Collections.Generic;
using UnityEngine;

public class EndlessTrackGenerator : MonoBehaviour
{
    [SerializeField] private PlayerControl _playerControl;
    [SerializeField] private TrackSegment _firstTrackPrefab;
    [SerializeField] private TrackSegment[] _easyTrackPrefabs, _hardTrackPrefabs, _rewardTrackPrefabs;

    [Header("Endless Generation Parameters")]
    [SerializeField] private int _initialTrackCount = 5, _minTracksInFrontOfPlayer = 3;
    [SerializeField] private float _minDistanceToConsiderInsideTrack = 3;
    
    [Header("Level Difficulty Parameters")]
    [SerializeField, Range(0, 1)] private float _hardTrackChance = 0.2f;
    [SerializeField] private int _minTracksBeforeReward = 10, _maxTracksBeforeReward = 20, _minRewardTrackCount = 1, _maxRewardTrackCount = 3;

    private List<TrackSegment> _currentSegments = new List<TrackSegment>();
    private bool _isSpawningRewardTracks = false;
    private int _rewardTracksLeftToSpawn = 0, _trackSpawnedAfterLastReward = 0;

    private void Start()
    {
        _Initialize();
    }
    private void Update()
    {
        _UpdateTracks();
    }
    private void _Initialize()
    {
        PoolingSystem.Instance.ClearPollDictionarys();
        TrackSegment previousTrack = _SpawnTrackSegment(_firstTrackPrefab, null);
        _SpawnTracks(_initialTrackCount);
    }
    private void _UpdateTracks()
    {
        var playerTrackIndex = _FindTrackIndexWithPlayer();
        
        if (playerTrackIndex < 0)
        {
            return;
        }
        int tracksInFrontOfPlayer = _currentSegments.Count - (playerTrackIndex + 1);
        
        if (tracksInFrontOfPlayer < _minTracksInFrontOfPlayer)
        {
            _SpawnTracks(_minTracksInFrontOfPlayer - tracksInFrontOfPlayer);
        }
        
        for (int i = 0; i < playerTrackIndex; i++)
        {
            PoolingSystem.Instance.ReturnTrackObject(_currentSegments[i]);
        }
        _currentSegments.RemoveRange(0, playerTrackIndex);
    }

    private int _FindTrackIndexWithPlayer()
    {
        for (int i = 0; i < _currentSegments.Count; i++)
        {
            var track = _currentSegments[i];
            
            if (_playerControl.transform.position.z >= track.StartPoint.position.z + _minDistanceToConsiderInsideTrack &&
                _playerControl.transform.position.z <= track.EndPoint.position.z)
            {
                return i;
            }
        }
        return -1;
    }
    private void _SpawnTracks(int trackCount)
    {
        TrackSegment previousTrack = _currentSegments.Count > 0 ? _currentSegments[_currentSegments.Count - 1] : null;
        
        for (int i = 0; i < trackCount; i++)
        {
            TrackSegment track = _GetRandomTrack();
            previousTrack = _SpawnTrackSegment(track, previousTrack);
        }
    }
    private TrackSegment _GetRandomTrack()
    {
        TrackSegment[] trackList = null;
        
        if (_isSpawningRewardTracks)
        {
            trackList = _rewardTrackPrefabs;
        }
        else
        {
            trackList = Random.value <= _hardTrackChance ? _hardTrackPrefabs : _easyTrackPrefabs; //TODO: MEDIUM CHANCE
        }
        return trackList[Random.Range(0, trackList.Length)];
    }
    private TrackSegment _SpawnTrackSegment(TrackSegment track, TrackSegment previousTrack)
    {
        TrackSegment trackInstance = PoolingSystem.Instance.GetTrackObject(track);
        trackInstance.transform.parent = this.transform;

        if (previousTrack != null)
        {
            trackInstance.transform.position = previousTrack.EndPoint.position + (trackInstance.transform.position - trackInstance.StartPoint.position);
        }
        else
        {
            trackInstance.transform.position = Vector3.zero;
        }
        trackInstance.TrackSpawObstacles();
        trackInstance.TrackSpawnPicUps();
        trackInstance.TrackSpawnDecorations();

        _currentSegments.Add(trackInstance);
        _UpdateTrackDifficultyParameters();

        return trackInstance;
    }
    private void _UpdateTrackDifficultyParameters()
    {
        if (_isSpawningRewardTracks)
        {
            _rewardTracksLeftToSpawn--;
            
            if (_rewardTracksLeftToSpawn <= 0)
            {
                _isSpawningRewardTracks = false;
                _trackSpawnedAfterLastReward = 0;
            }
        }
        else
        {
            _trackSpawnedAfterLastReward++;
            int requiredTracksBeforeReward = Random.Range(_minTracksBeforeReward, _maxTracksBeforeReward + 1);
            
            if (_trackSpawnedAfterLastReward >= requiredTracksBeforeReward)
            {
                _isSpawningRewardTracks = true;
                _rewardTracksLeftToSpawn = Random.Range(_minRewardTrackCount, _maxRewardTrackCount + 1);
            }
        }
    }
}