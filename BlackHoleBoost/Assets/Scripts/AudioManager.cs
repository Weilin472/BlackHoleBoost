
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : Singleton<AudioManager>
{
    [Header("Audio Source")]
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioSource _sfxSource;
    [SerializeField] AudioMixer _audioMixer;


    [Header("Clip")]
    public AudioClip BGMusic;

    // Start is called before the first frame update
    void Start()
    {
        _musicSource.clip = BGMusic;
        _musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        _sfxSource.pitch = Random.Range(.95f, 1.05f);
        _sfxSource.PlayOneShot(clip);
    }

    public void OnMasterMusicChange(float value)
    {
        _audioMixer.SetFloat("MasterVolume", value);
    }

    public void OnMusicChange(float value)
    {
        _audioMixer.SetFloat("MusicVolume", value);
    }

    public void OnSFXChange(float value)
    {
        _audioMixer.SetFloat("SFXVolume", value);
    }
}
