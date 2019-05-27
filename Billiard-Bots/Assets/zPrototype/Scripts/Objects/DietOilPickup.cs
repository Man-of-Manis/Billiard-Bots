using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DietOilPickup : MonoBehaviour
{
    public int turns = 1;
    public float mass = 9f;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Pickup(other);
        }
    }

    private void Pickup(Collider other)
    {
        /*
        MassChange change = other.gameObject.AddComponent<MassChange>();
        change.turnAmount = turns;
        change.newMass = mass;
        */

        other.GetComponent<PlayerStats>().PickupItem(PlayerCollectedItem.CollecedItem.DietOil, turns, mass);
        PlayerPickupUI.Instance.PickedUp(2);

        if (GetComponentInParent<ItemSelector>() != null)
        {
            gameObject.SetActive(false);
            GetComponentInParent<ItemSelector>().Taken();
        }

        else
        {
            Destroy(gameObject);
        }
    }
}
