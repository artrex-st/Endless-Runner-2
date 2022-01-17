using UnityEngine;

public static class AudioUtils
{
    public static void PlayAudioCue(AudioSource _AudioSource, AudioClip _Clip)
    {
        if (_AudioSource.outputAudioMixerGroup == null)
        {
            Debug.LogWarning($"({_AudioSource.gameObject.name}) Every AudioSource must have a AudioSourceMixerGroup");
        }
        else
        {
            _AudioSource.clip = _Clip;
            _AudioSource.loop = false;
            _AudioSource.Play();
        }
    }
    public static void PlayMusic(AudioSource _AudioSource, AudioClip _Clip)
    {
        if (_AudioSource.outputAudioMixerGroup == null)
        {
            Debug.LogWarning($"Every AudioSource must have a AudioSourceMixerGroup ({_AudioSource.name} don't have)");
        }
        else
        {
            _AudioSource.clip = _Clip;
            _AudioSource.loop = true;
            _AudioSource.Play();
        }
    }
    public static void PlayMusic(AudioSource _AudioSource, AudioClip _Clip, float _Delay)
    {
        if (_AudioSource.outputAudioMixerGroup == null)
        {
            Debug.LogWarning($"Every AudioSource must have a AudioSourceMixerGroup ({_AudioSource.name} don't have)");
        }
        else
        {
            _AudioSource.clip = _Clip;
            _AudioSource.loop = true;
            _AudioSource.PlayDelayed(_Delay);
        }
    }
    public static void PlayMusicOneTime(AudioSource _AudioSource, AudioClip _Clip)
    {
        if (_AudioSource.outputAudioMixerGroup == null)
        {
            Debug.LogWarning($"Every AudioSource must have a AudioSourceMixerGroup ({_AudioSource.name} don't have)");
        }
        else
        {
            _AudioSource.clip = _Clip;
            _AudioSource.loop = false;
            _AudioSource.Play();
        }
    }
}
