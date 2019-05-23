using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPad : MonoBehaviour
{
    public float multiplier = 200f;


    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();

            Vector3 currentVelocity = rb.velocity;

            Vector3.Normalize(currentVelocity);

            rb.AddForce(currentVelocity * multiplier);
        }
    }
}
