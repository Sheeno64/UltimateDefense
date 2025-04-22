using UnityEngine;

public class TowerPlacer : MonoBehaviour
{
    public GameObject[] towerPrefabs;
    private GameObject towerToPlace;

    public void SelectTower(int index)
    {
        towerToPlace = towerPrefabs[index];
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && towerToPlace != null)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D hit = Physics2D.OverlapPoint(mousePos);
            if (hit != null && hit.CompareTag("BuildZone"))
            {
                int cost = towerToPlace.GetComponent<Tower>().damage * 50;
                if (GameManager.Instance.gold >= cost)
                {
                    Instantiate(towerToPlace, hit.transform.position, Quaternion.identity);
                    GameManager.Instance.gold -= cost;
                    Destroy(hit.gameObject);
                    towerToPlace = null;
                }
            }
        }
    }
}
