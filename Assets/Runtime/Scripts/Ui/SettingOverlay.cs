using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingOverlay : MonoBehaviour
{
    [SerializeField] private MainHUD mainHUD;
    [SerializeField] private SaveGame saveGame;
    [SerializeField] private AudioController audioController;
    [Header("Settings UiOverlay Elements")]
    [SerializeField] private Slider sliderMaster;
    [SerializeField] private Slider sliderMusic;
    [SerializeField] private Slider sliderSFX;
    [SerializeField] private Button btnDeleteData;

    private void Awake()
    {
        mainHUD = mainHUD != null ? mainHUD : GetComponentInParent<MainHUD>();
    }
    private void OnEnable()
    {
        UpdateUi();
        btnDeleteData.interactable = true;
    }
    private void OnDisable()
    {
        audioController.SaveAudioSettings();
    }
    //call backs
    public void OnMasterVolumeChange(float value)
    {
        audioController.MasterVolume = value;
    }
    public void OnMusicVolumeChange(float value)
    {
        audioController.MusicVolume = value;
    }
    public void OnSFXVolumeChange(float value)
    {
        audioController.SfxVolume = value;
    }
    //
    public void BtnDeleteData()
    {
        mainHUD.BtnMainHudSound();
        btnDeleteData.interactable = false;
        saveGame.DeleteData();
    }
    public void BtnCloseSettings()
    {
        mainHUD.BtnMainHudSound();
        mainHUD.OpenMenu(Menu.CLOSE, gameObject);
    }
    private void UpdateUi()
    {
        sliderMaster.value = audioController.MasterVolume;
        sliderMusic.value = audioController.MusicVolume;
        sliderSFX.value = audioController.SfxVolume;
    }
}
