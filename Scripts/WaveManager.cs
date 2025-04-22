using System.Collections;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public static WaveManager Instance;

    public GameObject enemyPrefab;
    public Transform spawnPoint;
    
    [HideInInspector]
    public int[] enemiesPerWave;
    private int currentWave = 0;
    public int totalWaves => enemiesPerWave.Length;

    void Awake()
    {
        Instance = this;
        enemiesPerWave = new int[] { 5, 8, 10, 12, 15, 18, 20, 24, 28, 32 };
    }

    public void StartNextWave()
    {
        if (currentWave < enemiesPerWave.Length)
        {
            currentWave++;
            GameManager.Instance.UpdateWaveText();
            StartCoroutine(SpawnWave());
        }
    }

    IEnumerator SpawnWave()
    {
        int count = enemiesPerWave[currentWave];
        for (int i = 0; i < count; i++)
        {
            GameObject e = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
            Enemy enemy = e.GetComponent<Enemy>();
            enemy.health += currentWave;
            enemy.speed += currentWave * 0.05f;
            yield return new WaitForSeconds(1f);
        }
        if (currentWave >= enemiesPerWave.Length)
        {
            GameManager.Instance.noMoreWaves = true;
        }
    }

    public int GetCurrentWaveNumber()
    {
        return currentWave;
    }
}
