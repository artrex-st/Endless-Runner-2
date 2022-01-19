using UnityEngine;

public class PicUp : MonoBehaviour
{
    [SerializeField] private AudioClip _picUpSound;
    [Header("Components")]
    [SerializeField] private AudioSource _audioSource;
    private AudioSource _AudioSource => _audioSource == null ? _audioSource = GetComponent<AudioSource>() : _audioSource;
    public void OnPic()
    {
        AudioUtils.PlayAudioCue(_AudioSource,_picUpSound);
        PoolingSystem.Instance.ReturnGameObject(gameObject);
    } 
}
