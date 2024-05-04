using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : SingleTon<AudioManager>
{
    [SerializeField] private AudioClip musicClip;
    [SerializeField] private AudioClip clickSound;

    private AudioSource audioSourceEffects;
    private AudioSource audioSourceMusic;


    private new void Awake()
    {
        audioSourceEffects = GetComponent<AudioSource>();
        if (audioSourceEffects == null)
        {
            audioSourceEffects = gameObject.AddComponent<AudioSource>();
        }
        audioSourceMusic = gameObject.AddComponent<AudioSource>();
        audioSourceMusic.loop = true;
        audioSourceMusic.clip = musicClip;
    }

    private void Start()
    {
        PlayMusic();
    }

    public void SetupMusicSlider(Slider musicSlider)
    {
        if (musicSlider != null)
        {
            musicSlider.value = audioSourceMusic.volume;
            musicSlider.onValueChanged.AddListener(SetMusicVolume);
        }
    }

    public void SetupSoundSlider(Slider soundSlider)
    {
        if (soundSlider != null)
        {
            soundSlider.value = GetSoundVolume();
            soundSlider.onValueChanged.AddListener(SetSoundVolume);
        }
    }

    public void PlayEffect(AudioClip clip)
    {
        audioSourceEffects.PlayOneShot(clip);
    }

    public void PlayMusic()
    {
        if (!audioSourceMusic.isPlaying)
        {
            audioSourceMusic.Play();
        }
    }

    public void PlayClickEffect()
    {
        PlayEffect(clickSound);
    }

    public float GetSoundVolume()
    {
        return audioSourceEffects.volume;
    }

    public void SetSoundVolume(float volume)
    {
        audioSourceEffects.volume = volume;
    }

    public float GetMusicVolume()
    {
        return audioSourceMusic.volume;
    }

    public void SetMusicVolume(float volume)
    {
        audioSourceMusic.volume = volume;
    }

    public void PauseEffects()
    {
        audioSourceEffects.Pause();
    }

    public void ResumeEffects()
    {
        audioSourceEffects.UnPause();
    }

    public void PauseMusic()
    {
        if (audioSourceMusic.isPlaying)
            audioSourceMusic.Pause();
    }

    public void ResumeMusic()
    {
        if (!audioSourceMusic.isPlaying)
            audioSourceMusic.Play();
    }
}
