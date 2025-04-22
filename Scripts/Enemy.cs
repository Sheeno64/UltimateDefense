using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 2f;
    public int health = 3;
    private int waypointIndex = 0;

    void Update()
    {
        if (GameManager.Instance.pathPoints.Length == 0) return;
        Transform target = GameManager.Instance.pathPoints[waypointIndex];
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, target.position) < 0.1f)
        {
            waypointIndex++;
            if (waypointIndex >= GameManager.Instance.pathPoints.Length)
            {
                GameManager.Instance.PlayerHit();
                Destroy(gameObject);
            }
        }
    }

    public void TakeDamage(int dmg)
    {
        health -= dmg;
        if (health <= 0)
        {
            GameManager.Instance.AddGold(10);
            AudioSource.PlayClipAtPoint(GameManager.Instance.deathSFX, transform.position);
            Destroy(gameObject);
        }
    }
}
