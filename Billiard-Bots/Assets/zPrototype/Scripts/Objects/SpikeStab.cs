using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeStab : MonoBehaviour
{
    public int spikeDamage = 2;

    private Transform spikeGroup;

    Coroutine co;

    private void Start()
    {
        spikeGroup = transform.Find("SpikeConeGroup");
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            other.GetComponent<PlayerHealth>().SubHealth(spikeDamage);
            AudioManager.instance.Play("SpikesHit");
            Stats ps = other.GetComponent<PlayerStats>().playerStatistics;
            ps.timesSpiked++;
            ps.damageTaken += spikeDamage;
            if(co != null)
            {
                StopCoroutine(co);
            }
            co = StartCoroutine(Stab());
            Debug.Log(other.name + " hit spikes receiving " + spikeDamage + " damage");
        }
    }

    IEnumerator Stab()
    {
        spikeGroup.localPosition = Vector3.zero;
        yield return new WaitForSeconds(0.5f);
        spikeGroup.localPosition = new Vector3(0f, -1.5f, 0f);
    }
}
