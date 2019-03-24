using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileLaunched : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;

    public float missileSpeed;

    public float rotationSpeed = 200f;

    public float explosionRadius;

    public float explosionForce;

    public LayerMask explosionMask;

    public GameObject explosion;

    public GameObject smokeTrail;

    private Transform enemy;

    private bool launched = false;


    // Update is called once per frame
    void FixedUpdate()
    {
        if(launched)
        {
            transform.SetParent(null);
            rb.isKinematic = false;
            LaunchRocket();
            smokeTrail.GetComponent<ParticleSystem>().Play();
        }
    }

    public void Launching(Transform enemy)
    {
        this.enemy = enemy;
        launched = true;
    }

    void LaunchRocket()
    {
        Vector3 direction = enemy.position - transform.position;

        direction.Normalize();

        Vector3 rotateAmount = Vector3.Cross(direction, transform.forward);

        rb.angularVelocity = -rotateAmount * rotationSpeed;

        rb.velocity = transform.forward * missileSpeed;
    }
    

    void OnCollisionEnter(Collision other)
    {
        if(launched)
        {
            GetColliders();
            Instantiate(explosion, transform.position, Quaternion.identity);
            smokeTrail.transform.SetParent(null);
            smokeTrail.GetComponent<ParticleSystem>().Stop();
            Destroy(gameObject);
        }
    }

    void GetColliders()
    {
        Collider[] players = Physics.OverlapSphere(transform.position, explosionRadius, explosionMask);

        foreach (Collider player in players)
        {
            float dist = Vector3.Distance(player.transform.position, transform.position);
            player.GetComponent<PlayerHealth>().SubHealth((int)(explosionRadius - dist));
            player.GetComponent<Rigidbody>().velocity *= 0.00001f;
            player.GetComponent<Rigidbody>().AddForce((player.transform.position - transform.position) * (1 / (dist * dist)) * explosionForce, ForceMode.Impulse);

        }
    }
}
