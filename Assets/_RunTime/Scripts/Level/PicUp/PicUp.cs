using UnityEngine;

public class PicUp : MonoBehaviour
{
    [SerializeField] private AudioClip picUpSound;
    [Header("Components")]
    private AudioSource audioSource;
    private AudioSource AudioSource => audioSource == null ? audioSource = GetComponent<AudioSource>() : audioSource;
    public void OnPic()
    {
        AudioUtils.PlayAudioCue(AudioSource,picUpSound);
        PoolingSystem.Instance.ReturnGameObject(gameObject);
    } 
}
