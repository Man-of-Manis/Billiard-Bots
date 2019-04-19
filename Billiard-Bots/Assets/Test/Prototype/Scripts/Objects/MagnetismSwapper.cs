using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetismSwapper : MonoBehaviour
{
    void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Player"))
        {
            other.collider.GetComponent<PlayerMagnetism>().ChangeMagnetism();
            other.collider.GetComponent<Rigidbody>().velocity *= 0.001f;
            Destroy(gameObject);
        }
    }
}
