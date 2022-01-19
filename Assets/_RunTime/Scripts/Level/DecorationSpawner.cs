using UnityEngine;

public class DecorationSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] _decorationOptions;
    public GameObject CurrentDecoration {get; private set;}

    public DecorationSpawner(GameObject[] decorationOptions, GameObject currentDecoration)
    {
        _decorationOptions = decorationOptions;
        CurrentDecoration = currentDecoration;
    }
    
    public void SpawnDecorations()
    {
        if (_decorationOptions.Length >= 0)
        {
            GameObject prefab = _decorationOptions[Random.Range(0, _decorationOptions.Length)];
            CurrentDecoration = Instantiate(prefab, transform);
            CurrentDecoration.transform.localPosition = Vector3.zero;
            CurrentDecoration.transform.rotation = Quaternion.identity;
        }
        else
        {
            Debug.LogWarning($"decorationOptions Array in {gameObject.name} is Empty");
        }
    }
}
