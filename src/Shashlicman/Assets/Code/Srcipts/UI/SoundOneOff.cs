using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundOneOff : MonoBehaviour
{
    private AudioSource m_AudioSource;
    private bool isPlayed = false;
    // Start is called before the first frame update
    void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_AudioSource != null && isPlayed && !m_AudioSource.isPlaying) Destroy(m_AudioSource);
        if (!isPlayed)
        {
            m_AudioSource.Play();
            isPlayed = true;
        }
    }
}
