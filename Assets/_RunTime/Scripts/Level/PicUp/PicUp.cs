using System.Collections;
using UnityEngine;

public class PicUp : MonoBehaviour
{
    [SerializeField] private GameObject _visual;
    [SerializeField] private AudioClip _picUpSound;
    [Header("Components")]
    [SerializeField] private AudioSource _audioSource;
    private AudioSource _AudioSource => _audioSource == null ? _audioSource = GetComponent<AudioSource>() : _audioSource;
    public void OnPic()
    {
        AudioUtils.PlayAudioCue(_AudioSource,_picUpSound);
        _visual.SetActive(false);
        StartCoroutine(DisablePicUp());
    } 
    private IEnumerator DisablePicUp()
    {
        while (_audioSource.isPlaying)
        {
            yield return null;
        }
        PoolingSystem.Instance.ReturnGameObject(gameObject);
    }
    private void OnDisable()
    {
        _visual.SetActive(true);
    }
}
