using UnityEngine;
public class JumpAnimationState : StateMachineBehaviour
{
    public delegate float AnimationJumpModifyHandler();
    public static event AnimationJumpModifyHandler OnAnimationJumpModifier;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        AnimatorClipInfo[] clips = animator.GetNextAnimatorClipInfo(layerIndex);
        if (clips.Length > 0)
        {
            AnimatorClipInfo jumpClipInfo = clips[0];
            //PlayerControl player = animator.transform.parent.GetComponent<PlayerControl>();
            float playerModifier = (float)OnAnimationJumpModifier?.Invoke();
            float multiplier = jumpClipInfo.clip.length / playerModifier;
            animator.SetFloat(PlayerAnimationConstants.JumpMultiplier, multiplier);
        }
    }
}
