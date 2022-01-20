using System.Collections.Generic;
using UnityEngine;

public sealed class PoolingSystem : MonoBehaviour
{
    public static PoolingSystem Instance;
    private Dictionary<string, Queue<GameObject>> _objectPool = new Dictionary<string, Queue<GameObject>>();
    private Dictionary<string, Queue<TrackSegment>> _trackPool = new Dictionary<string, Queue<TrackSegment>>();
    public void ClearPollDictionarys()
    {
        _objectPool.Clear();
        _trackPool.Clear();
    }
    
    public GameObject GetObject(GameObject gameObject)
    {
        if (_objectPool.TryGetValue(gameObject.name, out Queue<GameObject> objectList))
        {
            if (objectList.Count == 0)
            {
                return _CreateNewObject(gameObject);
            }
            else
            {
                GameObject _object = objectList.Dequeue();
                _object.SetActive(true);
                return _object;
            }
        }
        else
        {
            return _CreateNewObject(gameObject);
        }

    }
    public TrackSegment GetTrackObject(TrackSegment trackObject)
    {
        if (_trackPool.TryGetValue(trackObject.name, out Queue<TrackSegment> objectList))
        {
            if (objectList.Count == 0)
            {
                return _CreateNewTrackObject(trackObject);
            }
            else
            {
                TrackSegment _object = objectList.Dequeue();
                _object.gameObject.SetActive(true);
                return _object;
            }
        }
        else
        {
            return _CreateNewTrackObject(trackObject);
        }

    }

    public void ReturnGameObject(GameObject gameObject)
    {
        if (_objectPool.TryGetValue(gameObject.name, out Queue<GameObject> objectList))
        {
            objectList.Enqueue(gameObject);
        }
        else
        {
            Queue<GameObject> newObjectQueue = new Queue<GameObject>();
            newObjectQueue.Enqueue(gameObject);
            _objectPool.Add(gameObject.name, newObjectQueue);
        }
        gameObject.SetActive(false);
    }
    public void ReturnTrackObject(TrackSegment trackObject)
    {
        if (_trackPool.TryGetValue(trackObject.name, out Queue<TrackSegment> trackList))
        {
            trackList.Enqueue(trackObject);
        }
        else
        {
            Queue<TrackSegment> newTrackQueue = new Queue<TrackSegment>();
            newTrackQueue.Enqueue(trackObject);
            _trackPool.Add(trackObject.name, newTrackQueue);
        }
        trackObject.gameObject.SetActive(false);
    }
    private void Awake()
    {
        _Initialize();
    }
    private void _Initialize()
    {
        if (Instance != null)
        {
            GameObject.Destroy(Instance);
            _objectPool.Clear();
        }
        else
        {
            Instance = this;
        }
    }
    private GameObject _CreateNewObject(GameObject gameObject)
    {
        GameObject newGO = Instantiate(gameObject);
        newGO.name = gameObject.name;
        return newGO;
    }

    private TrackSegment _CreateNewTrackObject(TrackSegment trackObject)
    {
        TrackSegment newTrack = Instantiate(trackObject);
        newTrack.name = trackObject.name;
        return newTrack;
    }
}
