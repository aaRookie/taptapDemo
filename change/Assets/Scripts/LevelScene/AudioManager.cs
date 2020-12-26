using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance=null;

    public AudioSource m_audiosource;

    public AudioClip m_clickButton;
    public AudioClip m_bgm;
    public AudioClip m_dead;
    public AudioClip m_switch;
    public AudioClip m_getfire;
    public AudioClip m_getthun;
    public AudioClip m_getwind;
    public AudioClip m_usefire;
    public AudioClip m_usethun;
    public AudioClip m_usewind;

    public void Awake()
    {
        if(instance=null)
        instance = this;
    }
        
    public void PlayClickButton()
    {
        m_audiosource.PlayOneShot(m_clickButton);
    }

    public void PlayBgm()
    {
        m_audiosource.PlayOneShot(m_bgm);
    }

    public void PlayDead()
    {
        m_audiosource.PlayOneShot(m_dead);
    }

    public void PlaySwitch()
    {
        m_audiosource.PlayOneShot(m_switch);
    }

    public void PlayGetThun()
    {
        m_audiosource.PlayOneShot(m_getthun);
    }

    public void PlayGetFire()
    {
        m_audiosource.PlayOneShot(m_getfire);
    }

    public void PayGetWind()
    {
        m_audiosource.PlayOneShot(m_getwind);
    }

    public void PlayUseFire()
    {
        m_audiosource.PlayOneShot(m_usefire);
    }

    public void PlayUseThun()
    {
        m_audiosource.PlayOneShot(m_usethun);
    }

    public void PlayUseWind()
    {
        m_audiosource.PlayOneShot(m_usewind);
    }

}
