using UnityEngine;
public class RollAnimationState : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        AnimatorClipInfo[] clips = animator.GetNextAnimatorClipInfo(layerIndex);
        //TODO: Assumindo que o PlayerController esta no objeto pai.
        PlayerControl player = animator.transform.parent.GetComponent<PlayerControl>();
        if (player != null && clips.Length > 0)
        {
            AnimatorClipInfo clipInfo = clips[0];
            float multiplier = clipInfo.clip.length / player.RollDuration;
            animator.SetFloat(PlayerAnimationConstants.RollSpeedMultiplier, multiplier);
        }
    }
}
