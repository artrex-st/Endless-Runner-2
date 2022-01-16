using System.Collections;
using UnityEngine;

public class GameMode : MonoBehaviour
{
    //Player
    [SerializeField] private PlayerControl player;
    [SerializeField] private PlayerAnimationController playerAnimationController;
    [SerializeField] private MusicController musicController;
    [SerializeField] private SaveGame saveGame;
    [Header("Multipliers")]
    [SerializeField] private float initialSpeed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float timeToMaxSpeed;
    [SerializeField] private float baseScoreMultiplier = 1;
    [Header("Scores")]
    public SaveGameData dataLoaded;
    private int cherryPicUpCount;
    private float score;
    private float distanceCount;
    public int Score => Mathf.RoundToInt(score);
    public int DistanceCount => Mathf.RoundToInt(distanceCount);
    public int CherryPicUpCount => cherryPicUpCount;
    //Player END
    //Game Mode
    [SerializeField, Range(0,9f)] float timerToStart;
    public float TimerToStart => timerToStart;
    private bool isDead = false;
    public bool IsDead => isDead;
    [SerializeField, Range(0,10f)] private float multiplySpeed;
    //Game Mode End
    [SerializeField] private float reloadGameDelay = 3;
    private float startTime;

    private void Awake()
    {
        player.enabled = false;
        saveGame.LoadGame();
    }
    private void Update()
    {
        if (CanPlay())
        {
            DistanceCalc();
            SpeedLevelCalc();
        }
        else
        {
            if(!player.isActiveAndEnabled)
                player.enabled = playerAnimationController.EndStartAnimation();
            startTime = Time.time;
        }
    }
    private bool CanPlay()
    {
        return player.isActiveAndEnabled && !isDead;
    }
    private void SpeedLevelCalc()
    {
        float _percent = (Time.time - startTime) / timeToMaxSpeed;
        player.ForwardSpeed = Mathf.Lerp(initialSpeed, maxSpeed, _percent);
        ScoreCalc(_percent);
    }
    public void OnGameOver()
    {
        isDead = true;
        musicController.PlayDeathTrackMusic();
        if (saveGame.CurrentSave.highestScore < Score)
            saveGame.CurrentSave.highestScore = Score;
        saveGame.CurrentSave.totalCherry += CherryPicUpCount;
        saveGame.CurrentSave.lastScore = Score;
        saveGame.SavePlayerData(saveGame.CurrentSave);
        StartCoroutine(ReloadGameCoroutine());
    }
    private void ScoreCalc(float _Multiply)
    {
        float _extraScore = 1 + _Multiply;
        score += baseScoreMultiplier * player.ForwardSpeed * Time.deltaTime * _extraScore;
    }
    private void DistanceCalc()
    {
        distanceCount += player.ForwardSpeed * Time.deltaTime;
    }
    public void PauseGame()
    {
        Time.timeScale = 0f;
    }
    public void ResumeGame()
    {
        Time.timeScale = 1f;
    }
    public void StartGame()
    {
        playerAnimationController.SetStartTriggerAnimation();
        isDead = false;
        musicController.PlayMainTrackMusic();
    }
    private IEnumerator ReloadGameCoroutine()
    {
        yield return new WaitForSeconds(reloadGameDelay);
        GameManager.instance.ReloadScene((int)SceneIndexes.MAIN_GAME_SCREEN);
    }
    public void AddPickUp()
    {
        cherryPicUpCount++;
    }
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_WEBGL
        Debug.Log("'WebGL Player' não pode ser fechado"); //para evitar
#else
        Application.Quit();
#endif
    }
    public SaveGameData GetLoadedData()
    {
        return saveGame.CurrentSave;
    }
}
