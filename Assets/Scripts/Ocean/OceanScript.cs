using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OceanScript : MonoBehaviour
{

    private void OnTriggerEnter(Collider collision)
    {
        GameObject collisionObject = collision.gameObject;
        if (collisionObject.CompareTag("Player"))
        {
            GameManager.Instance.EndGame();
        }
    }
}
