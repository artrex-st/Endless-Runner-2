using UnityEngine;

sealed public class SwipeDetection : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;
    [SerializeField] private PlayerControl playerControl;
    [SerializeField] private float minimumDistance = 0.2f;
    [SerializeField] private float maximumTime = 1f;
    [SerializeField, Range(0,1)] private float directionThreshold = 0.9f;
    private Vector2 startPosition;
    private Vector2 endPosition;
    private float startTime;
    private float endTime;
    private Vector2 axisDirection;
    public Vector2 AxisDirection => axisDirection;

    private void Start()
    {
        inputManager = inputManager != null ? inputManager : GetComponentInParent<InputManager>();
        playerControl = playerControl != null ? playerControl : GetComponentInParent<PlayerControl>();
    }
    private void OnEnable()
    {
        inputManager.OnStartTouch += SwipeStart;
        inputManager.OnEndTouch += SwipeEnd;
        inputManager.OnSwipeAxis += SwipeAxis;
    }
    private void OnDisable()
    {
        inputManager.OnStartTouch -= SwipeStart;
        inputManager.OnEndTouch -= SwipeEnd;
        inputManager.OnSwipeAxis -= SwipeAxis;
    }
    private void SwipeStart(Vector2 position, float time)
    {
        startPosition = position;
        startTime = time;
    }
    private void SwipeEnd(Vector2 position, float time)
    {
        endPosition = position;
        endTime = time;
        DetectSwipe();
    }
    private void DetectSwipe()
    {
        if (Vector3.Distance(startPosition, endPosition) >= minimumDistance && (endTime - startTime) <= maximumTime)
        {
            Vector3 direction = endPosition - startPosition;
            Vector2 direction2D = new Vector2(direction.x, direction.y).normalized;
            playerControl.SwipeDirection(direction2D, directionThreshold);
        }
    }
    private void SwipeAxis(Vector2 axis)
    {
        playerControl.PlayerInputsVector2(axis);
    }
}
