using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lose : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    [System.Obsolete]
    void FixedUpdate()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Player");
        if (objs.Length != 1) return;

        Player player = objs[0].GetComponent<Player>();

        if (player.is_dead)
        {
            Debug.Log("IS DEAD(((");
            gameObject.SetActiveRecursively(true);
            if (Input.GetKeyDown(KeyCode.F)) { 
                gameObject.SetActiveRecursively(false); 
                Restart(); 
            }

        }
    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
