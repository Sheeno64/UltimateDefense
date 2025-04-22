using System.Collections;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public float range = 3f;
    public float fireRate = 1f;
    public int damage = 1;
    public GameObject bulletPrefab;
    public Transform firePoint;
    private float fireCooldown = 0f;

    void Update()
    {
        fireCooldown -= Time.deltaTime;
        GameObject target = FindTarget();
        if (target != null && fireCooldown <= 0f)
        {
            Shoot(target);
            fireCooldown = fireRate;
        }
    }

    GameObject FindTarget()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, range);
        foreach (var hit in hits)
        {
            if (hit.CompareTag("Enemy"))
            {
                Debug.Log("Enemy found in range: " + hit.name);
                return hit.gameObject;
            }    
        }
        return null;
    }

    void Shoot(GameObject target)
    {
        Debug.Log("Attempting to shoot at: " + target.name);
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        bullet.GetComponent<Bullet>().SetTarget(target, damage);
        AudioSource.PlayClipAtPoint(GameManager.Instance.shootSFX, transform.position);
    }
}
