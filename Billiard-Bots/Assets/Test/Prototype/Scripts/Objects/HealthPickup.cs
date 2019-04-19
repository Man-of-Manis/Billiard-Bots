using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int health = 2;

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            other.GetComponent<PlayerHealth>().AddHealth(health);
            Debug.Log(other.name + " picked up " + health + " health!");
            Destroy(gameObject);
        }     
    }
}
