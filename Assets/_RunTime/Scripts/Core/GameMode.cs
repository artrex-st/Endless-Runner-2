using System.Collections;
using UnityEngine;

public class GameMode : MonoBehaviour
{
    //Player
    [SerializeField] private PlayerControl player;
    [SerializeField] private PlayerAnimationController playerAnimationController;
    [SerializeField] private MusicController musicController;
    [SerializeField] private MainHUD mainHUD;
    [SerializeField] private SaveGame saveGame;
    [Header("Multipliers")]
    [SerializeField] private float initialSpeed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float timeToMaxSpeed;
    [SerializeField] private float baseScoreMultiplier = 1;
    public ScoreData CurrentSave => saveGame.CurrentScoreData;
    [Header("Scores")]
    private int picUpsCount;
    private float score;
    private float distanceCount;
    public int Score => Mathf.RoundToInt(score);
    public int DistanceCount => Mathf.RoundToInt(distanceCount);
    public int PicUpsCount => picUpsCount;
    //Player END
    //Game Mode
    [SerializeField, Range(0,9f)] float timerToStart;
    public float TimerToStart => timerToStart;
    private bool isDead = false;
    [SerializeField, Range(0,10f)] private float multiplySpeed;
    //Game Mode End
    [SerializeField] private float reloadGameDelay = 3;
    private float startTime;
    public void AddPickUp()
    {
        picUpsCount++;
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
    public void OnGameOver()
    {
        isDead = true;
        musicController.PlayDeathTrackMusic();
        saveGame.SavePlayerData(new ScoreData
        {
            HighestScore = Score > saveGame.CurrentScoreData.HighestScore ? Score : saveGame.CurrentScoreData.HighestScore,
            LastScore = Score,
            TotalPicUps = saveGame.CurrentScoreData.TotalPicUps + PicUpsCount
        });
        StartCoroutine(_ReloadGameCoroutine());
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
        StartCoroutine(StartGameCoroutine());
    }
    
    private void Awake()
    {
        _Initialize();
    }
    private void Update()
    {
        if (_CanPlay())
        {
            _DistanceCalc();
            _SpeedLevelCalc();
        }
    }
    private void _Initialize()
    {
        player.enabled = false;
        saveGame.LoadGame();
    }
    private bool _CanPlay()
    {
        return player.enabled && !isDead;
    }
    private void _SpeedLevelCalc()
    {
        float _percent = (Time.time - startTime) / timeToMaxSpeed;
        player.ForwardSpeed = Mathf.Lerp(initialSpeed, maxSpeed, _percent);
        _ScoreCalc(_percent);
    }
    private void _ScoreCalc(float _Multiply)
    {
        float _extraScore = 1 + _Multiply;
        score += baseScoreMultiplier * player.ForwardSpeed * Time.deltaTime * _extraScore;
    }
    private void _DistanceCalc()
    {
        distanceCount += player.ForwardSpeed * Time.deltaTime;
    }
    private IEnumerator _ReloadGameCoroutine()
    {
        yield return new WaitForSeconds(reloadGameDelay);
        GameManager.instance.ReloadScene((int)SceneIndexes.MAIN_GAME_SCREEN);
    }
    private IEnumerator StartGameCoroutine()
    {
        yield return StartCoroutine(playerAnimationController.PlayStartGameAnimation());
        musicController.PlayMainTrackMusic();
        isDead = false;
        player.enabled = true;
        startTime = Time.time;
    }
}
