using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    public GameObject prefab;
    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Player");
        DontDestroyOnLoad(this.gameObject);
    }
}
