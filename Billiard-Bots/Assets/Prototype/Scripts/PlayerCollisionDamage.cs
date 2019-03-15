using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionDamage : MonoBehaviour
{
    void OnCollisionEnter(Collision other)
    {
        if(other.collider.CompareTag("Player") && !other.gameObject.Equals(gameObject) && !other.collider.gameObject.Equals(PlayerTurn.Instance.playerObjTurn))
        {
            Rigidbody rb = gameObject.GetComponent<Rigidbody>();

            float magnitude = rb.velocity.magnitude;

            int damage = (int)(magnitude / 100f * 10f);

            Debug.Log(gameObject.name + " has hit " + other.gameObject.name + " with a magnitude of " + magnitude + " dealing " + damage + " points of damage.");            

            other.gameObject.GetComponent<PlayerHealth>().SubHealth(damage);            

            Vector3 point = other.GetContact(0).normal; //point at which the other player was hit

            //Debug.Log(point + ", " + -point);

            other.gameObject.GetComponent<Rigidbody>().AddForce(-point * magnitude * 10f, ForceMode.Impulse);

            rb.velocity *= 0.0001f; //Sets velocity to almost zero so that character doesn't change

            rb.AddForce(0f, 35f, 0f, ForceMode.Impulse); //Adds a little hop to the character after hitting the other player
        }
    }
}
