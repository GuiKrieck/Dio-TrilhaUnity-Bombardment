using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordHitboxScript : MonoBehaviour
{
    public PlayerController playerController;

    private void OnTriggerEnter(Collider other)
    {
        playerController.onSwordCollisionEnter(other);
    }
}
