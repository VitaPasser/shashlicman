using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject playerGameObject;
    [SerializeField] private Transform[] MoveSpots;
    private int randomSpot;
    private float waitTime;
    [SerializeField] public float startWaiteTime;
    [SerializeField] public float speed;


    

    void Start()
    {
        waitTime = startWaiteTime;
        
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, MoveSpots[randomSpot].position, speed*Time.deltaTime);

        if (Vector2.Distance(transform.position, MoveSpots[randomSpot].position) < 0.2f)
        {
            if (waitTime <= 0)
            {
                randomSpot = Random.Range(0, MoveSpots.Length);
                waitTime = startWaiteTime;
            } else
            {
                waitTime -= Time.deltaTime;
            }
        }
       
    }

    void MoveTowards(Vector2 targetPosition)
    {
        
    }

}
