using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpireMagnetism : MonoBehaviour
{
    public bool spireMagnetism;
    public float magnetismForce;
    public float rad;
    private AudioSource src;

    // Start is called before the first frame update
    void Start()
    {
        src = GetComponent<AudioSource>();

        rad = GetComponent<SphereCollider>().radius;

        spireMagnetism = Random.value > 0.5f;

        if(spireMagnetism)
        {
            var ps = GetComponentInChildren<ParticleSystem>().main;
            ps.startColor = new Color(1f, 0.1f, 0.1f, 0.25f);
        }

        else
        {
            var ps = GetComponentInChildren<ParticleSystem>().main;
            ps.startColor = new Color(0.1f, 0.3f, 9f, 0.25f);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if(other.GetComponent<PlayerMagnetism>().positiveMagnetism == spireMagnetism)
            {
                Magnetism(other, 1f);
            }

            else
            {
                Magnetism(other, -1f);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            src.Stop();
        }
    }

    private void Magnetism(Collider other, float polarity)
    {
        Vector3 toCenter = new Vector3(other.gameObject.transform.position.x - transform.position.x, 0, other.gameObject.transform.position.z - transform.position.z);

        float forceMod = ((rad - toCenter.magnitude) / rad) * ((rad - toCenter.magnitude) / rad);

        toCenter = toCenter.normalized * polarity * magnetismForce * forceMod;

        Rigidbody rb = other.GetComponent<Rigidbody>();

        rb.velocity = new Vector3(rb.velocity.x + toCenter.x, rb.velocity.y, rb.velocity.z + toCenter.z).normalized * rb.velocity.magnitude;

        src.volume = rb.velocity.magnitude / 7.5f;  

        if(!src.isPlaying)
        {
            src.Play();
        }        
    }
}
