using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : Singleton<AudioManager>
{
    [Header("Base Setting(s)")]
    [SerializeField]
    private SFX_AudioSetup m_SFX;
    [SerializeField]
    private AudioSource m_MusicPlayer;

    [Header("Audio Mixer")]
    [SerializeField]
    private AudioMixerGroup m_MusicGroup;
    [SerializeField]
    private AudioMixerGroup m_SFXGroup;

    #region  SFX Functions

    public void Play2DSFX(AudioClip aClip, Vector3 aPosition)
    {
        SFX_AudioSetup audio = Instantiate(m_SFX, aPosition, Quaternion.identity);
        audio.SetupAudio(aClip, 1, 1, false, 0);
        audio.PlayAudio(0);
    }

    public void Play2DSFX(AudioClip aClip, Vector3 aPosition, ulong aDelay)
    {
        SFX_AudioSetup audio = Instantiate(m_SFX, aPosition, Quaternion.identity);
        audio.SetupAudio(aClip, 1, 1, false, 0);
        audio.PlayAudio(aDelay);
    }

    public void Play2DSFX(AudioClip aClip, Vector3 aPosition, float aVolume, float aPitch, bool aFadeIn)
    {
        SFX_AudioSetup audio = Instantiate(m_SFX, aPosition, Quaternion.identity);
        audio.SetupAudio(aClip, aVolume, aPitch, aFadeIn, 0);
        audio.PlayAudio(0);
    }

    public void Play2DSFX(AudioClip aClip, Vector3 aPosition, float aVolume, float aPitch, bool aFadeIn, ulong aDelay)
    {
        SFX_AudioSetup audio = Instantiate(m_SFX, aPosition, Quaternion.identity);
        audio.SetupAudio(aClip, aVolume, aPitch, aFadeIn, 0);
        audio.PlayAudio(aDelay);
    }

    #endregion

    #region Music Functions

    public void PlayMusicOnce(AudioClip aClip)
    {
        m_MusicPlayer.PlayOneShot(aClip);
    }

    public void PlayMusic()
    {
        m_MusicPlayer.Play();
    }

    public void StopMusic()
    {
        m_MusicPlayer.Stop();
    }

    public void PauseMusic()
    {
        m_MusicPlayer.Pause();
    }

    public void SetMusic(AudioClip aClip, bool aAutoPlay)
    {
        m_MusicPlayer.clip = aClip;
        if(aAutoPlay)
        {
            PlayMusic();
        }
    }

    #endregion

    #region Option Functions

    public void OptionMusicGroupVolume(float aVolume)
    {
        m_MusicGroup.audioMixer.SetFloat("musicVolume", aVolume);
    }

    public void OptionSFXGroupVolume(float aVolume)
    {
        m_SFXGroup.audioMixer.SetFloat("sfxVolume", aVolume);
    }

    #endregion
}
