using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonScript : MonoBehaviour
{
    public Vector2 timeInterval = new Vector2(1, 1);
    private float cooldown = 3f;
    public List<GameObject> bombPrefabs;

    public GameObject bombSpawnPoint;
    public GameObject target;
    public float rangeInDegrees;
    public float arcDegrees;
    public Vector2 force;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //ignore if game is over
        if (GameManager.Instance.hasWon)
        {
            Destroy(gameObject);
        }
        if (GameManager.Instance.isGameOver) { return; }


        cooldown -= Time.deltaTime;
        if (cooldown < 0)
        {
            cooldown = Random.Range(timeInterval.x, timeInterval.y);

            Fire();
        }

        
    }

    private void Fire()
    {
        //get Prefab
        GameObject bombPrefab = bombPrefabs[Random.Range(0, bombPrefabs.Count)];

        //create bomb
        GameObject bomb = Instantiate(bombPrefab, bombSpawnPoint.transform.position, bombPrefab.transform.rotation);

        //apply force
        Rigidbody bombRigidbody = bomb.GetComponent<Rigidbody>();
        Vector3 impulseVector = target.transform.position - bombSpawnPoint.transform.position;
        impulseVector.Scale(new Vector3(1, 0, 1));
        impulseVector.Normalize();        
        impulseVector += new Vector3(0,arcDegrees / 45f, 0);
        impulseVector.Normalize();
        impulseVector = Quaternion.AngleAxis(rangeInDegrees * Random.Range(-1f, 1f), Vector3.up) * impulseVector;
        impulseVector *= Random.Range(force.x, force.y);
        bombRigidbody.AddForce(impulseVector, ForceMode.Impulse);

    }
}
