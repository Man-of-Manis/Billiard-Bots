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
            //if(transform.parent.GetComponent<SpireMagnetism>().spireMagnetism != other.collider.GetComponent<PlayerMagnetism>().positiveMagnetism)
            if(transform.GetComponent<SpireMagnetism>().spireMagnetism != other.collider.GetComponent<PlayerMagnetism>().positiveMagnetism)
            {
                StartCoroutine(Magnetism(other.gameObject));
            }           
        }
    }

    IEnumerator Magnetism(GameObject player)
    {
        Vector3 spirePos = new Vector3(transform.position.x, player.transform.position.y, transform.position.z);
        float dist = Vector3.Distance(player.transform.position, spirePos);
        yield return new WaitForSeconds(1f);
        Debug.Log(player.gameObject.name);
        player.gameObject.GetComponent<PlayerMagnetism>().ChangeMagnetism();
        player.gameObject.GetComponent<Rigidbody>().AddForce((player.transform.position - spirePos) * (1 / (dist * dist)) * magnetismForce, ForceMode.Impulse);
        AudioManager.instance.Play("MagnetPoleThrow");
        yield return null;
    }
}
