using UnityEngine;
using UnityEngine.Audio;

public class AudioController : MonoBehaviour
{
    [SerializeField] private SaveGame gameSaver;
    [SerializeField] private AudioMixer mixerAudio;
    //constants
    private const string masterVolume = "Master";
    private const string musicVolume = "Music";
    private const string sfxVolume = "SFX";
    //volume range
    private const int minVolumeDb = -40;
    private const int maxVolumeDb = 0;
    private const float defaultSliderDbValue = 0.85f;
    private const float mutedDbValue = -80f;
    public float MasterVolume
    {
        get => GetMixerVolumeParameter(masterVolume);
        set => SetMixerVolumeParameter(masterVolume, value);
    }
    public float MusicVolume
    {
        get => GetMixerVolumeParameter(musicVolume);
        set => SetMixerVolumeParameter(musicVolume, value);
    }
    public float SfxVolume
    {
        get => GetMixerVolumeParameter(sfxVolume);
        set => SetMixerVolumeParameter(sfxVolume, value);
    }
    public void SaveAudioSettings()
    {
        gameSaver.SaveSettings(new SettingsData
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
    private void SetMixerVolumeParameter(string key, float volumePercent)
    {
        float volume = Mathf.Lerp(minVolumeDb, maxVolumeDb, volumePercent) <= minVolumeDb ? mutedDbValue : Mathf.Lerp(minVolumeDb, maxVolumeDb, volumePercent);
        //volume = Mathf.Log10(volumePercent) * 20;
        mixerAudio.SetFloat(key, volume);
        
    }
    private float GetMixerVolumeParameter(string key)
    {
        if (mixerAudio.GetFloat(key, out var value))
        {
            return Mathf.InverseLerp(minVolumeDb,maxVolumeDb,value);
        }
        return defaultSliderDbValue;
    }
    private void _LoadAudioSettings()
    {
        gameSaver.LoadGame();
        MasterVolume = gameSaver.CurrentSettingsData.masterVolume;
        MusicVolume = gameSaver.CurrentSettingsData.musicVolume;
        SfxVolume = gameSaver.CurrentSettingsData.sfxVolume;
    }
}
