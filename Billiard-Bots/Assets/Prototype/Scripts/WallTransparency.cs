using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallTransparency : MonoBehaviour
{
    private const float dist = 8f;

    public LayerMask wallMask;

    [SerializeField] private GameObject hitObj;

    [SerializeField] private GameObject prevHitObj;


    void FixedUpdate()
    {
        RayCasting();
    }

    void RayCasting()
    {
        Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, dist, wallMask);

        if (!hit.collider.CompareTag("Player"))
        {
            if (hitObj == null)
            {
                hitObj = hit.collider.gameObject;
            }

            if (hitObj != prevHitObj)
            {
                SetTransparent();
                prevHitObj = hitObj;
            }

        }

        else if (hitObj != null)
        {
            SetOpaque();
            prevHitObj = null;
            hitObj = null;
        }
    }

    void SetTransparent()
    {
        Color wall = hitObj.GetComponent<MeshRenderer>().material.color;
        wall.a = 0.5f;
        hitObj.GetComponent<MeshRenderer>().material.color = wall;
    }

    void SetOpaque()
    {
        Color wall = prevHitObj.GetComponent<MeshRenderer>().material.color;
        wall.a = 1f;
        prevHitObj.GetComponent<MeshRenderer>().material.color = wall;
    }

}
