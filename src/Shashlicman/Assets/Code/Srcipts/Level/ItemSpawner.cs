using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public List<GameObject> itemPrefabs;
    public GameObject healPrefab;
    private int minItemCount = 1;
    private int maxItemCount = 3;
    private Vector2 spawnAreaMin = new Vector2(-7, -4);
    private Vector2 spawnAreaMax = new Vector2(7, 4);

    void Start()
    {
        SpawnItems();
    }

    void SpawnItems()
    {

        int itemCount = Random.Range(minItemCount, maxItemCount + 1);

        for (int i = 0; i < itemCount; i++)
        {

            GameObject itemPrefab = itemPrefabs[Random.Range(0, itemPrefabs.Count-1)];


            float spawnPosX = Random.Range(spawnAreaMin.x, spawnAreaMax.x);
            float spawnPosY = Random.Range(spawnAreaMin.y, spawnAreaMax.y);
            Vector3 spawnPosition = new Vector3(spawnPosX, spawnPosY, 0f);

            Instantiate(itemPrefab, spawnPosition, Quaternion.identity);
        }
        float PosX = Random.Range(spawnAreaMin.x, spawnAreaMax.x);
        float PosY = Random.Range(spawnAreaMin.y, spawnAreaMax.y);
        Vector3 Position = new Vector3(PosX, PosY, 0f);
        Instantiate(healPrefab, Position, Quaternion.identity);
    }
}