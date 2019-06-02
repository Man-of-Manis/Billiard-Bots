using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPickup : MonoBehaviour
{
    public int turns = 1;
    public float maxPower = 1.5f;

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
        /*
        SpeedIncrease increase = other.gameObject.AddComponent<SpeedIncrease>();
        increase.turnAmount = turns;
        increase.maxPowerIncrease = maxPower;
        */

        AudioManager.instance.Play("Speed");

        other.GetComponent<PlayerStats>().PickupItem(PlayerCollectedItem.CollecedItem.SpeedBoost, turns, maxPower);
        PlayerPickupUI.Instance.PickedUp(1);

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
