using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bumper : MonoBehaviour
{
    public float bumpForce;

    private Animator anim;

    void Start()
    {
        if(GetComponent<Animator>() != null)
        {
            anim = GetComponent<Animator>();
        }        
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Player"))
        {
            PlayerTurn.Instance.ObjectActivated(true);
            other.collider.GetComponent<Rigidbody>().velocity *= 0.00001f;
            other.collider.GetComponent<Rigidbody>().AddForce((other.transform.position - new Vector3(transform.position.x, other.transform.position.y, transform.position.z)) * bumpForce, ForceMode.Impulse);

            if (anim != null)
            {
                anim.SetTrigger("bump");
            }
            
            PlayerTurn.Instance.ObjectActivated(false);
        }
    }
}
