using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMissileTrigger : MonoBehaviour
{
    private GameObject player;
    private GameObject missile;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && other.name != gameObject.name)
        {
            missile.GetComponent<MissileLaunched>().Launching(other.transform);
            Destroy(this);
        }
    }

    public void SetOwner(GameObject player, GameObject missile)
    {
        this.player = player;
        this.missile = missile;
    }
}
