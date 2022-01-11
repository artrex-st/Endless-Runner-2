using System.Collections;
using System.Collections.Generic;
using UnityEngine;

sealed public class PlayerControl : MonoBehaviour
{
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
    private bool CanJump => !IsJumping;
    private bool CanRoll => !IsRolling;

    void Awake()
    {
        initialPosition = transform.position;
        StopRoll();
    }
    void Update()
    {
        ProcessMovements();
    }
    private void ProcessMovements()
    {
        Vector3 position = transform.position;
        position.x = ProcessLaneMovement();
        ProcessRoll();
        position.y = ProcessJump();
        position.z = ProcessForwardMovement();
        transform.position = position;
    }
    private float ProcessLaneMovement()
    {
        return Mathf.Lerp(transform.position.x, targetPositionX, Time.deltaTime * horizontalSpeed);
    }

    private float ProcessForwardMovement()
    {
        return transform.position.z + ForwardSpeed * Time.deltaTime;
    }

    public void PlayerInputsVector2(Vector2 axis)
    {
        targetPositionX = Mathf.Clamp(targetPositionX + (axis.x * laneDistanceX), LeftLaneX, RightLaneX);
        if (axis.y > 0 && CanJump)
        {
            StartJump();
        }
        if (axis.y < 0 && CanRoll)
        {
            StartRoll();
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
            StartJump();
        }
        if (Vector2.Dot(Vector2.down, direction) > directionThreshold && CanRoll)
        {
            StartRoll();
        }
        Debug.Log($"Swipe Axis Touch:{direction}");
        targetPositionX = Mathf.Clamp(targetPositionX, LeftLaneX, RightLaneX);
    }
     private void StartJump()
    {
        //playerAudioController.PlayJumpSound();
        IsJumping = true;
        jumpStartZ = transform.position.z;
        StopRoll();
    }
    private void StopJump()
    {
        IsJumping = false;
    }

    private float ProcessJump()
    {
        float deltaY = 0;
        if (IsJumping)
        {
            float jumpCurrentProgress = transform.position.z - jumpStartZ;
            float jumpPercent = jumpCurrentProgress / jumpDistanceZ;
            if (jumpPercent >= 1)
            {
                StopJump();
            }
            else
            {
                deltaY = Mathf.Sin(Mathf.PI * jumpPercent) * jumpHeightY;
            }
        }
        float targetPositionY = initialPosition.y + deltaY;
        return Mathf.Lerp(transform.position.y, targetPositionY, Time.deltaTime * jumpLerpSpeed);
    }
    private void ProcessRoll()
    {
        if (IsRolling)
        {
            float percent = (transform.position.z - rollStartZ) / rollDistanceZ;
            if (percent >= 1)
            {
                StopRoll();
            }
        }
    }

    private void StartRoll()
    {
        //playerAudioController.PlayRollSound();
        rollStartZ = transform.position.z;
        IsRolling = true;
        regularCollider.enabled = false;
        rollCollider.enabled = true;
        StopJump();
    }

    private void StopRoll()
    {
        IsRolling = false;
        regularCollider.enabled = true;
        rollCollider.enabled = false;
    }

    public void Die()
    {
        //playerAudioController.PlayDieSound();
        ForwardSpeed = 0;
        horizontalSpeed = 0;
        StopRoll();
        StopJump();
    }
}
