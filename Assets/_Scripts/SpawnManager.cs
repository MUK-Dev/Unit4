using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private GameObject powerupPrefab;

    private float spawnRange = 9f;
    private int enemyCount;
    private int waveNumber = 1;

    private void Start()
    {
        SpawnEnemyWave(waveNumber);
    }

    private void Update()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length;
        if (enemyCount == 0)
        {
            waveNumber++;
            SpawnEnemyWave(waveNumber);
        }
    }

    private void SpawnEnemyWave(int enemiesToSpawn)
    {
        Instantiate(powerupPrefab, GenerateSpawnPosition(), powerupPrefab.transform.rotation);
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            GameObject randomEnemy = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
            Instantiate(randomEnemy, GenerateSpawnPosition(), randomEnemy.transform.rotation);
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
