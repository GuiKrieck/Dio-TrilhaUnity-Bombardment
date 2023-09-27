using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipControllerScript : MonoBehaviour
{
    private LifeScript shipLife;
    [SerializeField] private GameObject superExplosionPrefab;
    // Start is called before the first frame update
    void Start()
    {
        shipLife = GetComponent<LifeScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (shipLife.health <= 0)
        {
            Instantiate(superExplosionPrefab, transform.position, superExplosionPrefab.transform.rotation);
            Destroy(gameObject);
            GameManager.Instance.hasWon = true;
            GameManager.Instance.EndGame();
        }
    }

    public void TakeDamage(int Damage)
    {
        shipLife.health -= Damage;
    }
}
