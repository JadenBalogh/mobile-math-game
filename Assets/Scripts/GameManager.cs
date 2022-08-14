using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    [SerializeField] private int minibossInterval = 6;

    private void Start()
    {
        float spawnX = 0f;

        for (int i = 0; i < numTowers; i++)
        {
            float spawnY = startY;
            int towerHeight = Random.Range(minTowerHeight, maxTowerHeight);
            Room.RoomType spawnType = Room.RoomType.Normal;

            if (i > 0 && i % minibossInterval == 0)
            {
                towerHeight = 1;
                spawnType = Room.RoomType.Miniboss;
            }

            if (i == numTowers - 1)
            {
                towerHeight = 1;
                spawnType = Room.RoomType.Boss;
            }

            for (int j = 0; j < towerHeight; j++)
            {
                Room room = Instantiate(roomPrefab, new Vector2(spawnX, spawnY), Quaternion.identity);
                room.StrengthMult = (j + 1) * 1.5f + i * i * Random.value;
                room.SpawnType = spawnType;
                spawnY += ROOM_HEIGHT;
            }

            Instantiate(roofPrefab, new Vector2(spawnX, spawnY), Quaternion.identity);

            spawnX += towerSeparation;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
