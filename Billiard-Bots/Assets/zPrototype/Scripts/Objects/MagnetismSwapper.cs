using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetismSwapper : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Pickup(other);
        }
    }

    private void Pickup(Collider other)
    {
        other.GetComponent<PlayerMagnetism>().ChangeMagnetism();
        //other.GetComponent<Rigidbody>().velocity *= 0.001f;
        other.GetComponent<PlayerStats>().PickupItem(PlayerCollectedItem.CollecedItem.PolarityReverser, 5f);
        PlayerPickupUI.Instance.PickedUp(3);

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
