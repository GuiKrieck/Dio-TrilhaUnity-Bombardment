using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombScript : MonoBehaviour
{
    public GameObject ExplosionPrefab;
    public GameObject woodBreakingPrefab;
    public float ExplosionDelay = 5f;
    public float blastRadius = 5f;
    public int blastDamage = 10;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ExplosionCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator ExplosionCoroutine()
    {
        // wait for the time of the delay in seconds
        yield return new WaitForSeconds(ExplosionDelay);

        //explode
        Explode();
    }


    private void Explode()
    {
        //Create Explosion
        Instantiate(ExplosionPrefab, transform.position, ExplosionPrefab.transform.rotation);

        //Destroy wood plataform in range
        //this will return an array of colliders, based on what the sphere that the physics created the size is deffined by the variable blast radius
        Collider[] colliders = Physics.OverlapSphere(transform.position, blastRadius);
        //this will iterate on the array we just got 
        foreach(Collider collider in colliders)
        {
            //getting the gameobject of the collider, and checking if it has the tag "plataform"
            GameObject hitObject = collider.gameObject;
            if (hitObject.CompareTag("Plataform"))
            {
                //getting the component lifescript from the gameobject we got earlier and checking if it is not null
                LifeScript lifeScript = hitObject.GetComponent<LifeScript>();
                if (lifeScript != null)
                {
                    int damage = calculateDamage(hitObject);

                    //apllying the damage to the object and cheking if it's "dead"
                    lifeScript.health -= damage;
                    if(lifeScript.health <= 0)
                    {
                        Instantiate(woodBreakingPrefab, gameObject.transform.position, woodBreakingPrefab.transform.rotation);
                        Destroy(hitObject);
                    }
                }               
            }            
        }

        //destroy bomb
        Destroy(gameObject);
    }

    private int calculateDamage(GameObject hitObject)
    {
        //this will calculate the damage in accordance to the distance the object is from the bomb.
        float distance = (hitObject.transform.position - transform.position).magnitude;
        float distanceRate = Mathf.Clamp(distance / blastRadius, 0, 1);
        float damageRate = 1f - Mathf.Pow(distanceRate, 4);
        return (int)Mathf.Ceil(damageRate * blastDamage);
            
        
    }
}
