using System.Collections;
using UnityEngine;

[RequireComponent(typeof(PlayerControl))]
public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private PlayerControl player;
    public Animator PlayerAnimator => animator == null ? animator = GetComponent<Animator>() : animator;

    public void Die()
    {
        animator.SetTrigger(PlayerAnimationConstants.DieTrigger);
    }
    public IEnumerator PlayStartGameAnimation()
    {
        animator.SetTrigger(PlayerAnimationConstants.StartGameTrigger);
        while (!animator.GetCurrentAnimatorStateInfo(0).IsName(PlayerAnimationConstants.StartRun))
        {
            yield return null;
        }
        while (animator.GetCurrentAnimatorStateInfo(0).IsName(PlayerAnimationConstants.StartRun)
            && animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
        {
            yield return null;
        }
    }
    private void Awake()
    {
        _Initialize();
    }
    private void Update()
    {
        _SetBoolAnimations();
    }
    private void _Initialize()
    {
        player = GetComponent<PlayerControl>();
        animator = animator != null ? animator : GetComponentInChildren<Animator>();
    }
    private void _SetBoolAnimations()
    {
        animator.SetBool(PlayerAnimationConstants.IsJumping, player.IsJumping);
        animator.SetBool(PlayerAnimationConstants.IsRolling, player.IsRolling);
    }
}
