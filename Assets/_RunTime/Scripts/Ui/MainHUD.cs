using TMPro;
using UnityEngine;

public sealed class MainHUD : MonoBehaviour
{
    public delegate void MainMenuEventHandler();
    public event MainMenuEventHandler OnPauseGame, OnResumeGame, OnClickedQuitGame;

    [SerializeField] private MusicController _musicController;
    [SerializeField] private MainHudSound _mainHudSound;
    [Header("Overlays")]
    [SerializeField] private GameObject _mainUiOverlay, _pauseUiOverlay, _startUiOverlay, _settingsOverlay;

    public bool IsInitialised {get; private set;}
    private GameObject lastMenu;

    public MainHUD(MusicController musicController, MainHudSound mainHudSound, GameObject mainUiOverlay, GameObject pauseUiOverlay, GameObject startUiOverlay, GameObject settingsOverlay)
    {
        _musicController = musicController;
        _mainHudSound = mainHudSound;
        _mainUiOverlay = mainUiOverlay;
        _pauseUiOverlay = pauseUiOverlay;
        _startUiOverlay = startUiOverlay;
        _settingsOverlay = settingsOverlay;
         _Initialize();
    }

    public void ButtonQuitGame()
    {
        OnClickedQuitGame?.Invoke();
    }
    public void BtnMainHudSound()
    {
        _mainHudSound.PlayButtonPressSound();
    }
    public void OpenMenu(Menu menuOpened, GameObject callingMenu)
    {
        if (!IsInitialised)
        {
            _Initialize();
        }
        switch (menuOpened)
        {
            case Menu.MAIN:
                OnResumeGame?.Invoke();
                _mainUiOverlay.SetActive(true);
                break;
            case Menu.PAUSE:
                OnPauseGame?.Invoke();
                _pauseUiOverlay.SetActive(true);
                break;
            case Menu.START:
                OnResumeGame?.Invoke();
                _startUiOverlay.SetActive(true);
                break;
            case Menu.SETTINGS:
                _settingsOverlay.SetActive(true);
                break;
            case Menu.CLOSE:
                lastMenu.SetActive(true);
                break;
            default:
                break;
        }
        lastMenu = callingMenu;
        callingMenu.SetActive(false);
    }
    private void Awake()
    {
        _Initialize();
    }
    private void _Initialize()
    {
        IsInitialised = true;
        _startUiOverlay.SetActive(true);
        _musicController.PlayStartMenuMusic();
    }
}
