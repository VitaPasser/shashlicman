using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Player");
        if (this.gameObject.GetComponent<Player>().is_dead)
        {
            DontDestroyOnLoad(this.gameObject);
            Destroy(objs[0]);

        } else
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }
}
