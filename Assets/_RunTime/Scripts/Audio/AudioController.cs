using UnityEngine;
using UnityEngine.Audio;

public class AudioController : MonoBehaviour
{
    [SerializeField] private SaveGame _gameSaver;
    [SerializeField] private AudioMixer _mixerAudio;
    //constants
    private const string masterVolume = "Master", musicVolume = "Music", sfxVolume = "SFX";
    private const float minVolumeDb = -50, maxVolumeDb = 0, defaultSliderDbValue = 0.85f, mutedDbValue = -80f;

    public AudioController(SaveGame gameSaver, AudioMixer mixerAudio)
    {
        _gameSaver = gameSaver;
        _mixerAudio = mixerAudio;
        _LoadAudioSettings();
    }

    public float MasterVolume
    {
        get => _GetMixerVolumeParameter(masterVolume);
        set => _SetMixerVolumeParameter(masterVolume, value);
    }
    public float MusicVolume
    {
        get => _GetMixerVolumeParameter(musicVolume);
        set => _SetMixerVolumeParameter(musicVolume, value);
    }
    public float SfxVolume
    {
        get => _GetMixerVolumeParameter(sfxVolume);
        set => _SetMixerVolumeParameter(sfxVolume, value);
    }
    public void SaveAudioSettings()
    {
        _gameSaver.SaveSettings(new SettingsData
        {
            masterVolume = MasterVolume,
            musicVolume = MusicVolume,
            sfxVolume = SfxVolume,
        });
    }
    //
    private void Start()
    {
        _LoadAudioSettings();
    }
    //
    private void _SetMixerVolumeParameter(string key, float volumePercent)
    {
        float volume = Mathf.Lerp(minVolumeDb, maxVolumeDb, volumePercent);
        volume = volume <= minVolumeDb ? mutedDbValue : volume;
        _mixerAudio.SetFloat(key, volume);
    }
    private float _GetMixerVolumeParameter(string key)
    {
        if (_mixerAudio.GetFloat(key, out var value))
        {
            return Mathf.InverseLerp(minVolumeDb,maxVolumeDb,value);
        }
        return defaultSliderDbValue;
    }
    private void _LoadAudioSettings()
    {
        _gameSaver.LoadGame();
        MasterVolume = _gameSaver.CurrentSettingsData.masterVolume;
        MusicVolume = _gameSaver.CurrentSettingsData.musicVolume;
        SfxVolume = _gameSaver.CurrentSettingsData.sfxVolume;
    }
}
