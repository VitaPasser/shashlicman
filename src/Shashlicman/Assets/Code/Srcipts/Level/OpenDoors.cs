using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;


public class DoorsController : MonoBehaviour
{

    private float openAngle = -90f;

    private float closeAngle = 0f;

    public void OpenDoors()
    {
        GameObject[] doors = GameObject.FindGameObjectsWithTag("Door");

        if (doors != null && doors.Length > 0)
        {
            foreach (GameObject door in doors)
            {
                if (CanOpenDoor(door))
                {
                    door.transform.rotation = Quaternion.Euler(0f, 0f, openAngle);
                }
            }
        }
        else
        {
            Debug.LogError("Не найдено объектов с тегом 'Door'.");
        }
    }

    public void CloseDoors()
    {
        GameObject[] doors = GameObject.FindGameObjectsWithTag("Door");

        if (doors != null && doors.Length > 0)
        {
            foreach (GameObject door in doors)
            {
                door.transform.rotation = Quaternion.Euler(0f, closeAngle, 0f);
            }
        }
        else
        {
            Debug.LogError("Не найдено объектов с тегом 'Door'");
        }
    }

    private bool CanOpenDoor(GameObject door)
    {
        RaycastHit hit;
        if (Physics.Raycast(door.transform.position, door.transform.forward, out hit, 1f))
        {
            if (hit.collider.CompareTag("Covers"))
            {
                return false;
            }
        }
        return true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("enemy");
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            if (enemies.Length == 0)
            {
                OpenDoors();
            }
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
}


