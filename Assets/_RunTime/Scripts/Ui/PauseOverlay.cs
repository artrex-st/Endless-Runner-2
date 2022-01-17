using UnityEngine;

public class PauseOverlay : MonoBehaviour
{
    private MainHUD mainHUD;
    private GameMode gameMode;
    [SerializeField] private GameObject btnQuit;
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
    private void Awake()
    {
        _Initialize();
    }
    private void _Initialize()
    {
#if UNITY_WEBGL  && !UNITY_EDITOR
        btnQuit.SetActive(false);
#endif  
    }
}
