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
        _Initialize();
    }
    private void LateUpdate()
    {
        scoreText.text = $"Score: {gameMode.Score}.";
        distanceText.text = $"{gameMode.DistanceCount} M.";
        cherryCountText.text = $"{gameMode.PicUpsCount}";
    }
    
    public void BtnPause()
    {
        mainHUD.BtnMainHudSound();
        mainHUD.OpenMenu(Menu.PAUSE, gameObject);
        gameMode.PauseGame();
    }
    private void _Initialize()
    {
        pauseBtn.interactable = false;
        StartCoroutine(_StartGameCoroutine(Mathf.RoundToInt(gameMode._TimerToStartRun)));
    }
    private IEnumerator _StartGameCoroutine(int countdownSeconds)
    {
        countDownText.gameObject.SetActive(false);
        if (countdownSeconds <= 0)
        {
            yield break;
        }
        float _timeToStart = Time.time + countdownSeconds;
        yield return null;
        countDownText.gameObject.SetActive(true);
        int _lastRemainingTime = countdownSeconds;
        
        while (Time.time <= _timeToStart)
        {
            float remainingTime = _timeToStart - Time.time;
            int _remainingTimeInt = Mathf.FloorToInt(remainingTime);
            countDownText.text = $"{_remainingTimeInt + 1}";
            
            if (_lastRemainingTime != _remainingTimeInt)
            {
                mainHudSound.PlayCountDownSound(_remainingTimeInt + 1);
            }

            _lastRemainingTime = _remainingTimeInt;
            float _percent = remainingTime - _remainingTimeInt;
            countDownText.transform.localScale = Vector3.Lerp(Vector3.one, Vector3.zero, _percent);
            yield return null;
        }
        countDownText.gameObject.SetActive(false);
        mainHudSound.PlayCountDownEndSound();
        gameMode.StartGame();
        pauseBtn.interactable = true;
    }
}
