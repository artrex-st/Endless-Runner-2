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
        highestScoreText.text = $"Highest Score! \n{gameMode.CurrentSave.HighestScore}";
        lastScoreText.text = $"Last Score! \n{gameMode.CurrentSave.LastScore}";
        totalPicUpsText.text = $"{gameMode.CurrentSave.TotalPicUps}";
    }
}
