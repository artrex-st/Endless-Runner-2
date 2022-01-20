using UnityEngine;
using UnityEngine.Audio;

public class AudioController : MonoBehaviour
{
    public delegate void AudioSaveHandler(SettingsData settingsData);
    public event AudioSaveHandler OnSavedSettingsData;
    public delegate SettingsData AudioLoadHandler();
    public event AudioLoadHandler OnLoadingSettingsData;

    [SerializeField] private AudioMixer _mixerAudio;
    private const string _masterVolume = "Master", _musicVolume = "Music", _sfxVolume = "SFX";
    private const float _minimumVolumeDb = -50, _maximumVolumeDb = 0, _defaultSliderDbValue = 0.85f, _mutedDbValue = -80f;

    public AudioController(AudioMixer mixerAudio)
    {
        _mixerAudio = mixerAudio;
        _LoadAudioSettings();
    }

    public float MasterVolume
    {
        get => _GetMixerVolumeParameter(_masterVolume);
        set => _SetMixerVolumeParameter(_masterVolume, value);
    }
    public float MusicVolume
    {
        get => _GetMixerVolumeParameter(_musicVolume);
        set => _SetMixerVolumeParameter(_musicVolume, value);
    }
    public float SfxVolume
    {
        get => _GetMixerVolumeParameter(_sfxVolume);
        set => _SetMixerVolumeParameter(_sfxVolume, value);
    }
    public void SaveAudioSettings()
    {
        OnSavedSettingsData?.Invoke(new SettingsData
        {
            masterVolume = MasterVolume,
            musicVolume = MusicVolume,
            sfxVolume = SfxVolume,
        });
    }

    private void Start()
    {
        _LoadAudioSettings();
    }
    private void _SetMixerVolumeParameter(string key, float volumePercent)
    {
        float volume = Mathf.Lerp(_minimumVolumeDb, _maximumVolumeDb, volumePercent);
        volume = volume <= _minimumVolumeDb ? _mutedDbValue : volume;
        _mixerAudio.SetFloat(key, volume);
    }
    private float _GetMixerVolumeParameter(string key)
    {
        if (_mixerAudio.GetFloat(key, out var value))
        {
            return Mathf.InverseLerp(_minimumVolumeDb,_maximumVolumeDb,value);
        }
        return _defaultSliderDbValue;
    }
    private void _LoadAudioSettings()
    {
        SettingsData currentSettingsLoaded = new SettingsData();
        currentSettingsLoaded = OnLoadingSettingsData?.Invoke();
        MasterVolume = currentSettingsLoaded.masterVolume;
        MusicVolume = currentSettingsLoaded.musicVolume;
        SfxVolume = currentSettingsLoaded.sfxVolume;
    }
}
