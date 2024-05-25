using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lose : MonoBehaviour
{
    private Player player;
    // Start is called before the first frame update
    void Start()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Player");
        player = objs[0].GetComponent<Player>();
    }

    // Update is called once per frame
    [System.Obsolete]
    void Update()
    {
        if (player.is_dead)
        {
            Debug.Log("IS DEAD(((");
            gameObject.SetActiveRecursively(true);
            if (Input.GetKeyDown(KeyCode.F)) { gameObject.SetActiveRecursively(false); Restart(); }

        }
    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
