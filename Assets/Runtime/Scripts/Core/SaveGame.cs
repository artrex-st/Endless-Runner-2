using UnityEngine;

public class SaveGameData
{
    public int highestScore;
    public int lastScore;
    public int totalCherry;
}
public class AudioSettingsData
{
    public float masterVolume;
    public float musicVolume;
    public float sfxVolume;
}
public class SaveGame : MonoBehaviour
{
    public const string keyScore = "Score";
    public const string keyLastScore = "LastScore";
    public const string keyMaxCherry = "MaxCherry";
    public const string keyMasterVolume = "masterVolume";
    public const string keyMusicVolume = "musicVolume";
    public const string keyEfxVolume = "sfxVolume";
    // variaveis
    public SaveGameData CurrentSave {get; private set;}
    public AudioSettingsData AudioSettingsData {get; private set;}
    private bool IsLoaded => CurrentSave != null && AudioSettingsData != null;
    private void Awake()
    {
        LoadGame();
    }
    //save
    public void SavePlayerData(SaveGameData saveData)
    {
        CurrentSave = saveData;
        PlayerPrefs.SetInt(keyScore, saveData.highestScore);
        PlayerPrefs.SetInt(keyLastScore, saveData.lastScore);
        PlayerPrefs.SetInt(keyMaxCherry, saveData.totalCherry);
        PlayerPrefs.Save();
    }
    public void SaveSettings(AudioSettingsData saveAudioSettings)
    {
        AudioSettingsData = saveAudioSettings;
        PlayerPrefs.SetFloat(keyMasterVolume,AudioSettingsData.masterVolume);
        PlayerPrefs.SetFloat(keyMusicVolume,AudioSettingsData.musicVolume);
        PlayerPrefs.SetFloat(keyEfxVolume,AudioSettingsData.sfxVolume);
        PlayerPrefs.Save();
    }
    //load
    public void LoadGame()
    {
        if (IsLoaded)
        {
            return;
        }
        CurrentSave = new SaveGameData
        {
            highestScore = PlayerPrefs.GetInt(keyScore, 0),
            totalCherry = PlayerPrefs.GetInt(keyMaxCherry, 0),
            lastScore = PlayerPrefs.GetInt(keyLastScore, 0)
        };
        AudioSettingsData = new AudioSettingsData
        {
            masterVolume = PlayerPrefs.GetFloat(keyMasterVolume, 1),
            musicVolume = PlayerPrefs.GetFloat(keyMusicVolume, 1),
            sfxVolume = PlayerPrefs.GetFloat(keyEfxVolume, 1)
        };
    }
    // DELETE ALL
    public void DeleteData()
    {
        SavePlayerData(new SaveGameData());
        PlayerPrefs.DeleteAll();
        LoadGame();
    }  
}