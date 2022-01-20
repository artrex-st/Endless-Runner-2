using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public sealed class MusicController : MonoBehaviour
{
    [SerializeField] private AudioClip _startMenuMusic, _mainTrackMusic, _deathTrackMusic;
    private AudioSource _audioSource;
    private AudioSource _AudioSource => _audioSource == null ? _audioSource = GetComponent<AudioSource>() : _audioSource;
    private const float defaultDelay = 0.1f;

    public MusicController(AudioClip startMenuMusic, AudioClip mainTrackMusic, AudioClip deathTrackMusic, AudioSource audioSource)
    {
        _startMenuMusic = startMenuMusic;
        _mainTrackMusic = mainTrackMusic;
        _deathTrackMusic = deathTrackMusic;
        _audioSource = audioSource;
    }

    public void PlayStartMenuMusic()
    {
        PlayMusic(_startMenuMusic, defaultDelay);
    }
    public void PlayMainTrackMusic()
    {
        PlayMusic(_mainTrackMusic);
    }
    public void PlayDeathTrackMusic()
    {
        PlayMusicOneTime(_deathTrackMusic);
    }

    public void PlayMusic(AudioClip _Clip)
    {
        AudioUtils.PlayMusic(_AudioSource,_Clip);
    }
    public void PlayMusic(AudioClip _Clip, float delay)
    {
        AudioUtils.PlayMusic(_AudioSource,_Clip, delay);
    }

    public void PlayMusicOneTime(AudioClip _Clip)
    {
        AudioUtils.PlayMusicOneTime(_AudioSource,_Clip);
    }
    public void StopMusic()
    {
        _AudioSource.Stop();
    }
}
