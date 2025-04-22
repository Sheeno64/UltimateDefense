using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int gold = 200;
    public int playerHealth = 10;
    public Transform[] pathPoints;
    public TMP_Text goldText;
    public TMP_Text healthText;
    public TMP_Text waveText;
    public GameObject gameOverPanel;
    public bool noMoreWaves = false;
    public AudioClip shootSFX;
    public AudioClip deathSFX;

    void Awake() { Instance = this; }

    void Update()
    {
        goldText.text = "Gold: " + gold;
        healthText.text = "HP: " + playerHealth;

        if (playerHealth <= 0)
        {
            gameOverPanel.SetActive(true);
            Time.timeScale = 0f;
        }

        if (noMoreWaves && GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            healthText.text = "YOU WIN!";
        }

        if (waveText != null)
        {
            int waveNum = WaveManager.Instance != null ? WaveManager.Instance.GetCurrentWaveNumber() + 1 : 1;
        }
    }

    public void PlayerHit()
    {
        playerHealth--;
    }

    public void AddGold(int amount)
    {
        gold += amount;
    }

    public void UpdateWaveText()
    {
        Debug.Log("UpdateWaveText() CALLED");

        if (waveText == null)
        {
            Debug.LogError(" waveText is NULL");
            return;
        }

        if (WaveManager.Instance == null)
        {
            Debug.LogError(" WaveManager.Instance is NULL");
            return;
        }

        int waveNum = WaveManager.Instance.GetCurrentWaveNumber();
        waveText.text = "Wave " + waveNum + "/" + WaveManager.Instance.totalWaves;

        Debug.Log(" waveText updated to: Wave " + waveNum + "/" + WaveManager.Instance.totalWaves);
    }
}
