using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissilePickup : MonoBehaviour
{
    public GameObject playerMissile;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameObject missile = Instantiate(playerMissile, other.transform.Find("PickupHolder").transform);
            //missile.transform.localPosition = new Vector3(0f, 1.5f, 0f);
            
            SphereCollider sphere = other.gameObject.AddComponent<SphereCollider>();
            sphere.isTrigger = true;
            sphere.radius = 5f;
            other.gameObject.AddComponent<PlayerMissileTrigger>().SetOwner(other.gameObject, missile, sphere);
            Destroy(gameObject);
        }
    }
}
