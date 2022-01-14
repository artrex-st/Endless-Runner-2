using UnityEngine;

public class PauseOverlay : MonoBehaviour
{
    private MainHUD mainHUD;
    private GameMode gameMode;
    [SerializeField] private GameObject btnQuit;
    private void Awake()
    {
        mainHUD = mainHUD != null ? mainHUD : GetComponentInParent<MainHUD>();
        gameMode = gameMode != null ? gameMode : mainHUD.GameMode;
#if UNITY_WEBGL  && !UNITY_EDITOR
        btnQuit.SetActive(false);
#endif
    }
    public void BtnResume()
    {
        mainHUD.BtnMainHudSound();
        mainHUD.OpenMenu(Menu.MAIN, gameObject);
        gameMode.ResumeGame();
    }
    public void BtnSettings()
    {
        mainHUD.BtnMainHudSound();
        mainHUD.OpenMenu(Menu.SETTINGS, gameObject);
    }
}
