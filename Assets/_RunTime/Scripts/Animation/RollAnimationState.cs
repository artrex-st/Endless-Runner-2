using UnityEngine;
public class RollAnimationState : StateMachineBehaviour
{
    public delegate float AnimationRollModifyHandler();
    public static event AnimationRollModifyHandler OnAnimationRollModifier;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        AnimatorClipInfo[] clips = animator.GetNextAnimatorClipInfo(layerIndex);
        //PlayerControl player = animator.transform.parent.GetComponent<PlayerControl>();
        float playerModifier = (float)OnAnimationRollModifier?.Invoke();
        if (playerModifier != 0 && clips.Length > 0)
        {
            AnimatorClipInfo clipInfo = clips[0];
            float multiplier = clipInfo.clip.length / playerModifier;
            animator.SetFloat(PlayerAnimationConstants.RollSpeedMultiplier, multiplier);
        }
    }
}
