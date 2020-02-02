using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicGlich : IGlich
{
    AudioSource m_audio;
    public AudioSource m_glichAudio;
    public AudioClip[] clips;
    [Range(1,10)]
    public float levelOfGlich;
    public bool m_enable;
    float originalPitch;
    public override void glich(float levelOfGlich)
    {
        levelOfGlich = levelOfGlich * 10;
        if (levelOfGlich < 1)
            m_enable = false;
        else
            m_enable = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        m_audio = GetComponent<AudioSource>();
        originalPitch = m_audio.pitch;
        StartCoroutine(glichAudio());
    }

    IEnumerator glichAudio()
    {
        while (true)
        {
            if (m_enable)
            {
                int times =(int) (levelOfGlich) * 3;
                for (int i = 0; i<times; ++i)
                {
                    m_audio.pitch = UnityEngine.Random.Range(1, 3.0f);
                    yield return null;
                }
                m_audio.pitch = originalPitch;
                int restart = UnityEngine.Random.Range(0, 100);
                if (restart < levelOfGlich * 10)
                {
                    int index = UnityEngine.Random.Range(0, clips.Length);
                    if(index >= clips.Length && m_glichAudio)
                    {
                        m_glichAudio.clip = clips[index];
                        m_glichAudio.Play();
                    }
                }
                restart = UnityEngine.Random.Range(0, 100);
                if (restart < levelOfGlich * 10)
                {
                    m_audio.Stop();
                    m_audio.Play();
                }
                yield return new WaitForSeconds((10 - levelOfGlich) + (10 - levelOfGlich) + 1);
            }
            yield return null;
        }
        
    }
}
