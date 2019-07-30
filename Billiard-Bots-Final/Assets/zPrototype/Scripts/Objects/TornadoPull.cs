using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TornadoPull : MonoBehaviour
{
    public float magnetismForce;

    public List<GameObject> pullOBj;

    private CapsuleCollider coll;

    public float endHeight;

    public float radius;

    private void Start()
    {
        coll = GetComponent<CapsuleCollider>();
    }


    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //pullOBj.Add(other.gameObject);

            if(Vector3.Distance(other.transform.position, transform.position) > 1f && Vector3.Distance(other.transform.position, transform.position) < coll.radius)
            {
                AttractPlayer(other.gameObject);
            }

            else if(Vector3.Distance(other.transform.position, transform.position) <= 1f && !Tornado(other.gameObject))
            {
                StartCoroutine(Spin(other.transform));

                pullOBj.Add(other.gameObject);
            }
        }
    }

    bool Tornado(GameObject go)
    {
        return pullOBj.Contains(go);
    }

    void AttractPlayer(GameObject go)
    {
        Vector3 spirePos = new Vector3(transform.position.x, 0f, transform.position.z);

        float dist = Vector3.Distance(go.transform.position, spirePos);

        go.GetComponent<Rigidbody>().AddForce(-(go.transform.position - spirePos) * (1 / (dist * dist)) * magnetismForce);
    }

    IEnumerator Spin(Transform player)
    {
        for (float i = 0; i < endHeight; i += Time.deltaTime / 0.1f)
        {
            float x = Mathf.Cos(i) * radius;
            float z = Mathf.Sin(i) * radius;
            Vector3 pos = new Vector3(x, i, z) + transform.position;
            player.position = Vector3.Lerp(player.position, pos, i / endHeight);

            yield return null;
        }
        Vector3 velocity = player.GetComponent<Rigidbody>().velocity;
        
        Vector3.Normalize(velocity);
        Debug.Log(velocity + " into " + (new Vector3(velocity.x, 1f, velocity.z) * 20f));
        player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        player.GetComponent<Rigidbody>().AddForce(new Vector3(velocity.x, 1f, velocity.z) * 20f, ForceMode.Impulse);
        pullOBj.Remove(player.gameObject);
    }

    
}
