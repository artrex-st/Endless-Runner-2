using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public sealed class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] private GameObject loadScreen;
    [SerializeField] private GameObject loadBarObject;
    [SerializeField] private Slider loadBar;
    [SerializeField] private TextMeshProUGUI loadBarText;
    
    private float totalSceneProgress;
    [SerializeField] private List<AsyncOperation> scenesLoading = new List<AsyncOperation>();

    public void LoadGame()
    {
        loadScreen.SetActive(true);
        scenesLoading.Add(SceneManager.UnloadSceneAsync((int)SceneIndexes.TITLE_SCREEN));
        scenesLoading.Add(SceneManager.LoadSceneAsync((int)SceneIndexes.MAIN_GAME_SCREEN, LoadSceneMode.Additive));

        StartCoroutine(GetSceneLoadProgress());
    }
    public void ReloadScene(int sceneIndex)
    {
        loadScreen.SetActive(true);
        scenesLoading.Add(SceneManager.UnloadSceneAsync(sceneIndex));
        scenesLoading.Add(SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Additive));
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
        loadScreen.SetActive(false);
    }
    private IEnumerator GetSceneLoadProgress()
    {
        for (int i = 0; i < scenesLoading.Count; i++)
        {
            while (!scenesLoading[i].isDone)
            {
                totalSceneProgress = 0;
                foreach (AsyncOperation operation in scenesLoading)
                {
                    totalSceneProgress += operation.progress;
                }
                loadBar.value = totalSceneProgress;
                totalSceneProgress = (totalSceneProgress / scenesLoading.Count) * 100f;
                loadBarText.text = $"Loading ... {Mathf.RoundToInt(totalSceneProgress)}%";
                yield return null;
            }
        }
        scenesLoading.Clear();
        loadScreen.SetActive(false);
    }
}
