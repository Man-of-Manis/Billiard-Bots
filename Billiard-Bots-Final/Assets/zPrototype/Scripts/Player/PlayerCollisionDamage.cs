using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionDamage : MonoBehaviour
{
    public float playerDamageMultiplier = 1;

    public float playerPushMultiplier = 1;

    void OnCollisionEnter(Collision other)
    {
        if(other.collider.CompareTag("Player") && !other.gameObject.Equals(gameObject) && !other.collider.gameObject.Equals(PlayerTurn.Instance.playerObjTurn))
        {
            Rigidbody rb = gameObject.GetComponent<Rigidbody>();

            //gameObject.GetComponent<Animator>().SetTrigger("spike");

            float magnitude = rb.velocity.magnitude;

            int damage = (int)((magnitude * playerDamageMultiplier) / 90f * 10f);

            damage = Mathf.Clamp(damage, 0, 3);

            Debug.Log(gameObject.name + " has hit " + other.gameObject.name + " with a magnitude of " + magnitude + " dealing " + damage + " points of damage.");            

            other.gameObject.GetComponent<PlayerHealth>().SubHealth(damage);

            other.gameObject.GetComponent<PlayerStats>().playerStatistics.damageTaken += damage;

            Stats ps = GetComponent<PlayerStats>().playerStatistics;

            ps.damageDealt += damage;

            ps.enemiesHit++;

            Vector3 point = other.GetContact(0).normal; //point at which the other player was hit

            other.gameObject.GetComponent<Rigidbody>().AddForce(-point * magnitude * playerPushMultiplier * 10f, ForceMode.Impulse);

            rb.velocity *= 0.0001f; //Sets velocity to almost zero so that character doesn't change

            rb.AddForce(0f, 35f, 0f, ForceMode.Impulse); //Adds a little hop to the character after hitting the other player

            AudioManager.instance.Play("MetalHit1");
        }
    }
}
