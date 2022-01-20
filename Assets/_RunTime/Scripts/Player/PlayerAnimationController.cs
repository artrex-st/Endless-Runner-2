using System.Collections;
using UnityEngine;

[RequireComponent(typeof(PlayerControl))]
public class PlayerAnimationController : MonoBehaviour
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
}
