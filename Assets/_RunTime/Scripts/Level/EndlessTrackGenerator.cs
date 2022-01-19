using System.Collections.Generic;
using UnityEngine;

public class EndlessTrackGenerator : MonoBehaviour
{
    [SerializeField] private PlayerControl _playerControl;
    [SerializeField] private TrackGeneratorConfig trackGeneratorConfig;
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
        TrackSegment previousTrack = _SpawnTrackSegment(trackGeneratorConfig.firstTrackPrefab, null);
        _SpawnTracks(trackGeneratorConfig.initialTrackCount);
    }
    private void _UpdateTracks()
    {
        var playerTrackIndex = _FindTrackIndexWithPlayer();
        
        if (playerTrackIndex < 0)
        {
            return;
        }
        int tracksInFrontOfPlayer = _currentSegments.Count - (playerTrackIndex + 1);
        
        if (tracksInFrontOfPlayer < trackGeneratorConfig.minTracksInFrontOfPlayer)
        {
            _SpawnTracks(trackGeneratorConfig.minTracksInFrontOfPlayer - tracksInFrontOfPlayer);
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
            
            if (_playerControl.transform.position.z >= track.StartPoint.position.z + trackGeneratorConfig.minDistanceToConsiderInsideTrack &&
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
            trackList = trackGeneratorConfig.rewardTrackPrefabs;
        }
        else
        {
            trackList = Random.value <= trackGeneratorConfig.hardTrackChance ? trackGeneratorConfig.hardTrackPrefabs : trackGeneratorConfig.easyTrackPrefabs; //TODO: MEDIUM CHANCE
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
            int requiredTracksBeforeReward = Random.Range(trackGeneratorConfig.minTracksBeforeReward, trackGeneratorConfig.maxTracksBeforeReward + 1);
            
            if (_trackSpawnedAfterLastReward >= requiredTracksBeforeReward)
            {
                _isSpawningRewardTracks = true;
                _rewardTracksLeftToSpawn = Random.Range(trackGeneratorConfig.minRewardTrackCount, trackGeneratorConfig.maxRewardTrackCount + 1);
            }
        }
    }
}