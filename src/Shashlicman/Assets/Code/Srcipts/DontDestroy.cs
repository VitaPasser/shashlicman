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
        

        DontDestroyOnLoad(this.gameObject);
        //DontDestroyOnLoad(this.gameObject);
    }
}
