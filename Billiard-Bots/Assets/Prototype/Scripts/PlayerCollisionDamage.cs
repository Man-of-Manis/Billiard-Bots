using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionDamage : MonoBehaviour
{
    void OnCollisionEnter(Collision other)
    {
        if(other.collider.CompareTag("Player") && !other.gameObject.Equals(gameObject) && !other.collider.gameObject.Equals(PlayerTurn.Instance.playerObjTurn))
        {
            float magnitude = gameObject.GetComponent<Rigidbody>().velocity.magnitude;

            int damage = (int)(magnitude / 100f * 10f);

            Debug.Log(gameObject.name + " has hit " + other.gameObject.name + " with a magnitude of " + magnitude + " dealing " + damage + " points of damage.");            

            other.gameObject.GetComponent<PlayerHealth>().SubHealth(damage);
        }
    }
}
