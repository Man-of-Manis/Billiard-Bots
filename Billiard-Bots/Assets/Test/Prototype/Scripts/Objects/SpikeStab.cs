using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeStab : MonoBehaviour
{
    public int spikeDamage = 2;
    [SerializeField] private Animator anim;

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            other.GetComponent<PlayerHealth>().SubHealth(spikeDamage);
            anim.SetTrigger("stab");
            Debug.Log(other.name + " hit spikes receiving " + spikeDamage + " damage");
        }
    }
}
