using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DietOilPickup : MonoBehaviour
{
    public int turns = 1;
    public float mass = 9f;

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            MassChange change = other.gameObject.AddComponent<MassChange>();
            change.turnAmount = turns;
            change.newMass = mass;
            Destroy(gameObject);
        }
    }
}
