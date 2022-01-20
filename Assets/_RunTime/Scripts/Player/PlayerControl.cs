using UnityEngine;

public sealed class PlayerControl : MonoBehaviour
{
    public delegate void AnimationSetingBool(string animation, bool isEnabled);
    public event AnimationSetingBool OnStartedBoolAnimation;
    public delegate void AnimationSetingTrigger(string animation);
    public event AnimationSetingTrigger OnStartedTriggerAnimation;
    [SerializeField] private PlayerAudioController _playerAudioController;
    [SerializeField] private Collider _regularCollider, _rollCollider;
    [Header("Player Config")]
    [SerializeField] private PlayerControlConfig _playerConfig;

    public float ForwardSpeed {get; set;}
    public bool IsJumping { get; private set; }
    public bool IsRolling { get; private set; }
    public float JumpDuration => _playerConfig.jumpDistanceZ / ForwardSpeed;
    public float RollDuration => _playerConfig.rollDistanceZ / ForwardSpeed;
    private Vector3 _initialPosition;
    private float _targetPositionX, _rollStartZ, _jumpStartZ;

    public PlayerControl(PlayerAudioController playerAudioController, Collider regularCollider, Collider rollCollider, PlayerControlConfig playerConfig)
    {
        _playerAudioController = playerAudioController;
        _regularCollider = regularCollider;
        _rollCollider = rollCollider;
        _playerConfig = playerConfig;
        _Initialize();
    }

    private float _LeftLaneX => _initialPosition.x - _playerConfig.laneDistanceX;
    private float _RightLaneX => _initialPosition.x + _playerConfig.laneDistanceX;
    private bool _CanJump => !IsJumping && ForwardSpeed > 0;
    private bool _CanRoll => !IsRolling && ForwardSpeed > 0;

    public void PlayerInputsVector2(Vector2 axis)
    {
        _targetPositionX = Mathf.Clamp(_targetPositionX + (axis.x * _playerConfig.laneDistanceX), _LeftLaneX, _RightLaneX);
        
        if (axis.y > 0 && _CanJump)
        {
            _StartJump();
        }

        if (axis.y < 0 && _CanRoll)
        {
            _StartRoll();
        }
    }
    public void SwipeDirection(Vector2 direction, float directionThreshold)
    {
        // X
        if (Vector2.Dot(Vector2.left, direction) > directionThreshold)
        {
            _targetPositionX -= _playerConfig.laneDistanceX;
        }
        
        if (Vector2.Dot(Vector2.right, direction) > directionThreshold)
        {
            _targetPositionX += _playerConfig.laneDistanceX;
        }
        
        // Y
        if (Vector2.Dot(Vector2.up, direction) > directionThreshold && _CanJump)
        {
            _StartJump();
        }
        
        if (Vector2.Dot(Vector2.down, direction) > directionThreshold && _CanRoll)
        {
            _StartRoll();
        }
        _targetPositionX = Mathf.Clamp(_targetPositionX, _LeftLaneX, _RightLaneX);
    }
    public void Die()
    {
        OnStartedTriggerAnimation?.Invoke(PlayerAnimationConstants.DieTrigger);
        _playerAudioController.PlayDieSound();

        ForwardSpeed = 0;
        _playerConfig.horizontalSpeed = 0;
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
        _initialPosition = transform.position;
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
        return Mathf.Lerp(transform.position.x, _targetPositionX, Time.deltaTime * _playerConfig.horizontalSpeed);
    }
    private float _ProcessForwardMovement()
    {
        return transform.position.z + ForwardSpeed * Time.deltaTime;
    }
    private void _StartJump()
    {
        IsJumping = true;
        OnStartedBoolAnimation?.Invoke(PlayerAnimationConstants.IsJumping,IsJumping);
        
        _playerAudioController.PlayJumpSound();
        _jumpStartZ = transform.position.z;
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
            float jumpCurrentProgress = transform.position.z - _jumpStartZ;
            float jumpPercent = jumpCurrentProgress / _playerConfig.jumpDistanceZ;
            
            if (jumpPercent >= 1)
            {
                _StopJump();
            }
            else
            {
                deltaY = Mathf.Sin(Mathf.PI * jumpPercent) * _playerConfig.jumpHeightY;
            }
        }
        float targetPositionY = _initialPosition.y + deltaY;
        return Mathf.Lerp(transform.position.y, targetPositionY, Time.deltaTime * _playerConfig.jumpLerpSpeed);
    }
    private void _ProcessRoll()
    {
        if (IsRolling)
        {
            float percent = (transform.position.z - _rollStartZ) / _playerConfig.rollDistanceZ;
            
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

        _playerAudioController.PlayRollSound();
        _rollStartZ = transform.position.z;
        _regularCollider.enabled = false;
        _rollCollider.enabled = true;
        _StopJump();
    }
    private void _StopRoll()
    {
        IsRolling = false;
        OnStartedBoolAnimation?.Invoke(PlayerAnimationConstants.IsRolling,IsRolling);

        _regularCollider.enabled = true;
        _rollCollider.enabled = false;
    }
}
