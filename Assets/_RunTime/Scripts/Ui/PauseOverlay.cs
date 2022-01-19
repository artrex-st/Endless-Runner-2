using UnityEngine;

public class PauseOverlay : MonoBehaviour
{
    [SerializeField] private MainHUD _mainHUD;
    [SerializeField] private GameObject _btnQuit;

    public PauseOverlay(MainHUD mainHUD, GameObject btnQuit)
    {
        _mainHUD = mainHUD;
        _btnQuit = btnQuit;
        _Initialize();
    }

    public void BtnResume()
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
    private void _Initialize()
    {
#if UNITY_WEBGL  && !UNITY_EDITOR
        btnQuit.SetActive(false);
#endif  
    }
}
