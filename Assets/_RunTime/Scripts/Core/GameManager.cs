using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public sealed class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] private GameObject _loadScreen, _loadBarObject;
    [SerializeField] private Slider _loadBar;
    [SerializeField] private TextMeshProUGUI _loadBarText;
    
    private float _totalSceneProgress;
    [SerializeField] private List<AsyncOperation> _scenesLoading = new List<AsyncOperation>();

    public void LoadGame()
    {
        _loadScreen.SetActive(true);
        _scenesLoading.Add(SceneManager.UnloadSceneAsync((int)SceneIndexes.TITLE_SCREEN));
        _scenesLoading.Add(SceneManager.LoadSceneAsync((int)SceneIndexes.MAIN_GAME_SCREEN, LoadSceneMode.Additive));

        StartCoroutine(GetSceneLoadProgress());
    }
    public void ReloadScene(int sceneIndex)
    {
        _loadScreen.SetActive(true);
        _scenesLoading.Add(SceneManager.UnloadSceneAsync(sceneIndex));
        _scenesLoading.Add(SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Additive));
        StartCoroutine(GetSceneLoadProgress());
    }
    private void Awake()
    {
        _initialize();
    }

    private void _initialize()
    {
        instance = this;
        SaveSystem.Initialize();
        SceneManager.LoadSceneAsync((int)SceneIndexes.TITLE_SCREEN, LoadSceneMode.Additive);
        _loadScreen.SetActive(false);
    }
    private IEnumerator GetSceneLoadProgress()
    {
        for (int i = 0; i < _scenesLoading.Count; i++)
        {
            while (!_scenesLoading[i].isDone)
            {
                _totalSceneProgress = 0;
                foreach (AsyncOperation operation in _scenesLoading)
                {
                    _totalSceneProgress += operation.progress;
                }
                _loadBar.value = _totalSceneProgress;
                _totalSceneProgress = (_totalSceneProgress / _scenesLoading.Count) * 100f;
                _loadBarText.text = $"Loading ... {Mathf.RoundToInt(_totalSceneProgress)}%";
                yield return null;
            }
        }
        _scenesLoading.Clear();
        _loadScreen.SetActive(false);
    }
}
