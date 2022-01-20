using UnityEngine;

sealed public class SwipeDetection : MonoBehaviour
{
    [SerializeField] private InputManager _inputManager;
    [SerializeField] private PlayerControl _playerControl;
    [SerializeField] private float _minimumDistance = 0.2f, _maximumTime = 1f;
    [SerializeField, Range(0,1)] private float _directionThreshold = 0.7f;
    private Vector2 _startPosition, _endPosition, _axisDirection;
    private float _startTime, _endTime;

    public SwipeDetection(InputManager inputManager, PlayerControl playerControl, float minimumDistance, float maximumTime, float directionThreshold, Vector2 startPosition, Vector2 endPosition, Vector2 axisDirection, float startTime, float endTime)
    {
        _inputManager = inputManager;
        _playerControl = playerControl;
        _minimumDistance = minimumDistance;
        _maximumTime = maximumTime;
        _directionThreshold = directionThreshold;
        _startPosition = startPosition;
        _endPosition = endPosition;
        _axisDirection = axisDirection;
        _startTime = startTime;
        _endTime = endTime;
    }

    public Vector2 AxisDirection => _axisDirection;

    private void OnEnable()
    {
        _InitializeEneble();
    }
    private void OnDisable()
    {
        _InitializeDisable();
    }
    private void _InitializeEneble()
    {
        _inputManager.OnStartTouch += _SwipeStart;
        _inputManager.OnEndTouch += _SwipeEnd;
        _inputManager.OnSwipeAxis += _SwipeAxis;
    }
    private void _InitializeDisable()
    {
        _inputManager.OnStartTouch -= _SwipeStart;
        _inputManager.OnEndTouch -= _SwipeEnd;
        _inputManager.OnSwipeAxis -= _SwipeAxis;
    }
    private void _SwipeStart(Vector2 position, float time)
    {
        _startPosition = position;
        _startTime = time;
    }
    private void _SwipeEnd(Vector2 position, float time)
    {
        _endPosition = position;
        _endTime = time;
        _DetectSwipe();
    }
    private void _DetectSwipe()
    {
        if (Vector3.Distance(_startPosition, _endPosition) >= _minimumDistance 
            && (_endTime - _startTime) <= _maximumTime
            && _playerControl.isActiveAndEnabled)
        {
            Vector3 direction = _endPosition - _startPosition;
            Vector2 direction2D = new Vector2(direction.x, direction.y).normalized;
            _playerControl.SwipeDirection(direction2D, _directionThreshold);
        }
    }
    private void _SwipeAxis(Vector2 axis)
    {
        _playerControl.PlayerInputsVector2(axis);
    }
}
