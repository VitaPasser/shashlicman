using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CkeckEnemies : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.position = new Vector3(50, 0, 0);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            other.transform.position = new Vector3(0, 0, 0);
        }
    }
}
