using System.Collections;
using UnityEngine;

public sealed class GameMode : MonoBehaviour
{
    public int Score => Mathf.RoundToInt(score);
    public int DistanceCount => Mathf.RoundToInt(distanceCount);
    public int PicUpsCount => picUpsCount;
    private ScoreData _CurrentSave => saveGame.CurrentScoreData;
    [SerializeField] private PlayerControl player;
    [SerializeField] private PlayerAnimationController playerAnimationController;
    [SerializeField] private MusicController musicController;
    [SerializeField] private SaveGame saveGame;
    [SerializeField, Header("Game Configurations")] private GameModeConfig gameModeConfig;
    private int picUpsCount;
    private float score, distanceCount;
    private float startTime;
    private bool isDead = false;

    public GameMode(PlayerControl player, PlayerAnimationController playerAnimationController, MusicController musicController, SaveGame saveGame, GameModeConfig gameModeConfig)
    {
        this.player = player;
        this.playerAnimationController = playerAnimationController;
        this.musicController = musicController;
        this.saveGame = saveGame;
        this.gameModeConfig = gameModeConfig;
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
        musicController.PlayDeathTrackMusic();
        
        saveGame.SavePlayerData(new ScoreData
        {
            HighestScore = Score > _CurrentSave.HighestScore ? Score : _CurrentSave.HighestScore,
            LastScore = Score,
            TotalPicUps = _CurrentSave.TotalPicUps + PicUpsCount
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
            timerToStartRun = gameModeConfig.timerToStartRun,
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
            player.Die();
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
        player.enabled = false;
        saveGame.LoadGame();
    }
    private bool _CanPlay()
    {
        return player.enabled && !isDead;
    }

    private void _SpeedLevelCalc()
    {
        float percent = (Time.time - startTime) / gameModeConfig.timeToMaximumSpeed;
        player.ForwardSpeed = Mathf.Lerp(gameModeConfig.initialSpeed, gameModeConfig.maximumSpeed, percent);
        distanceCount = player.transform.position.z;
        
        _ScoreCalc(percent);
    }
    private void _ScoreCalc(float _Multiply)
    {
        float _extraScore = gameModeConfig.scoreByDistanceValue + _Multiply;
        score += gameModeConfig.baseScoreMultiplier * player.ForwardSpeed * Time.deltaTime * _extraScore;
    }
    private IEnumerator _ReloadGameCoroutine()
    {
        yield return new WaitForSeconds(gameModeConfig.reloadGameDelay);

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
        yield return StartCoroutine(playerAnimationController.PlayStartGameAnimation());
        musicController.PlayMainTrackMusic();
        isDead = false;
        player.enabled = true;
        startTime = Time.time;
    }
}
