using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] private AudioMixer mixer = null;
    [SerializeField] private AudioSource source = null;
    [Header("Tracks")]
    [SerializeField] private AudioClip introTrack = null;
    [SerializeField] private AudioClip mainTrack = null;

    private void Awake()
    {
        Instance = this;
        PlayMenuTrack();
    }

    public void Mute()
    {
        source.mute = true;
    }
    public void Unmute()
    {
        source.mute = false;
    }

    public void Distort()
    {
        mixer.SetFloat("distortionAmount", 435f);
    }

    public void Undistort()
    {
        mixer.SetFloat("distortionAmount", 5000f);
    }
    
    public void PlayMenuTrack()
    {
        source.clip = introTrack;
        source.loop = true;
        source.volume = 0.7f;
        source.Play();
    }

    public void PlayGameTrack()
    {
        float remainingTime = introTrack.length - source.time;
        Invoke(nameof(SwitchTrack), remainingTime + 0.33f);
    }

    private void SwitchTrack()
    {
        source.Stop();
        source.clip = mainTrack;
        source.volume = 0.5f;
        source.Play();
    }
}
