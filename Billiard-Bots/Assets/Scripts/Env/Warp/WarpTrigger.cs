using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpTrigger : MonoBehaviour
{
    public bool randomWarp = false;

    public Transform siblingWarpGate;

    public float exitWarpForce;

    private Vector3 exitDirection;

    private Transform exitPoint;

    // Start is called before the first frame update
    void Start()
    {
        exitDirection = siblingWarpGate.transform.forward;
        exitPoint = siblingWarpGate.Find("Exit_Point");
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            rb.velocity = Vector3.zero;
            other.transform.position = exitPoint.position;
            rb.AddForce(exitDirection * exitWarpForce, ForceMode.Impulse);
        }
    }
}
