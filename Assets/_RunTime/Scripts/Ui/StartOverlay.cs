using TMPro;
using UnityEngine;

public class StartOverlay : MonoBehaviour
{
    [SerializeField] private MainHUD mainHUD;
    [SerializeField] private GameMode gameMode;
    [Header("Start UiOverlay Elements")]
    [SerializeField] private TextMeshProUGUI highestScoreText;
    [SerializeField] private TextMeshProUGUI lastScoreText;
    [SerializeField] private TextMeshProUGUI totalPicUpsText;
    [SerializeField] private GameObject btnQuit; 
    [SerializeField] private GameObject tutorialObject; 
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
        Initialize();
    }
    private void OnEnable()
    {
        GetLoadedScoresTexts(gameMode);
    }
    private void Initialize()
    {
        mainHUD = mainHUD != null ? mainHUD : GetComponentInParent<MainHUD>();
        gameMode = gameMode != null ? gameMode : mainHUD.GameMode;
#if UNITY_WEBGL && !UNITY_EDITOR
        btnQuit.SetActive(false);
#endif
#if UNITY_IOS || UNITY_ANDROID
        tutorialObject.SetActive(false);
#endif  
    }
    private void GetLoadedScoresTexts(GameMode gameMode)
    {
        highestScoreText.text = $"Highest Score! \n{gameMode.GetLoadedData().highestScore}";
        lastScoreText.text = $"Last Score! \n{gameMode.GetLoadedData().lastScore}";
        totalPicUpsText.text = $"{gameMode.GetLoadedData().totalCherry}";
    }
}
