using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ObstacleDecoration : MonoBehaviour
{
    [SerializeField] private AudioClip collisionSound;
    [SerializeField] private Animation collisionAnimation;
    [Header("Components")]
    private AudioSource audioSource;
    private AudioSource AudioSource => audioSource == null ? audioSource = GetComponent<AudioSource>() : audioSource;
    public void PlayCollisionFeedBack()
    {
        AudioUtils.PlayAudioCue(AudioSource, collisionSound);
        
        if (collisionAnimation != null)
        {
            collisionAnimation.Play();
        }
        else
        {
            Debug.LogWarning($"{transform.name} there is no animation!");
        }
    }
}
