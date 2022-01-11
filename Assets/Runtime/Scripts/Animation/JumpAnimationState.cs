using UnityEngine;
public class JumpAnimationState : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //olhar a duracao da animacao de pulo 
        AnimatorClipInfo[] clips = animator.GetNextAnimatorClipInfo(layerIndex);
        if (clips.Length > 0)
        {
            AnimatorClipInfo jumpClipInfo = clips[0];
            //TODO: Assumindo que o PlayerController esta no objeto pai.
            PlayerControl player = animator.transform.parent.GetComponent<PlayerControl>();

            //seta o JumpMultiplier para que a duracao final da animacao de pulo seja = a duracao do pulo no gameplay
            float multiplier = jumpClipInfo.clip.length / player.JumpDuration;
            animator.SetFloat(PlayerAnimationConstants.JumpMultiplier, multiplier);
        }
    }
}
