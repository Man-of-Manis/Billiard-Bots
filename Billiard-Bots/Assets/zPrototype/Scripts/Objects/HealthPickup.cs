using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int health = 2;

    private string identity;

    void OnTriggerEnter(Collider other)
    {
        if(identity != null)
        {
            if (other.CompareTag("Player") && other.GetComponent<PlayerIdentifier>().player.ToString().Equals(identity))
            {
                Pickup(other);
            }
        }

        else
        {
            if (other.CompareTag("Player"))
            {
                Pickup(other);
            }
        }  
    }

    private void Pickup(Collider other)
    {
        other.GetComponent<PlayerHealth>().AddHealth(health);
        Debug.Log(other.name + " picked up " + health + " health!");
        other.GetComponent<PlayerStats>().PickupItem(PlayerCollectedItem.CollecedItem.ReparKit, 5f);
        PlayerPickupUI.Instance.PickedUp(0);

        AudioManager.instance.Play("ItemGet");

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

    public void SetPlayer(string identifier)
    {
        identity = identifier;
    }
}
