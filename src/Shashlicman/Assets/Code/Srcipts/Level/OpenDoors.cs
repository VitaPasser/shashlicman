using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;


public class RotateObjects : MonoBehaviour
{

    /*private float openAngle = -90f;

    private float closeAngle = 0f;*/
    [SerializeField] private bool isOpen = false;
    [SerializeField] private DirectionDoor directionDoor;
    private enum DirectionDoor
    {
        up = 1,
        down = 2,
        left = 3,
        right = 4
    };

    public void OpenDoors()
    {
        Transform door = gameObject.GetComponentsInChildren<Transform>()[1];

        if (door != null)
        {
            /*door.rotation = Quaternion.Euler(0f, 0f, openAngle);
            door.localPosition = new Vector2(-1, 0);*/
            door.GetComponent<SpriteRenderer>().enabled = false;
            door.GetComponent<BoxCollider2D>().enabled = false;
            isOpen = true;   
        }
    }

    public void CloseDoors()
    {
        Transform door = gameObject.GetComponentsInChildren<Transform>()[1];

        if (door != null)
        {
            /*door.transform.rotation = Quaternion.Euler(0f, closeAngle, 0f);
            door.localPosition = new Vector2(0, -1);*/
            door.GetComponent<SpriteRenderer>().enabled = true;
            door.GetComponent<BoxCollider2D>().enabled = false;
            isOpen = false;
            
        }
    }

    private void OnEnterTrigger()
    {
        BoxCollider2D zoneToNextLevel = gameObject.GetComponentsInChildren<BoxCollider2D>()[1];
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        List<Collider2D> colliders = new List<Collider2D>();
        ContactFilter2D filter2D = new ContactFilter2D();
        filter2D.minDepth = 0;
        filter2D.maxDepth = 1;
        zoneToNextLevel.OverlapCollider(filter2D, colliders);
        colliders.RemoveAll(item => item.GetComponent<Player>() == null);

        if (colliders.Count > 0)
        {
            switch(directionDoor)
            {
                case DirectionDoor.up:
                case DirectionDoor.down:
                    player.transform.position = new Vector2(player.transform.position.x, -player.transform.position.y);
                    break;
                case DirectionDoor.left:
                case DirectionDoor.right:
                    player.transform.position = new Vector2(-player.transform.position.x, player.transform.position.y);
                    break;
            }
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            CloseDoors();

        }

    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.End))
        {
            OpenDoors();
        }
        else if (Input.GetKeyDown(KeyCode.Home))
        {
            CloseDoors();
        }
    }

    private void FixedUpdate()
    {
        GameObject[] enemys = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemys.Length < 1 || isOpen)
        {
            OnEnterTrigger();
            OpenDoors();
        }
    }
}


