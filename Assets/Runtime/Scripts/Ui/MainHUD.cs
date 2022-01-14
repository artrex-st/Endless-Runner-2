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
    //[SerializeField] private TextMeshProUGUI buildVersionText;
    //Overlays
    private void Awake()
    {
        Init();
        //OpenMenu(Menu.START);
        //buildVersionText.text = $"Version: {Application.version} \n In development!";
    }
    private void Start()
    {
        startUiOverlay.SetActive(true);
        musicController.PlayStartMenuMusic();
    }
    public void QuitGame()
    {
        gameMode.QuitGame();
    }
    public void Init()
    {
        //TODO: Maybe use Utils
        mainUiOverlay = mainUiOverlay != null ? mainUiOverlay : transform.Find("Main Overlay").gameObject;
        pauseUiOverlay = pauseUiOverlay != null ? pauseUiOverlay : transform.Find("Pause Overlay").gameObject;
        startUiOverlay = startUiOverlay != null ? startUiOverlay : transform.Find("Start Overlay").gameObject;
        settingsOverlay = settingsOverlay != null ? settingsOverlay : transform.Find("Setting Overlay").gameObject;

        IsInitialised = true;
    }
    public void OpenMenu(Menu _Menu, GameObject _CallingMenu)
    {
        if (!IsInitialised)
        {
            Init();
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
                //CloseAllMenus();
                break;
        }
        lastMenu = _CallingMenu;
        _CallingMenu.SetActive(false);
    }
    // public void OpenMenu(Menu _Menu)
    // {
    //     if (!IsInitialised)
    //     {
    //         Init();
    //     }
    //     switch (_Menu)
    //     {
    //         case Menu.MAIN:
    //             mainUiOverlay.SetActive(true);
    //             break;
    //         case Menu.PAUSE:
    //             pauseUiOverlay.SetActive(true);
    //             break;
    //         case Menu.START:
    //             startUiOverlay.SetActive(true);
    //             break;
    //         case Menu.SETTINGS:
    //             settingsOverlay.SetActive(true);
    //             break;
    //         default:
    //             //CloseAllMenus();
    //             break;
    //     }
    //     musicController.PlayStartMenuMusic();
    // }
    private void CloseAllMenus()
    {
        foreach(GameObject _MenuChild in transform)
        {
            _MenuChild.SetActive(false);
        } 
    }
    public void BtnMainHudSound()
    {
        mainHudSound.PlayButtonPressSound();
    }
}
