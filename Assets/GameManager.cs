using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private const float ROOM_HEIGHT = 2;

    [SerializeField] private Room roomPrefab;
    [SerializeField] private GameObject roofPrefab;
    [SerializeField] private int numTowers = 5;
    [SerializeField] private float startY = -2.5f;
    [SerializeField] private int minTowerHeight = 2;
    [SerializeField] private int maxTowerHeight = 5;
    [SerializeField] private float towerSeparation = 3f;

    private void Start()
    {
        float spawnX = 0f;

        for (int i = 0; i < numTowers; i++)
        {
            float spawnY = startY;

            int towerHeight = Random.Range(minTowerHeight, maxTowerHeight);
            for (int j = 0; j < towerHeight; j++)
            {
                Instantiate(roomPrefab, new Vector2(spawnX, spawnY), Quaternion.identity);
                spawnY += ROOM_HEIGHT;
            }

            Instantiate(roofPrefab, new Vector2(spawnX, spawnY), Quaternion.identity);

            spawnX += towerSeparation;
        }
    }
}
