using TMPro;
using UnityEngine;

public class MainHUD : MonoBehaviour
{
    [SerializeField] private GameMode gameMode;
    [SerializeField] private MusicController musicController;
    [SerializeField] private MainHudSound mainHudSound;
    public GameMode GameMode => gameMode;
    [Header("Overlays")]
    [SerializeField] private GameObject mainUiOverlay;
    [SerializeField] private GameObject pauseUiOverlay;
    [SerializeField] private GameObject startUiOverlay;
    [SerializeField] private GameObject settingsOverlay;
    public bool IsInitialised {get; private set;}
    private GameObject lastMenu;

    public void QuitGame()
    {
        gameMode.QuitGame();
    }
    public void BtnMainHudSound()
    {
        mainHudSound.PlayButtonPressSound();
    }
    public void OpenMenu(Menu _Menu, GameObject _CallingMenu)
    {
        if (!IsInitialised)
        {
            Initialize();
        }
        switch (_Menu)
        {
            case Menu.MAIN:
                mainUiOverlay.SetActive(true);
                break;
            case Menu.PAUSE:
                pauseUiOverlay.SetActive(true);
                break;
            case Menu.START:
                startUiOverlay.SetActive(true);
                break;
            case Menu.SETTINGS:
                settingsOverlay.SetActive(true);
                break;
            case Menu.CLOSE:
                lastMenu.SetActive(true);
                break;
            default:
                break;
        }
        lastMenu = _CallingMenu;
        _CallingMenu.SetActive(false);
    }
    private void Awake()
    {
        Initialize();
    }
    private void Initialize()
    {
        mainUiOverlay = mainUiOverlay != null ? mainUiOverlay : GetComponentInChildren<MainOverlay>().gameObject;
        pauseUiOverlay = pauseUiOverlay != null ? pauseUiOverlay : GetComponentInChildren<PauseOverlay>().gameObject;
        startUiOverlay = startUiOverlay != null ? startUiOverlay : GetComponentInChildren<StartOverlay>().gameObject;
        settingsOverlay = settingsOverlay != null ? settingsOverlay : GetComponentInChildren<SettingOverlay>().gameObject;

        IsInitialised = true;
        
        startUiOverlay.SetActive(true);
        musicController.PlayStartMenuMusic();
    }
}
