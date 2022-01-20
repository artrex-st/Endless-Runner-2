using System.Collections;
using UnityEngine;

public sealed class GameMode : MonoBehaviour
{
    public delegate ScoreData ScoreLoadHandler();
    public event ScoreLoadHandler OnLoadingScoreData;
    public delegate void ScoreSaveHandler(ScoreData scoreData);
    public event ScoreSaveHandler OnSavedScoreData;
    public delegate IEnumerator PlayerAnimationHandler();
    public event PlayerAnimationHandler OnStartedGameAnimation;
    
    public int Score => Mathf.RoundToInt(score);
    public int DistanceCount => Mathf.RoundToInt(distanceCount);
    public int PicUpsCount => picUpsCount;
    [SerializeField] private PlayerControl _player;
    [SerializeField] private MusicController _musicController;
    [SerializeField, Header("Game Configurations")] private GameModeConfig _gameModeConfig;
    private int picUpsCount;
    private float score, distanceCount;
    private float startTime;
    private bool isDead = false;
    private ScoreData _currentScore = new ScoreData();

    public GameMode(PlayerControl player , MusicController musicController, GameModeConfig gameModeConfig)
    {
        _player = player;
        _musicController = musicController;
        _gameModeConfig = gameModeConfig;
        _Initialize();
    }

    public void AddPickUp()
    {
        picUpsCount++;
    }
    public void OnClickedQuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_WEBGL
        Debug.Log("'WebGL Player' cannot be closed");
#else
        Application.Quit();
#endif
    }
    public void OnGameOver()
    {
        isDead = true;
        _musicController.PlayDeathTrackMusic();
        _currentScore = OnLoadingScoreData?.Invoke();
        OnSavedScoreData?.Invoke(new ScoreData
        {
            HighestScore = Score > _currentScore.HighestScore ? Score : _currentScore.HighestScore,
            LastScore = Score,
            TotalPicUps = _currentScore.TotalPicUps + PicUpsCount
        });

        StartCoroutine(_ReloadGameCoroutine());
    }
    public void OnPauseGame()
    {
        Time.timeScale = 0f;
    }
    public void OnResumeGame()
    {
        Time.timeScale = 1f;
    }
    public void OnStartedRun()
    {
        StartCoroutine(StartGameCoroutine());
    }
    public OverlayStatus OnOverlayStatusCalled()
    {
        OverlayStatus overlayStatus = new OverlayStatus
        {
            timerToStartRun = _gameModeConfig.timerToStartRun,
            score = Score,
            distanceCount = DistanceCount,
            picUpsCount = PicUpsCount
        };

        return overlayStatus;
    }
    public void OnColision(Collider other)
    {
        if (other.TryGetComponent(out Obstacle obstacle))
        {
            OnGameOver();
            _player.Die();
            obstacle.PlayCollisionFeedBack(other);
        }

        if (other.TryGetComponent(out PicUp picUp))
        {
            AddPickUp();
            picUp.OnPic();
        }
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
        _player.enabled = false;
    }
    private bool _CanPlay()
    {
        return _player.enabled && !isDead;
    }

    private void _SpeedLevelCalc()
    {
        float percent = (Time.time - startTime) / _gameModeConfig.timeToMaximumSpeed;
        _player.ForwardSpeed = Mathf.Lerp(_gameModeConfig.initialSpeed, _gameModeConfig.maximumSpeed, percent);
        distanceCount = _player.transform.position.z;
        
        _ScoreCalc(percent);
    }
    private void _ScoreCalc(float _Multiply)
    {
        float _extraScore = _gameModeConfig.scoreByDistanceValue + _Multiply;
        score += _gameModeConfig.baseScoreMultiplier * _player.ForwardSpeed * Time.deltaTime * _extraScore;
    }
    private IEnumerator _ReloadGameCoroutine()
    {
        yield return new WaitForSeconds(_gameModeConfig.reloadGameDelay);

#if UNITY_EDITOR
        if (GameManager.instance == null)
        {
            Debug.LogWarning($"To reload the game there must be a GameManager instance of the Scene index:{(int)SceneIndexes.MANAGER}");
        }
        else
        {
            GameManager.instance.ReloadScene((int)SceneIndexes.MAIN_GAME_SCREEN);
        }
#else 
        GameManager.instance.ReloadScene((int)SceneIndexes.MAIN_GAME_SCREEN);
#endif
    }
    private IEnumerator StartGameCoroutine()
    {
        yield return StartCoroutine(OnStartedGameAnimation?.Invoke());
        _musicController.PlayMainTrackMusic();
        isDead = false;
        _player.enabled = true;
        startTime = Time.time;
    }
}
