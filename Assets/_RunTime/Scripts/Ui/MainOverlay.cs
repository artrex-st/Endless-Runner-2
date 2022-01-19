using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public sealed class MainOverlay : MonoBehaviour
{
    public delegate void MainOverlayStartHandler();
    public event MainOverlayStartHandler OnStartedRun;

    public delegate OverlayStatus MainOverlayStatusHandler();
    public event MainOverlayStatusHandler OnOverlayStatusCalled;

    [SerializeField] private MainHUD _mainHUD;
    [SerializeField] private MainHudSound _mainHudSound;
    [Header("Ui Elements")]
    [SerializeField] private TextMeshProUGUI _scoreText, _distanceText, _countDownText, _picUpCountText;
    [SerializeField] private Button _pauseBtn;
    private OverlayStatus _overlay = new OverlayStatus();
    private void Awake()
    {
        _Initialize();
    }
    private void LateUpdate()
    {
        _overlay = OnOverlayStatusCalled?.Invoke();
        _scoreText.text = $"Score: {_overlay.score}.";
        _distanceText.text = $"{_overlay.distanceCount} M.";
        _picUpCountText.text = $"{_overlay.picUpsCount}";
    }
    
    public void BtnPause()
    {
        _mainHUD.BtnMainHudSound();
        _mainHUD.OpenMenu(Menu.PAUSE, gameObject);
    }
    private void _Initialize()
    {
        
        _pauseBtn.interactable = false;
        _overlay = OnOverlayStatusCalled?.Invoke();
        StartCoroutine(_StartGameCoroutine(Mathf.RoundToInt((float)_overlay.timerToStartRun)));
    }
    private IEnumerator _StartGameCoroutine(int countdownSeconds)
    {
        _countDownText.gameObject.SetActive(false);
       
        if (countdownSeconds <= 0)
        {
            yield break;
        }
        float _timeToStart = Time.time + countdownSeconds;
        yield return null;
        _countDownText.gameObject.SetActive(true);
        int _lastRemainingTime = countdownSeconds;
        
        while (Time.time <= _timeToStart)
        {
            float remainingTime = _timeToStart - Time.time;
            int _remainingTimeInt = Mathf.FloorToInt(remainingTime);
            _countDownText.text = $"{_remainingTimeInt + 1}";
            
            if (_lastRemainingTime != _remainingTimeInt)
            {
                _mainHudSound.PlayCountDownSound(_remainingTimeInt + 1);
            }

            _lastRemainingTime = _remainingTimeInt;
            float _percent = remainingTime - _remainingTimeInt;
            _countDownText.transform.localScale = Vector3.Lerp(Vector3.one, Vector3.zero, _percent);
            yield return null;
        }
        _countDownText.gameObject.SetActive(false);
        _mainHudSound.PlayCountDownEndSound();
        OnStartedRun?.Invoke();
        _pauseBtn.interactable = true;
    }
}
