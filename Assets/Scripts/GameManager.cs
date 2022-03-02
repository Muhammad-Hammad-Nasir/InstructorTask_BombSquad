using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    public GameObject enemyPrefab;
    public GameObject[] powerupPrefab;
    public int enemyCount;
    public int waveNumber = 1;

    private float spawnRange = 11.0f;
    private int randomPrefab;

    void Start()
    {
        SpawnEnemyWave(waveNumber);
        Instantiate(powerupPrefab[randomPrefab], GenerateSpawnPosition(), powerupPrefab[randomPrefab].transform.rotation);
    }

    void Update()
    {
        enemyCount = FindObjectsOfType<EnemyScript>().Length;
        if (enemyCount == 0 && player != null)
        {
            waveNumber++;
            SpawnEnemyWave(waveNumber);
            Instantiate(powerupPrefab[randomPrefab], GenerateSpawnPosition(), powerupPrefab[randomPrefab].transform.rotation);
        }
    }

    void SpawnEnemyWave(int enemiesToSpawn)
    {
        randomPrefab = Random.Range(0, powerupPrefab.Length);
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
        }
    }

    private Vector3 GenerateSpawnPosition()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);
        Vector3 randomPos = new Vector3(spawnPosX, 0, spawnPosZ);

        return randomPos;
    }
}
