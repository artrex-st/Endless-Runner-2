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
    private const int minVolumeDb = -60;
    private const int maxVolumeDb = 0;
    private void Start()
    {
        LoadAudioSettings();
    }
    //
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
    //
    private void SetMixerVolumeParameter(string key, float volumePercent)
    {
        float volume = Mathf.Lerp(minVolumeDb,maxVolumeDb, volumePercent);
        mixerAudio.SetFloat(key, volume);
    }
    private float GetMixerVolumeParameter(string key)
    {
        if (mixerAudio.GetFloat(key, out var value))
        {
            return Mathf.InverseLerp(minVolumeDb,maxVolumeDb,value);
        }
        return 0.85f;
    }
    // Save settings
    public void SaveAudioSettings()
    {
        gameSaver.SaveSettings(new AudioSettingsData
        {
            masterVolume = MasterVolume,
            musicVolume = MusicVolume,
            sfxVolume = SfxVolume,
        });
    }
    // Load Settings
    private void LoadAudioSettings()
    {
        gameSaver.LoadGame();
        MasterVolume = gameSaver.AudioSettingsData.masterVolume;
        MusicVolume = gameSaver.AudioSettingsData.musicVolume;
        SfxVolume = gameSaver.AudioSettingsData.sfxVolume;
    }
}
