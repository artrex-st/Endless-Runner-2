using TMPro;
using UnityEngine;

public sealed class StartOverlay : MonoBehaviour
{
    public delegate ScoreData ScoreLoadHandler();
    public event ScoreLoadHandler OnLoadingScoreData;
    [SerializeField] private MainHUD _mainHUD;
    [Header("Start UiOverlay Elements")]
    [SerializeField] private TextMeshProUGUI _highestScoreText, _lastScoreText, _totalPicUpsText;
    [SerializeField] private GameObject _btnQuit, _tutorialObject;
    private ScoreData currentScoreData = new ScoreData();

    public StartOverlay(MainHUD mainHUD, TextMeshProUGUI highestScoreText, TextMeshProUGUI lastScoreText, TextMeshProUGUI totalPicUpsText, GameObject btnQuit, GameObject tutorialObject)
    {
        _mainHUD = mainHUD;
        _highestScoreText = highestScoreText;
        _lastScoreText = lastScoreText;
        _totalPicUpsText = totalPicUpsText;
        _btnQuit = btnQuit;
        _tutorialObject = tutorialObject;
        _Initialize();
    }

    public void BtnRun()
    {
        _mainHUD.BtnMainHudSound();
        _mainHUD.OpenMenu(Menu.MAIN, gameObject);
    }
    public void BtnSettings()
    {
        _mainHUD.BtnMainHudSound();
        _mainHUD.OpenMenu(Menu.SETTINGS, gameObject);
    }
    private void Awake()
    {
        _Initialize();
    }
    private void OnEnable()
    {
        _GetScoreDataTexts();
    }
    private void _Initialize()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        btnQuit.SetActive(false);
#endif
#if UNITY_IOS || UNITY_ANDROID
        _tutorialObject.SetActive(false);
#endif  
    }
    public void _GetScoreDataTexts()
    {
        currentScoreData = OnLoadingScoreData?.Invoke();
        _highestScoreText.text = $"Highest Score! \n{currentScoreData.HighestScore}";
        _lastScoreText.text = $"Last Score! \n{currentScoreData.LastScore}";
        _totalPicUpsText.text = $"{currentScoreData.TotalPicUps}";
    }
}
