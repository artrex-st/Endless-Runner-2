using UnityEngine;

public class GameData
{
    public int highestScore;
    public int lastScore;
    public int totalPicUps;
}
public class SettingsData
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
    public GameData CurrentSave {get; private set;}
    public SettingsData AudioSettingsData {get; private set;}
    private bool IsLoaded => CurrentSave != null && AudioSettingsData != null;
    //save
    public void SavePlayerData(GameData saveData)
    {
        CurrentSave = saveData;
        PlayerPrefs.SetInt(keyScore, saveData.highestScore);
        PlayerPrefs.SetInt(keyLastScore, saveData.lastScore);
        PlayerPrefs.SetInt(keyMaxCherry, saveData.totalPicUps);
        PlayerPrefs.Save();
    }
    public void SaveSettings(SettingsData saveAudioSettings)
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
        _LoadScore();
        _LoadSetting();
    }
    public void DeleteData()
    {
        SavePlayerData(new GameData());
        PlayerPrefs.DeleteAll();
        LoadGame();
    }  
    private void Awake()
    {
        LoadGame();
    }
    private void _LoadScore()
    {
        CurrentSave = new GameData
        {
            highestScore = PlayerPrefs.GetInt(keyScore, 0),
            totalPicUps = PlayerPrefs.GetInt(keyMaxCherry, 0),
            lastScore = PlayerPrefs.GetInt(keyLastScore, 0)
        };
    }
    private void _LoadSetting()
    {
        AudioSettingsData = new SettingsData
        {
            masterVolume = PlayerPrefs.GetFloat(keyMasterVolume, 1),
            musicVolume = PlayerPrefs.GetFloat(keyMusicVolume, 1),
            sfxVolume = PlayerPrefs.GetFloat(keyEfxVolume, 1)
        };
    }
}