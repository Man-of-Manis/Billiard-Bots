using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetismSwitch : MonoBehaviour
{
    public float magnetismForce;

    public bool destroy = false;

    void OnCollisionEnter(Collision other)
    {
        if(other.collider.CompareTag("Player"))
        {
            if(transform.parent.GetComponent<SpireMagnetism>().spireMagnetism != other.collider.GetComponent<PlayerMagnetism>().positiveMagnetism)
            {
                StartCoroutine(Magnetism(other));
            }           
        }
    }

    IEnumerator Magnetism(Collision player)
    {
        Vector3 spirePos = new Vector3(transform.position.x, player.transform.position.y, transform.position.z);
        float dist = Vector3.Distance(player.transform.position, spirePos);
        yield return new WaitForSeconds(1f);
        player.collider.GetComponent<PlayerMagnetism>().ChangeMagnetism();
        player.collider.GetComponent<Rigidbody>().AddForce((player.transform.position - spirePos) * (1 / (dist * dist)) * magnetismForce, ForceMode.Impulse);
        yield return null;
    }
}
