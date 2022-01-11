using UnityEngine;

[RequireComponent(typeof(PlayerControl))]
public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    public Animator PlayerAnimator => animator == null ? animator = GetComponent<Animator>() : animator;
    private PlayerControl player;

    private void Awake()
    {
        player = GetComponent<PlayerControl>();
        animator = animator != null ? animator : GetComponentInChildren<Animator>();
    }
    private void Update()
    {
        animator.SetBool(PlayerAnimationConstants.IsJumping, player.IsJumping);
        animator.SetBool(PlayerAnimationConstants.IsRolling, player.IsRolling);
    }
    public void Die()
    {
        animator.SetTrigger(PlayerAnimationConstants.DieTrigger);
    }
    public void SetStartTriggerAnimation()
    {
        animator.SetTrigger(PlayerAnimationConstants.StartGameTrigger);
    }
    public bool EndStartAnimation()
    {
        return animator.GetCurrentAnimatorStateInfo(0).IsName(PlayerAnimationConstants.StartRun) && animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1;
    }
}
