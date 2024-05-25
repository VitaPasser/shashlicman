using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject playerGameObject;
    [SerializeField] private Transform[] MoveSpots;
    private int randomSpot;
    [SerializeField]private float waitTime;
    [SerializeField] public float startWaiteTime;
    [SerializeField] public float speed;
    [SerializeField] public float curranTspeed;
    private bool enemyPlayerCollided;

    

    void Start()
    {
        waitTime = startWaiteTime;
        enemyPlayerCollided = true;


    }

    void Update()
    {
        
        if (playerGameObject != null)
        {
            if (Vector2.Distance(playerGameObject.transform.position, transform.position) < 5.0f && !enemyPlayerCollided)
            {
                transform.position = Vector2.MoveTowards(transform.position, playerGameObject.transform.position, speed*Time.deltaTime);
                
            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, MoveSpots[randomSpot].position, speed * Time.deltaTime);
                if (Vector2.Distance(transform.position, MoveSpots[randomSpot].position) < 0.2f)
                {
                    do
                    {
                        if (waitTime <= 0)
                        {
                            randomSpot = Random.Range(0, MoveSpots.Length);
                            waitTime = startWaiteTime;
                            enemyPlayerCollided = false;
                        }
                        else
                        {
                            waitTime -= Time.deltaTime;
                        }
                    } while (waitTime <= 0);
                    
                }
                    
            }
        }
        if(Vector2.Distance(transform.position, playerGameObject.transform.position) < 1.4f)
        {
            enemyPlayerCollided = true;
        }
          
    }

}
