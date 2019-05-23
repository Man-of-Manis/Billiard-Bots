using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMissileTrigger : MonoBehaviour
{
    public GameObject player;
    public GameObject missile;
    public SphereCollider sphere;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(other.GetComponent<PlayerIdentifier>().player != GetComponentInParent<PlayerIdentifier>().player)
            {
                Debug.Log(other.gameObject.name);
                missile.GetComponent<MissileLaunched>().Launching(other.transform);
                player.GetComponent<PlayerStats>().UsedItem();
                Destroy(gameObject);
            }            
        }
    }

    public void SetOwner(GameObject player, GameObject missile, SphereCollider sphere)
    {
        this.player = player;
        this.missile = missile;
        this.sphere = sphere;
    }
}
