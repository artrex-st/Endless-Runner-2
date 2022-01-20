using UnityEngine;

public class ScoreData
{
    public int TotalPicUps;
    public float HighestScore, LastScore;
}
public class SettingsData
{
    public float masterVolume = 0.5f, musicVolume = 0.5f, sfxVolume = 0.5f;
}

public sealed class SaveGame : MonoBehaviour
{
    public const string keyScore = "Score", 
                        keyLastScore = "LastScore", 
                        keyMaxCherry = "MaxCherry", 
                        keyMasterVolume = "masterVolume", 
                        keyMusicVolume = "musicVolume", 
                        keyEfxVolume = "sfxVolume",
                        scoreDataFileName = "SaveScore",
                        settingsDataFileName = "SaveSettings";
    public ScoreData CurrentScoreData {get; private set;}
    public SettingsData CurrentSettingsData {get; private set;}
    private bool IsLoaded => CurrentScoreData != null && CurrentSettingsData != null;
    public void OnSavedScoreData(ScoreData scoreData)
    {
        CurrentScoreData = scoreData;
        string jsonString = JsonUtility.ToJson(CurrentScoreData);
        SaveSystem.Save(jsonString,scoreDataFileName);
    }
    public void OnSavedSettingsData(SettingsData settingsData)
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
    public void OnDeletedData()
    {
        OnSavedScoreData(new ScoreData());
        LoadGame();
    }
    public ScoreData OnLoadingScoreData()
    {
        return CurrentScoreData;
    }
    public SettingsData OnLoadingSettingsData()
    {
        return CurrentSettingsData;
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
            OnSavedScoreData(new ScoreData());
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