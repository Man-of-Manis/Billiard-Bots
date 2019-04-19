using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpireMagnetism : MonoBehaviour
{
    public bool spireMagnetism;
    public float magnetismForce;

    // Start is called before the first frame update
    void Start()
    {
        spireMagnetism = Random.value > 0.5f;
    }

    void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if(other.GetComponent<PlayerMagnetism>().positiveMagnetism == spireMagnetism)
            {
                RepelPlayer(other);
            }

            else
            {
                AttractPlayer(other);
            }
        }
    }

    private void AttractPlayer(Collider other)
    {
        Vector3 spirePos = new Vector3(transform.position.x, 0f, transform.position.z);

        float dist = Vector3.Distance(other.transform.position, spirePos);

        other.GetComponent<Rigidbody>().AddForce(-(other.transform.position - spirePos) * (1 / (dist * dist)) * magnetismForce);
    }

    private void RepelPlayer(Collider other)
    {
        Vector3 spirePos = new Vector3(transform.position.x, other.transform.position.y, transform.position.z);

        float dist = Vector3.Distance(other.transform.position, spirePos);

        other.GetComponent<Rigidbody>().AddForce((other.transform.position - spirePos) * (1 / (dist * dist)) * magnetismForce);
    }
}
