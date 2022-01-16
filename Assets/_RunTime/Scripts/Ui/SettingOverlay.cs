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
    
    [Header("Delete Button")]
    [SerializeField] private Button btnDeleteData;
    [SerializeField] private TextMeshProUGUI btnTextLabel;
    [SerializeField] private string textBeforeDelete = "Delete Data?";
    [SerializeField] private string textAfterDelete = "Deleted!";

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

    public void BtnDeleteData()
    {
        mainHUD.BtnMainHudSound();
        btnDeleteData.interactable = false;
        btnTextLabel.text = textAfterDelete;
        saveGame.DeleteData();
    }
    public void BtnCloseSettings()
    {
        mainHUD.BtnMainHudSound();
        mainHUD.OpenMenu(Menu.CLOSE, gameObject);
    }

    private void Awake()
    {
        Initialize();
    }
    private void OnEnable()
    {
        InitializeOnEnable();
    }
    private void OnDisable()
    {
        audioController.SaveAudioSettings();
    }
    private void Initialize()
    {
        mainHUD = mainHUD != null ? mainHUD : GetComponentInParent<MainHUD>(); 
    }
    private void InitializeOnEnable()
    {
        UpdateUi();
        btnDeleteData.interactable = true;
        btnTextLabel.text = textBeforeDelete;
    }

    private void UpdateUi()
    {
        sliderMaster.value = audioController.MasterVolume;
        sliderMusic.value = audioController.MusicVolume;
        sliderSFX.value = audioController.SfxVolume;
    }
}
