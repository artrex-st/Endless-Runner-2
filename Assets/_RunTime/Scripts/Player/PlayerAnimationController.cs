using System.Collections;
using UnityEngine;

[RequireComponent(typeof(PlayerControl))]
public sealed class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    public PlayerAnimationController(Animator animator)
    {
        _animator = animator;
    }

    public void OnStartedTriggerAnimation(string animationString)
    {
        _animator.SetTrigger(animationString);
    }
    public void OnStartedBoolAnimation(string animationString, bool isStarted)
    {
        _animator.SetBool(animationString, isStarted);
    }
    public IEnumerator OnStartedGameAnimation()
    {
        _animator.SetTrigger(PlayerAnimationConstants.StartGameTrigger);
       
        while (!_animator.GetCurrentAnimatorStateInfo(0).IsName(PlayerAnimationConstants.StartRun))
        {
            yield return null;
        }
        
        while (_animator.GetCurrentAnimatorStateInfo(0).IsName(PlayerAnimationConstants.StartRun)
            && _animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
        {
            yield return null;
        }
    }
    public void OnAnimationModifier()
    {
        AnimatorClipInfo[] clips = _animator.GetNextAnimatorClipInfo(0);
        PlayerControl player = _animator.transform.parent.GetComponent<PlayerControl>();
        if (player != null && clips.Length > 0)
        {
            AnimatorClipInfo clipInfo = clips[0];
            float multiplier = clipInfo.clip.length / player.RollDuration;
            _animator.SetFloat(PlayerAnimationConstants.RollSpeedMultiplier, multiplier);
        }
    }
}
