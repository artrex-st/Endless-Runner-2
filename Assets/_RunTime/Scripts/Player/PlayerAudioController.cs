using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayerAudioController : MonoBehaviour
{
    [Header("Sounds Effects")]
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip rollSound;
    [SerializeField] private AudioClip dieSound;
    [Header("Components")]
    private AudioSource audioSource;
    private AudioSource AudioSource => audioSource == null ? audioSource = GetComponent<AudioSource>() : audioSource;

    public void PlayJumpSound()
    {
        Play(jumpSound);
    }
    public void PlayRollSound()
    {
        Play(rollSound);
    }
    public void PlayDieSound()
    {
        Play(dieSound);
    }
    private void Play(AudioClip _Clip)
    {
        AudioUtils.PlayAudioCue(AudioSource,_Clip);
    }
    private void StopSond()
    {
        AudioSource.Stop();
    }
}
