using System.Collections.Generic;
using UnityEngine;

public class Covers : MonoBehaviour
{
    public GameObject coversPrefab; // ������ �������
    public int minCovers = 2;       // ����������� ���������� �������
    public int maxCovers = 4;       // ������������ ���������� �������
    public Vector2 spawnAreaMin = new Vector2(-5.5f, -3f); // ����������� ���������� ������� ������ (�� ���������)
    public Vector2 spawnAreaMax = new Vector2(5f, 2f); // ������������ ���������� ������� ������ (�� ���������)

    public float minDistance = 1.5f; // ����������� ���������� ����� ���������

    private List<Vector2> spawnedPositions = new List<Vector2>(); // ������ ������� ��� ������������ �������


    public void SpawnCovers()
    {
        // ���������, ���� �� ��� ���������� �������
        if (spawnedPositions.Count > 0)
        {
            // ������� ������������ �������
            foreach (Vector2 pos in spawnedPositions)
            {
                Destroy(GameObject.FindGameObjectWithTag("Cover"));
            }
            spawnedPositions.Clear();
        }

        // ��������� ���������� ���������� �������
        int coversCount = Random.Range(minCovers, maxCovers);

        // ����� �������
        for (int i = 0; i < coversCount; i++)
        {
            SpawnCover();
        }
    }

    void SpawnCover()
    {
        int maxAttempts = 1000; // ������������ ���������� ������� ��� ������ �������
        for (int attempt = 0; attempt < maxAttempts; attempt++)
        {
            // ��������� ��������� ������� � ������� ������
            float x = Random.Range(spawnAreaMin.x, spawnAreaMax.x);
            float y = Random.Range(spawnAreaMin.y, spawnAreaMax.y);
            Vector2 spawnPosition = new Vector2(x, y);

            // ��������, ��� ������� �� ������� ������ � ������ ��������
            bool tooClose = false;
            foreach (Vector2 pos in spawnedPositions)
            {
                if (Vector2.Distance(pos, spawnPosition) < minDistance)
                {
                    tooClose = true;
                    break;
                }
            }

            // ���� ������� ��������, ������� ������� � ��������� ������� � ������
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
