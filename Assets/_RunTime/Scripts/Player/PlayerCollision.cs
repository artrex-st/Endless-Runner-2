using UnityEngine;

[RequireComponent(typeof(PlayerControl))]
[RequireComponent(typeof(PlayerAnimationController))]
public class PlayerCollision : MonoBehaviour
{
    [SerializeField] private GameMode gameMode;
    private PlayerControl playerController;
    private PlayerAnimationController animationController;

    private void Awake()
    {
        Initialize();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Obstacle obstacle))
        {
            playerController.Die();
            animationController.Die();
            gameMode.OnGameOver();
            obstacle.PlayCollisionFeedBack(other);
        }
        if (other.TryGetComponent(out PicUp picUp))
        {
            gameMode.AddPickUp();
            picUp.OnPic();
        }
    }
    private void Initialize()
    {
        playerController = GetComponent<PlayerControl>();
        animationController = GetComponent<PlayerAnimationController>();
    }
}
