using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lose : MonoBehaviour
{
    private enum TypeFaile
    {
        none = 0,
        OnlyZeroHealth = 1,
        ZeroHealthAndMental = 2
    }

    private TypeFaile typeFaile = TypeFaile.none;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    [System.Obsolete]
    void Update()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Player");
        if (objs.Length != 1) return;

        Player player = objs[0].GetComponent<Player>();

        if (player.is_dead && (player.emotion_level <= 0) && (typeFaile == TypeFaile.none || (typeFaile == TypeFaile.ZeroHealthAndMental)))
        {
            Debug.Log("IS DEAD(((");
            transform.GetChild(1).gameObject.SetActive(true);
            typeFaile = TypeFaile.ZeroHealthAndMental;
            player.transform.GetChild(0).gameObject.SetActive(false);
            //gameObject.GetComponentsInChildren<Transform>()[1].gameObject.SetActiveRecursively(true);
            //gameObject.SetActiveRecursively(true);
            if (Input.GetKey(KeyCode.F))
            {
                transform.GetChild(1).gameObject.SetActive(false);
                //gameObject.GetComponentsInChildren<Transform>()[1].gameObject.SetActiveRecursively(false);
                Restart();
            }
        } else if (player.is_dead && (typeFaile == TypeFaile.none || (typeFaile == TypeFaile.OnlyZeroHealth)))
        {
            Debug.Log("IS DEAD(((");
            transform.GetChild(0).gameObject.SetActive(true);
            typeFaile = TypeFaile.OnlyZeroHealth;
            player.transform.GetChild(0).gameObject.SetActive(false);
            //gameObject.GetComponentsInChildren<Transform>()[0].gameObject.SetActiveRecursively(true);
            //gameObject.SetActiveRecursively(true);
            if (Input.GetKey(KeyCode.F))
            {
                transform.GetChild(0).gameObject.SetActive(false);
                //gameObject.GetComponentsInChildren<Transform>()[0].gameObject.SetActiveRecursively(false);
                Restart();
            }

        }
    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
