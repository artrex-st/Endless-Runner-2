using System.Collections;
using UnityEngine;

public class PicUp : MonoBehaviour, IPicUps
{
    [SerializeField] private GameObject _visual;
    [SerializeField] private AudioClip _picUpSound;
    [Header("Components")]
    [SerializeField] private AudioSource _audioSource;

    public PicUp(GameObject visual, AudioClip picUpSound, AudioSource audioSource)
    {
        _visual = visual;
        _picUpSound = picUpSound;
        _audioSource = audioSource;
    }

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
        PoolingSystem.Instance.ReturnGameObject(gameObject);
    }
}
