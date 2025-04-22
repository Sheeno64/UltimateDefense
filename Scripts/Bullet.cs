using UnityEngine;

public class Bullet : MonoBehaviour
{
    private GameObject target;
    private int damage;
    public float speed = 5f;

    public bool isSplash = false;
    public float splashRadius = 1.5f;

    public void SetTarget(GameObject t, int dmg)
    {
        target = t;
        damage = dmg;
    }

    void Update()
    {
        if (target == null) {  Debug.Log("Target gone"); Destroy(gameObject); return; }
        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, target.transform.position) < 0.1f)
        {
            if (isSplash)
            {
                Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, splashRadius);
                foreach (var hit in hits)
                {
                    if (hit.CompareTag("Enemy"))
                    {
                        hit.GetComponent<Enemy>().TakeDamage(damage);
                    }
                }
            }
            else
            {
                target.GetComponent<Enemy>().TakeDamage(damage);
            }

            Destroy(gameObject);
        }
    }
}

