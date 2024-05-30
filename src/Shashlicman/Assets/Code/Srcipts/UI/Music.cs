using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    [SerializeField] private AudioClip[] soundtracks;
    private AudioSource audioSource;
    private int numSoundtrackPlay = 0;
    private int numSoundtrackPlayWillBe = -1;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!audioSource.isPlaying) 
        {
            while (numSoundtrackPlayWillBe == numSoundtrackPlay)
            {
                numSoundtrackPlay = Random.Range(0, soundtracks.Length - 1);
            }

            audioSource.clip = soundtracks[numSoundtrackPlay];
            audioSource.Play();
            numSoundtrackPlayWillBe = numSoundtrackPlay;
        }

    }
}
