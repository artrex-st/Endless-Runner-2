using System.Collections.Generic;
using UnityEngine;

public class PoolingSystem : MonoBehaviour
{
    private static Dictionary<string, Queue<GameObject>> objectPool = new Dictionary<string, Queue<GameObject>>();    // get
    public static PoolingSystem Instance;
    void Awake()
    {
        if (Instance != null)
        {
            GameObject.Destroy(Instance);
            objectPool.Clear();
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
    }
    public static void ClearDictionary()
    {
        objectPool.Clear();
    }
    public static GameObject GetObject(GameObject gameObject)
    {
        if (objectPool.TryGetValue(gameObject.name, out Queue<GameObject> objectList))
        {
            if (objectList.Count == 0)
                return CreateNewObject(gameObject);
            else
            {
                GameObject _object = objectList.Dequeue();
                _object.SetActive(true);
                return _object;
            }
        }
        else
            return CreateNewObject(gameObject);

    }
    private static GameObject CreateNewObject(GameObject gameObject)
    {
        GameObject newGO = Instantiate(gameObject);
        newGO.name = gameObject.name;
        return newGO;
    }
    public static void ReturnGameObject(GameObject gameObject)
    {
        if (objectPool.TryGetValue(gameObject.name, out Queue<GameObject> objectList))
        {
            objectList.Enqueue(gameObject);
        }
        else
        {
            Queue<GameObject> newObjectQueue = new Queue<GameObject>();
            newObjectQueue.Enqueue(gameObject);
            objectPool.Add(gameObject.name, newObjectQueue);
        }
        gameObject.SetActive(false);
    }
}
