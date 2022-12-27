using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionCheck : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.LogWarning(1);
        if (other.gameObject.CompareTag("Player"))
            gameObject.transform.GetComponentInParent<ZombieController>().Hit();
    }
}
