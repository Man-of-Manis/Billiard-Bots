using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPickup : MonoBehaviour
{
    public int turns = 1;
    public float maxPower = 1.5f;

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            other.gameObject.AddComponent<SpeedIncrease>();
            Destroy(gameObject);
        }
    }
}
