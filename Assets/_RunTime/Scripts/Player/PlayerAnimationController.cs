using System.Collections;
using UnityEngine;

[RequireComponent(typeof(PlayerControl))]
public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private PlayerControl playerControl;
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
    private void Update()
    {
        _SetBoolAnimations();
    }
    private void _SetBoolAnimations()
    {
        animator.SetBool(PlayerAnimationConstants.IsJumping, playerControl.IsJumping);
        animator.SetBool(PlayerAnimationConstants.IsRolling, playerControl.IsRolling);
    }
}
