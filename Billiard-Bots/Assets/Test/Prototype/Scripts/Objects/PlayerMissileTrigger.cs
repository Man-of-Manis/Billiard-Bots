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
        if (other.CompareTag("Player") && other.name != gameObject.name)
        {
            missile.GetComponent<MissileLaunched>().Launching(other.transform);
            Destroy(sphere);
            Destroy(this);
        }
    }

    public void SetOwner(GameObject player, GameObject missile, SphereCollider sphere)
    {
        this.player = player;
        this.missile = missile;
        this.sphere = sphere;
    }
}
