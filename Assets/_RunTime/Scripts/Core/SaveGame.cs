using UnityEngine;

public class ScoreData
{
    public int HighestScore;
    public int LastScore;
    public int TotalPicUps;
}
public class SettingsData
{
    public float masterVolume = 0.5f;
    public float musicVolume = 0.5f;
    public float sfxVolume = 0.5f;
}
public class SaveGame : MonoBehaviour
{
    public const string keyScore = "Score";
    public const string keyLastScore = "LastScore";
    public const string keyMaxCherry = "MaxCherry";
    public const string keyMasterVolume = "masterVolume";
    public const string keyMusicVolume = "musicVolume";
    public const string keyEfxVolume = "sfxVolume";
    private const string scoreDataFileName = "SaveScore";
    private const string settingsDataFileName = "SaveSettings";
    // variaveis
    public ScoreData CurrentScoreData {get; private set;}
    public SettingsData CurrentSettingsData {get; private set;}
    private bool IsLoaded => CurrentScoreData != null && CurrentSettingsData != null;
    //save
    public void SavePlayerData(ScoreData scoreData)
    {
        CurrentScoreData = scoreData;
        // json
        string jsonString = JsonUtility.ToJson(CurrentScoreData);
        SaveSystem.Save(jsonString,scoreDataFileName);
    }
    public void SaveSettings(SettingsData settingsData)
    {
        CurrentSettingsData = settingsData;
        string jsonString = JsonUtility.ToJson(CurrentSettingsData);
        SaveSystem.Save(jsonString,settingsDataFileName);
    }
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
        SavePlayerData(new ScoreData());
        LoadGame();
    }  
    private void Awake()
    {
        _Initialize();
    }
    private void _Initialize()
    {
        LoadGame();
    }
    private void _LoadScore()
    {
        string jsonString = SaveSystem.Load(scoreDataFileName);
        if (jsonString != null)
        {
            CurrentScoreData = JsonUtility.FromJson<ScoreData>(jsonString);
        }
        else
        {
            SavePlayerData(new ScoreData());
        }
    }
    private void _LoadSetting()
    {
        string jsonString = SaveSystem.Load(settingsDataFileName);
        if (jsonString != null)
        {
            CurrentSettingsData = JsonUtility.FromJson<SettingsData>(jsonString);
        }
        else
        {
            CurrentSettingsData = new SettingsData();
        }
    }
}