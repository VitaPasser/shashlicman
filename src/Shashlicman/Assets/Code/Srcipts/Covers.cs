using System.Collections.Generic;
using UnityEngine;

public class Covers : MonoBehaviour
{
    public GameObject coversPrefab; // ѕрефаб укрыти€
    public int minCovers = 2;       // ћинимальное количество укрытий
    public int maxCovers = 4;       // ћаксимальное количество укрытий
    public Vector2 spawnAreaMin = new Vector2(-5.5f, -3f); // ћинимальные координаты области спавна (по умолчанию)
    public Vector2 spawnAreaMax = new Vector2(5f, 2f); // ћаксимальные координаты области спавна (по умолчанию)

    public float minDistance = 1.5f; // ћинимальное рассто€ние между укрыти€ми

    private List<Vector2> spawnedPositions = new List<Vector2>(); // —писок позиций уже заспавненных укрытий


    public void SpawnCovers()
    {
        // ѕровер€ем, были ли уже заспавнены укрыти€
        if (spawnedPositions.Count > 0)
        {
            // ”дал€ем заспавненные укрыти€
            foreach (Vector2 pos in spawnedPositions)
            {
                Destroy(GameObject.FindGameObjectWithTag("Cover"));
            }
            spawnedPositions.Clear();
        }

        // √енераци€ случайного количества укрытий
        int coversCount = Random.Range(minCovers, maxCovers);

        // —павн укрытий
        for (int i = 0; i < coversCount; i++)
        {
            SpawnCover();
        }
    }

    void SpawnCover()
    {
        int maxAttempts = 1000; // ћаксимальное количество попыток дл€ поиска позиции
        for (int attempt = 0; attempt < maxAttempts; attempt++)
        {
            // √енераци€ случайной позиции в области спавна
            float x = Random.Range(spawnAreaMin.x, spawnAreaMax.x);
            float y = Random.Range(spawnAreaMin.y, spawnAreaMax.y);
            Vector2 spawnPosition = new Vector2(x, y);

            // ѕроверка, что позици€ не слишком близко к другим укрыти€м
            bool tooClose = false;
            foreach (Vector2 pos in spawnedPositions)
            {
                if (Vector2.Distance(pos, spawnPosition) < minDistance)
                {
                    tooClose = true;
                    break;
                }
            }

            // ≈сли позици€ подходит, спавним укрытие и добавл€ем позицию в список
            if (!tooClose)
            {
                Instantiate(coversPrefab, spawnPosition, Quaternion.identity);
                spawnedPositions.Add(spawnPosition);
                return;
            }
        }

        Debug.LogWarning("Could not find a suitable position for a cover after multiple attempts.");
    }
}
