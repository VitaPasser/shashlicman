using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene : MonoBehaviour
{
    public GameObject Restart;
    public GameObject playerPrefab;
    public GameObject enemyPrefab;
    public int minCountStartCreateEnemy = 2;
    public int maxCountStartCreateEnemy = 6;
    private Covers covers;
    public AudioSource audioSource;

    // Start is called before the first frame update
    [System.Obsolete]
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        if (players.Length != 0 && players[0].GetComponent<Player>().is_dead)
        {
            Restart.SetActiveRecursively(false);
            Destroy(players[0]);
            Instantiate(playerPrefab, new Vector3(Random.Range(-7, 7), Random.Range(-4, 4), 0.0167967f), Quaternion.identity);
            GameObject[] playerss = GameObject.FindGameObjectsWithTag("Player");
        }
        else if (players.Length == 0)
        {
            Instantiate(playerPrefab, new Vector3(Random.Range(-7, 7), Random.Range(-4, 4), 0.0167967f), Quaternion.identity);
            GameObject[] playerss = GameObject.FindGameObjectsWithTag("Player");
        }
        else
        {
            Debug.Log("goal");
            audioSource.Play();
            GameObject[] playerss = GameObject.FindGameObjectsWithTag("Player");
        }

        GameObject[] enemys = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemys.Length != 0)
        {
            Restart.SetActiveRecursively(false);
            foreach (GameObject enemy in enemys)
            {
                Destroy(enemy);
            }

            for (int i = 0; i < Random.Range(minCountStartCreateEnemy, maxCountStartCreateEnemy); i++)
            {
                Instantiate(enemyPrefab, new Vector3(Random.Range(-7, 7), Random.Range(-4, 4), 0.0167967f), Quaternion.identity);
            }

        }
        if (enemys.Length == 0)
        {
            for (int i = 0; i < Random.Range(minCountStartCreateEnemy, maxCountStartCreateEnemy); i++)
            {
                Instantiate(enemyPrefab, new Vector3(Random.Range(-7, 7), Random.Range(-4, 4), 0.0167967f), Quaternion.identity);
            }
        }

        covers = GameObject.FindWithTag("Covers").GetComponent<Covers>();
        covers.SpawnCovers();


    }

    // Update is called once per frame
    void Update()
    {

    }
}
