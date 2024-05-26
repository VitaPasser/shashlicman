using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene : MonoBehaviour
{
    public GameObject Restart;
    public GameObject player;
    private Covers covers;

    // Start is called before the first frame update
    [System.Obsolete]
    void Start()
    {
        covers = GameObject.FindWithTag("Covers").GetComponent<Covers>();
        covers.SpawnCovers();


        GameObject[] objs = GameObject.FindGameObjectsWithTag("Player");
        if (objs.Length != 0 && objs[0].GetComponent<Player>().is_dead)
        {
            Restart.SetActiveRecursively(false);
            Destroy(objs[0]);
            Instantiate(player, new Vector3(3.68f, 2.87f, 0.0167967f), Quaternion.identity);
        }
        if (objs.Length == 0 )
        {
            Instantiate(player, new Vector3(3.68f, 2.87f, 0.0167967f), Quaternion.identity);
        }
        if (objs.Length == 1)
        {
            Player playerComponent = objs[0].GetComponent<Player>();
            switch (playerComponent.doorSide)
            {
                case "left":
                    objs[0].transform.position = new Vector3(8f, -0.2f, 0);
                    break;
                case "right":
                    objs[0].transform.position = new Vector3(-8f, -0.2f, 0);
                    break;
                case "up":
                    objs[0].transform.position = new Vector3(0, -4.6f, 0);
                    break;
                case "down":
                    objs[0].transform.position = new Vector3(0, 4.15f, 0);
                    break;
                default:
                    Debug.LogError("Неверное значение doorSide: " + playerComponent.doorSide);
                    break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
