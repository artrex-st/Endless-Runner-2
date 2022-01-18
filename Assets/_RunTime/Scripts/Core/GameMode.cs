using System.Collections;
using UnityEngine;

public class GameMode : MonoBehaviour
{
    public int Score => Mathf.RoundToInt(score);
    public int DistanceCount => Mathf.RoundToInt(distanceCount);
    public int PicUpsCount => picUpsCount;
    public float _TimerToStartRun => gameModeConfig.timerToStartRun;
    public ScoreData CurrentSave => saveGame.CurrentScoreData;
    [SerializeField] private PlayerControl player;
    [SerializeField] private PlayerAnimationController playerAnimationController;
    [SerializeField] private MusicController musicController;
    [SerializeField] private SaveGame saveGame;
    [SerializeField, Header("Game Configurations")] private GameModeConfig gameModeConfig;
    private int picUpsCount;
    private float score, distanceCount;
    private float startTime;
    private bool isDead = false;
    private float _InitialSpeed => gameModeConfig.initialSpeed;
    private float _MaximumSpeed => gameModeConfig.maximumSpeed;
    private float _TimeToMaximumSpeed => gameModeConfig.timeToMaximumSpeed;
    private float _BaseScoreMultiplier => gameModeConfig.baseScoreMultiplier;
    private float _ReloadGameDelay => gameModeConfig.reloadGameDelay;
    private float _ScoreValueSpeed => gameModeConfig.scoreValueSpeed;

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
            HighestScore = Score > CurrentSave.HighestScore ? Score : CurrentSave.HighestScore,
            LastScore = Score,
            TotalPicUps = CurrentSave.TotalPicUps + PicUpsCount
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
    public void OnColision(Collider other)
    {
        if (other.TryGetComponent(out Obstacle obstacle))
        {
            OnGameOver();
            player.Die();
            obstacle.PlayCollisionFeedBack(other);
        }
        if (other.TryGetComponent(out PicUp picUp))
        {
            AddPickUp();
            picUp.OnPic();
        }
    }
    public ScoreData OnCalledScoreData()
    {
        return CurrentSave;
    }

    private void Awake()
    {
        _Initialize();
    }
    private void Update()
    {
        if (_CanPlay())
        {
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
        float percent = (Time.time - startTime) / _TimeToMaximumSpeed;
        player.ForwardSpeed = Mathf.Lerp(_InitialSpeed, _MaximumSpeed, percent);
        _DistanceCalc();
        _ScoreCalc(percent);
    }
    private void _ScoreCalc(float _Multiply)
    {
        float _extraScore = _ScoreValueSpeed + _Multiply;
        score += _BaseScoreMultiplier * player.ForwardSpeed * Time.deltaTime * _extraScore;
    }
    private void _DistanceCalc()
    {
        distanceCount += player.ForwardSpeed * Time.deltaTime;
    }
    private IEnumerator _ReloadGameCoroutine()
    {
        yield return new WaitForSeconds(_ReloadGameDelay);
#if UNITY_EDITOR
        if (GameManager.instance == null)
        {
            Debug.LogWarning($"To reload the game there must be a GameManager instance of the {SceneIndexes.MANAGER}");
        }
#else 
        GameManager.instance.ReloadScene((int)SceneIndexes.MAIN_GAME_SCREEN);
#endif
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
