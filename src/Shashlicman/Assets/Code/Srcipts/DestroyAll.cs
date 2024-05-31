using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class DestroyAll : MonoBehaviour
{
    //[SerializeField]private GameObject GvideoPlayer;
    private VideoPlayer videoPlayer;
    [SerializeField] private VideoClip video;
    // Start is called before the first frame update
    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        if (players.Length > 0)
        {
            for (int i = 0; i < players.Length; i++)
            {
                //Destroy(soundtrack[i]);
                players[i].SetActive(false);
            }
        }

        GameObject[] soundtrack = GameObject.FindGameObjectsWithTag("SoundTrack");

        if (soundtrack.Length > 0)
        {
            for (int i = 0; i < soundtrack.Length; i++)
            {
                //Destroy(soundtrack[i]);
                soundtrack[i].SetActive(false);
            }
        }

        GameObject[] tutorials = GameObject.FindGameObjectsWithTag("Tutorial");

        if (tutorials.Length > 0)
        {
            for (int i = 0; i < tutorials.Length; i++)
            {
                tutorials[i].SetActive(false);
            }
        }

        GameObject[] TutorialCanvas = GameObject.FindGameObjectsWithTag("TutorialCanvas");

        if (TutorialCanvas.Length > 0)
        {
            for (int i = 0; i < TutorialCanvas.Length; i++)
            {
                TutorialCanvas[i].SetActive(false);
            }
        }

        GameObject[] DialogBar = GameObject.FindGameObjectsWithTag("DialogBar");

        if (DialogBar.Length > 0)
        {
            for (int i = 0; i < DialogBar.Length; i++)
            {
                DialogBar[i].SetActive(false);
            }
        }
        videoPlayer.clip = video;
        videoPlayer.Play();


    }

    // Update is called once per frame
    void Update()
    {
        if (!videoPlayer.isPlaying || Input.GetKeyDown(KeyCode.Space))
        {

            Debug.Log("end");
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

            if (players.Length > 0)
            {
                for (int i = 0; i < players.Length - 1; i++)
                {
                    //Destroy(soundtrack[i]);
                    players[i].SetActive(true);
                }
            }

            GameObject[] soundtrack = GameObject.FindGameObjectsWithTag("SoundTrack");

            if (soundtrack.Length > 0)
            {
                for (int i = 0; i < soundtrack.Length - 1; i++)
                {
                    //Destroy(soundtrack[i]);
                    soundtrack[i].SetActive(true);
                }
            }

            GameObject[] tutorials = GameObject.FindGameObjectsWithTag("Tutorial");

            if (tutorials.Length > 0)
            {
                for (int i = 0; i < tutorials.Length; i++)
                {
                    tutorials[i].SetActive(true);
                }
            }

            GameObject[] TutorialCanvas = GameObject.FindGameObjectsWithTag("TutorialCanvas");

            if (TutorialCanvas.Length > 0)
            {
                for (int i = 0; i < TutorialCanvas.Length; i++)
                {
                    TutorialCanvas[i].SetActive(true);
                }
            }

            GameObject[] DialogBar = GameObject.FindGameObjectsWithTag("DialogBar");

            if (DialogBar.Length > 0)
            {
                for (int i = 0; i < DialogBar.Length; i++)
                {
                    DialogBar[i].SetActive(true);
                }
            }
            SceneManager.LoadScene("SampleScene");
        }
    }
}
