using UnityEngine;

sealed public class PlayerControl : MonoBehaviour
{
    public delegate void AnimationSetingBool(string animation, bool isEnabled);
    public event AnimationSetingBool OnStartedBoolAnimation;
    public delegate void AnimationSetingTrigger(string animation);
    public event AnimationSetingTrigger OnStartedTriggerAnimation;

    [SerializeField] private PlayerAudioController playerAudioController;
    [SerializeField] private PlayerAnimationController playerAnimationController;
    [SerializeField] private float horizontalSpeed = 15;
    [SerializeField] private float laneDistanceX = 4;

    [Header("Jump")]
    [SerializeField] private float jumpDistanceZ = 5;
    [SerializeField] private float jumpHeightY = 2;
    [SerializeField] private float jumpLerpSpeed = 10;

    [Header("Roll")]
    [SerializeField] private float rollDistanceZ = 5;
    [SerializeField] private Collider regularCollider;
    [SerializeField] private Collider rollCollider;
    //others
    Vector3 initialPosition;
    private float targetPositionX;
    public float ForwardSpeed {get; set;} // GameMode edit this
    public bool IsJumping { get; private set; }
    private float rollStartZ;
    public bool IsRolling { get; private set; }
    public float JumpDuration => jumpDistanceZ / ForwardSpeed;
    public float RollDuration => rollDistanceZ / ForwardSpeed;
    private float jumpStartZ;
    private float LeftLaneX => initialPosition.x - laneDistanceX;
    private float RightLaneX => initialPosition.x + laneDistanceX;
    private bool CanJump => !IsJumping && ForwardSpeed > 0;
    private bool CanRoll => !IsRolling && ForwardSpeed > 0;

    public void PlayerInputsVector2(Vector2 axis)
    {
        targetPositionX = Mathf.Clamp(targetPositionX + (axis.x * laneDistanceX), LeftLaneX, RightLaneX);
        if (axis.y > 0 && CanJump)
        {
            _StartJump();
        }
        if (axis.y < 0 && CanRoll)
        {
            _StartRoll();
        }
    }
    public void SwipeDirection(Vector2 direction, float directionThreshold)
    {
        // X
        if (Vector2.Dot(Vector2.left, direction) > directionThreshold)
        {
            targetPositionX -= laneDistanceX;
        }
        if (Vector2.Dot(Vector2.right, direction) > directionThreshold)
        {
            targetPositionX += laneDistanceX;
        }
        // Y
        if (Vector2.Dot(Vector2.up, direction) > directionThreshold && CanJump)
        {
            _StartJump();
        }
        if (Vector2.Dot(Vector2.down, direction) > directionThreshold && CanRoll)
        {
            _StartRoll();
        }
        //Debug.Log($"Swipe Axis Touch:{direction}");
        targetPositionX = Mathf.Clamp(targetPositionX, LeftLaneX, RightLaneX);
    }
    public void Die()
    {
        OnStartedTriggerAnimation?.Invoke(PlayerAnimationConstants.DieTrigger);
        playerAudioController.PlayDieSound();

        ForwardSpeed = 0;
        horizontalSpeed = 0;
        _StopRoll();
        _StopJump();
    }

    private void Awake()
    {
        _Initialize();
    }
    private void Update()
    {
        _ProcessMovements();
    }

    private void _Initialize()
    {
        initialPosition = transform.position;
        _StopRoll();
        _StopJump();
    }
    private void _ProcessMovements()
    {
        Vector3 position = transform.position;
        position.x = _ProcessLaneMovement();
        _ProcessRoll();
        position.y = _ProcessJump();
        position.z = _ProcessForwardMovement();
        transform.position = position;
    }
    private float _ProcessLaneMovement()
    {
        return Mathf.Lerp(transform.position.x, targetPositionX, Time.deltaTime * horizontalSpeed);
    }
    private float _ProcessForwardMovement()
    {
        return transform.position.z + ForwardSpeed * Time.deltaTime;
    }
    private void _StartJump()
    {
        IsJumping = true;
        OnStartedBoolAnimation?.Invoke(PlayerAnimationConstants.IsJumping,IsJumping);
        
        playerAudioController.PlayJumpSound();
        jumpStartZ = transform.position.z;
        _StopRoll();
    }
    private void _StopJump()
    {
        IsJumping = false;
        OnStartedBoolAnimation?.Invoke(PlayerAnimationConstants.IsJumping,IsJumping);
    }
    private float _ProcessJump()
    {
        float deltaY = 0;
        if (IsJumping)
        {
            float jumpCurrentProgress = transform.position.z - jumpStartZ;
            float jumpPercent = jumpCurrentProgress / jumpDistanceZ;
            if (jumpPercent >= 1)
            {
                _StopJump();
            }
            else
            {
                deltaY = Mathf.Sin(Mathf.PI * jumpPercent) * jumpHeightY;
            }
        }
        float targetPositionY = initialPosition.y + deltaY;
        return Mathf.Lerp(transform.position.y, targetPositionY, Time.deltaTime * jumpLerpSpeed);
    }
    private void _ProcessRoll()
    {
        if (IsRolling)
        {
            float percent = (transform.position.z - rollStartZ) / rollDistanceZ;
            if (percent >= 1)
            {
                _StopRoll();
            }
        }
    }
    private void _StartRoll()
    {
        IsRolling = true;
        OnStartedBoolAnimation?.Invoke(PlayerAnimationConstants.IsRolling,IsRolling);

        playerAudioController.PlayRollSound();
        rollStartZ = transform.position.z;
        regularCollider.enabled = false;
        rollCollider.enabled = true;
        _StopJump();
    }
    private void _StopRoll()
    {
        IsRolling = false;
        OnStartedBoolAnimation?.Invoke(PlayerAnimationConstants.IsRolling,IsRolling);

        regularCollider.enabled = true;
        rollCollider.enabled = false;
    }
}
