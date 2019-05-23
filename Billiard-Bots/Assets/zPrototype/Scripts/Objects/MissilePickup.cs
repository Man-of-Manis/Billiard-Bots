using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissilePickup : MonoBehaviour
{
    public GameObject playerMissile;

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
        GameObject empty = new GameObject("Missile Trigger");
        empty.transform.parent = other.transform.Find("PickupHolder").transform;
        empty.transform.localPosition = Vector3.zero;
        empty.transform.localEulerAngles = Vector3.zero;
        empty.layer = LayerMask.NameToLayer("IgnoreHomingBomb");

        GameObject missile = Instantiate(playerMissile, other.transform.Find("PickupHolder").transform);

        SphereCollider sphere = empty.AddComponent<SphereCollider>();
        sphere.isTrigger = true;
        sphere.radius = 6f;
        empty.AddComponent<PlayerMissileTrigger>().SetOwner(other.gameObject, missile, sphere);
        other.GetComponent<PlayerStats>().PickupItem(PlayerCollectedItem.CollecedItem.HomingBomb);


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
