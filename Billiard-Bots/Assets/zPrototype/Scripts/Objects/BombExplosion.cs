using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombExplosion : MonoBehaviour
{
    public float explosionRadius = 5f;    
    public float explosionForce = 400f;
    public float explosionDelay = 1f;
    //public LayerMask explosionMask;
    public GameObject explosion;


    // Start is called before the first frame update
    void OnCollisionEnter(Collision other)
    {
        if(other.collider.CompareTag("Player"))
        {
            PlayerTurn.Instance.ObjectActivated(true);
            StartCoroutine(Delay());
        }        
    }

    IEnumerator Delay()
    {
        
        //GetComponent<Animator>().SetTrigger("Explode");
        yield return new WaitForSeconds(explosionDelay);
        GetColliders();
        Instantiate(explosion, transform.position, Quaternion.identity);
        AudioManager.instance.Play("Explosion");
        PlayerTurn.Instance.ObjectActivated(false);
        Destroy(gameObject);
    }

    void GetColliders()
    {
        Collider[] players = Physics.OverlapSphere(transform.position, explosionRadius, LayerMask.GetMask("Player"));

        foreach (Collider player in players)
        {
            float dist = Vector3.Distance(player.transform.position, transform.position);
            int damage = (int)(explosionRadius - dist);
            player.GetComponent<PlayerHealth>().SubHealth(damage);
            player.GetComponent<Rigidbody>().velocity *= 0.00001f;
            player.GetComponent<Rigidbody>().AddForce((player.transform.position - transform.position) * (1 / (dist * dist)) * explosionForce, ForceMode.Impulse);
            Stats ps = player.GetComponent<PlayerStats>().playerStatistics;
            ps.timesBlownUp++;
            ps.damageTaken += damage;

        }
    }
}
