using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPlataform : MonoBehaviour
{
    private Vector3 originalPosition;

    
    private void Awake()
    {
        originalPosition = transform.position;
    }
    private void OnDestroy()
    {
        if(GameManager.Instance == null) { return; }
        GameManager.Instance.Respawn(originalPosition);
    }


}
