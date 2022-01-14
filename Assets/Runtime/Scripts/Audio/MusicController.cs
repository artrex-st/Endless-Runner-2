using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicController : MonoBehaviour
{
    [Header("Musics")]
    [SerializeField] private AudioClip startMenuMusic;
    [SerializeField] private AudioClip mainTrackMusic;
    [SerializeField] private AudioClip deathTrackMusic;
    
    [Header("Components")]
    private AudioSource audioSource;
    private AudioSource AudioSource => audioSource == null ? audioSource = GetComponent<AudioSource>() : audioSource;
    
    public void PlayStartMenuMusic()
    {
        PlayMusic(startMenuMusic);
    }
    public void PlayMainTrackMusic()
    {
        PlayMusic(mainTrackMusic);
    }
    public void PlayDeathTrackMusic()
    {
        PlayMusicOneTime(deathTrackMusic);
    }
    public void PlayMusic(AudioClip _Clip)
    {
        AudioUtils.PlayMusic(AudioSource,_Clip);
    }
    public void PlayMusicOneTime(AudioClip _Clip)
    {
        AudioUtils.PlayMusicOneTime(AudioSource,_Clip);
    }
    public void StopMusic()
    {
        AudioSource.Stop();
    }
}
