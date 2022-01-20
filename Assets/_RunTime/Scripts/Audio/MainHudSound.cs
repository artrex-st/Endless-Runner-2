using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class MainHudSound : MonoBehaviour
{
    [SerializeField] private AudioClip _btnPressSound, _countDownEndSound;
    [SerializeField] private AudioClip[] _countDownSound;
    [SerializeField] private AudioSource _audioSource;

    public MainHudSound(AudioClip btnPressSound, AudioClip countDownEndSound, AudioClip[] countDownSound, AudioSource audioSource)
    {
        _btnPressSound = btnPressSound;
        _countDownEndSound = countDownEndSound;
        _countDownSound = countDownSound;
        _audioSource = audioSource;
    }

    private AudioSource _AudioSource => _audioSource;

    public void PlayButtonPressSound()
    {
        Play(_btnPressSound);
    }
    public void PlayCountDownSound(int index)
    {
        Play(_countDownSound[index]);
    }
    public void PlayCountDownEndSound()
    {
        Play(_countDownEndSound);
    }
    private void Play(AudioClip _Clip)
    {
        AudioUtils.PlayAudioCue(_AudioSource,_Clip);
    }
}
