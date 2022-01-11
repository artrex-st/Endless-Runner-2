using UnityEngine;

public static class AudioUtils
{
    public static void PlayAudioCue(AudioSource _AudioSource, AudioClip _Clip)
    {
        if (_AudioSource.outputAudioMixerGroup == null)
        {
            Debug.LogWarning($"erro: Todo AudioSource deve ter um AudioSourceMixerGroup");
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
            Debug.LogWarning($"erro: Todo AudioSource deve ter um AudioSourceMixerGroup");
        }
        else
        {
            _AudioSource.clip = _Clip;
            _AudioSource.loop = true;
            //_AudioSource.Play();
            _AudioSource.PlayDelayed(0.5f);
        }
    }
}
