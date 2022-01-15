using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] private GameObject loadScreen;
    [SerializeField] private GameObject loadBarObject;
    [SerializeField] private Slider loadBar;
    [SerializeField] private TextMeshProUGUI loadBarText;
    private float totalSceneProgress;
    List<AsyncOperation> scenesLoading = new List<AsyncOperation>();

    private void Awake()
    {
        instance = this;
        SceneManager.LoadSceneAsync((int)SceneIndexes.TITLE_SCREEN, LoadSceneMode.Additive);
        //loadBar.onValueChanged.AddListener(delegate {ValueChangeCheck(); });
    }
    private void Start()
    {
        loadScreen = loadScreen != null ? loadScreen : transform.Find("LoadScreen Menu").gameObject;
        loadBar = loadBar != null ? loadBar : GetComponentInChildren<Slider>();
        loadBarObject = loadBar.gameObject;
        loadBarText = loadBar.GetComponentInChildren<TextMeshProUGUI>();
        loadScreen.SetActive(false);
    }
    public void LoadGame()
    {
        loadScreen.SetActive(true);
        scenesLoading.Add(SceneManager.UnloadSceneAsync((int)SceneIndexes.TITLE_SCREEN));
        scenesLoading.Add(SceneManager.LoadSceneAsync((int)SceneIndexes.MAIN_GAME_SCREEN, LoadSceneMode.Additive));

        StartCoroutine(GetSceneLoadProgress());
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
        loadScreen.SetActive(false);
    }
}
