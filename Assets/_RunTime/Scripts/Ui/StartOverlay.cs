using TMPro;
using UnityEngine;

public class StartOverlay : MonoBehaviour
{
    public delegate ScoreData ScoreDataHandler();
    public event ScoreDataHandler OnCalledScoreData;
    [SerializeField] private MainHUD mainHUD;
    [Header("Start UiOverlay Elements")]
    [SerializeField] private TextMeshProUGUI highestScoreText, lastScoreText, totalPicUpsText;
    [SerializeField] private GameObject btnQuit, tutorialObject;
    private ScoreData currentScoreData = new ScoreData();
    public void BtnRun()
    {
        mainHUD.BtnMainHudSound();
        mainHUD.OpenMenu(Menu.MAIN, gameObject);
    }
    public void BtnSettings()
    {
        mainHUD.BtnMainHudSound();
        mainHUD.OpenMenu(Menu.SETTINGS, gameObject);
    }
    private void Awake()
    {
        _Initialize();
    }
    private void OnEnable()
    {
        _GetLoadedScoresTexts();
    }
    private void _Initialize()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        btnQuit.SetActive(false);
#endif
#if UNITY_IOS || UNITY_ANDROID
        tutorialObject.SetActive(false);
#endif  
    }
    public void _GetLoadedScoresTexts()
    {
        currentScoreData = OnCalledScoreData?.Invoke();
        highestScoreText.text = $"Highest Score! \n{currentScoreData.HighestScore}";
        lastScoreText.text = $"Last Score! \n{currentScoreData.LastScore}";
        totalPicUpsText.text = $"{currentScoreData.TotalPicUps}";
    }
}
