using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainOverlay : MonoBehaviour
{
    [SerializeField] private MainHUD mainHUD;
    [SerializeField] private GameMode gameMode;
    [SerializeField] private MainHudSound mainHudSound;
    [Header("Ui Elements")]
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI distanceText;
    [SerializeField] private TextMeshProUGUI countDownText;
    [SerializeField] private TextMeshProUGUI cherryCountText;
    [SerializeField] private Button pauseBtn;
    private void Awake()
    {
        Initialize();
    }
    private void Start()
    {
        StartCoroutine(StartGameCoroutine(Mathf.RoundToInt(gameMode.TimerToStart)));
    }
    private void Initialize()
    {
        mainHUD = mainHUD != null ? mainHUD : GetComponentInParent<MainHUD>();
        mainHudSound = mainHudSound != null ? mainHudSound : GetComponentInParent<MainHudSound>();
        gameMode = gameMode != null ? gameMode : mainHUD.GameMode;
        pauseBtn = pauseBtn != null ? pauseBtn : GetComponentInChildren<Button>();
        pauseBtn.interactable = false;
    }
    public void BtnPause()
    {
        mainHUD.BtnMainHudSound();
        mainHUD.OpenMenu(Menu.PAUSE, gameObject);
        gameMode.PauseGame();
    }
    private void LateUpdate()
    {
        scoreText.text = $"Score: {gameMode.Score}.";
        distanceText.text = $"{gameMode.DistanceCount} M.";
        cherryCountText.text = $"{gameMode.CherryPicUpCount}";
    }
    private IEnumerator StartGameCoroutine(int _countdownSeconds)
    {
        countDownText.gameObject.SetActive(false);
        if (_countdownSeconds <= 0)
        {
            yield break;
        }
        //tempo desde que a gente comecou o jogo
        float _timeToStart = Time.time + _countdownSeconds;
        countDownText.gameObject.SetActive(true);
        int _lastRemainingTime = 0;
        while (Time.time <= _timeToStart)
        {
            float remainingTime = _timeToStart - Time.time;
            int _remainingTimeInt = Mathf.FloorToInt(remainingTime);
            countDownText.text = $"{_remainingTimeInt + 1}";
            
            if (_remainingTimeInt != _lastRemainingTime)
            {
                mainHudSound.PlayCountDownSound(_remainingTimeInt + 1);
                _lastRemainingTime = _remainingTimeInt;
            }

            float _percent = remainingTime - _remainingTimeInt;
            countDownText.transform.localScale = Vector3.Lerp(Vector3.one, Vector3.zero, _percent);
            // isso que faz o update rodar mesmo sem terminar o IEnumerator
            yield return null;
        }
        mainHudSound.PlayCountDownEndSound();
        countDownText.gameObject.SetActive(false);
        gameMode.StartGame();
        pauseBtn.interactable = true;
    }
}
