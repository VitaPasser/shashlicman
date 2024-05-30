using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    //public GameObject prefab;
    //public AudioSource music;
    void Awake()
    {
        //GameObject[] objs = GameObject.FindGameObjectsWithTag("Player");
        GameObject[] soundtrack = GameObject.FindGameObjectsWithTag("SoundTrack");

        if (soundtrack.Length > 1)
        {
            for (int i = 1; i < soundtrack.Length; i++)
            {
                Destroy(soundtrack[i]);
            }
        }

        GameObject[] tutorials = GameObject.FindGameObjectsWithTag("Tutorial");

        if (tutorials.Length > 1)
        {
            for (int i = 1; i < tutorials.Length; i++)
            {
                Destroy(tutorials[i]);
            }
        }

        GameObject[] TutorialCanvas = GameObject.FindGameObjectsWithTag("TutorialCanvas");

        if (TutorialCanvas.Length > 1)
        {
            for (int i = 1; i < TutorialCanvas.Length; i++)
            {
                Destroy(TutorialCanvas[i]);
            }
        }

        GameObject[] DialogBar = GameObject.FindGameObjectsWithTag("DialogBar");

        if (DialogBar.Length > 1)
        {
            for (int i = 1; i < DialogBar.Length; i++)
            {
                Destroy(DialogBar[i]);
            }
        }


        DontDestroyOnLoad(this.gameObject);
        //DontDestroyOnLoad(this.gameObject);
    }
}
