using TMPro;
using UnityEngine;
using UnityEngine.UI;

public sealed class SettingOverlay : MonoBehaviour
{
    public delegate void DeletedDataHandler();
    public event DeletedDataHandler OnDeletedData;
    [SerializeField] private MainHUD _mainHUD;
    [SerializeField] private AudioController _audioController;
    [Header("Settings UiOverlay Elements")]
    [SerializeField] private Slider _sliderMaster, _sliderMusic, _sliderSFX;
    
    [Header("Delete Button")]
    [SerializeField] private Button _btnDeleteData;
    [SerializeField] private TextMeshProUGUI _btnTextLabel;
    [SerializeField] private string _textBeforeDelete = "Delete Data?", _textAfterDelete = "Deleted!";

    public void OnMasterVolumeChange(float value)
    {
        _audioController.MasterVolume = value;
    }
    public void OnMusicVolumeChange(float value)
    {
        _audioController.MusicVolume = value;
    }
    public void OnSFXVolumeChange(float value)
    {
        _audioController.SfxVolume = value;
    }
    public void BtnDeleteData()
    {
        _mainHUD.BtnMainHudSound();
        _btnDeleteData.interactable = false;
        _btnTextLabel.text = _textAfterDelete;
        OnDeletedData?.Invoke();
    }
    public void BtnCloseSettings()
    {
        _mainHUD.BtnMainHudSound();
        _mainHUD.OpenMenu(Menu.CLOSE, gameObject);
    }
    private void OnEnable()
    {
        _InitializeOnEnable();
    }
    private void OnDisable()
    {
        _SaveSettingsOnDisable();
    }
    private void _SaveSettingsOnDisable()
    {
        _audioController.SaveAudioSettings();
    }

    private void _InitializeOnEnable()
    {
        _UpdateUi();
        _btnDeleteData.interactable = true;
        _btnTextLabel.text = _textBeforeDelete;
    }

    private void _UpdateUi()
    {
        _sliderMaster.value = _audioController.MasterVolume;
        _sliderMusic.value = _audioController.MusicVolume;
        _sliderSFX.value = _audioController.SfxVolume;
    }
}
