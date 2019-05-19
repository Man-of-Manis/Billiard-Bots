﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpTrigger : MonoBehaviour
{
    public bool randomWarp = false;

    public Transform siblingWarpGate;

    public float exitWarpForce;

    private List<GameObject> teleportingPlayers = new List<GameObject>();


    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player") && !teleportingPlayers.Contains(other.gameObject))
        {
            if(!randomWarp)
            {
                Teleport(other, siblingWarpGate);
            }

            else
            {
                GameObject[] warps = GameObject.FindGameObjectsWithTag("WarpGate");

                bool same = true;

                while(same)
                {
                    int num = Random.Range(0, warps.Length);

                    if(!warps[num].Equals(gameObject))
                    {
                        Teleport(other, warps[num].transform);
                        same = false;
                    }
                }                
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player") && teleportingPlayers.Contains(other.gameObject))
        {
            teleportingPlayers.Remove(other.gameObject);
            Debug.Log(other.gameObject.name + " has exited the warp gate.");
        }
    }

    public void Teleport(Collider other, Transform exit)
    {
        Rigidbody rb = other.GetComponent<Rigidbody>();
        Debug.Log(rb.velocity.magnitude);

        if(rb.velocity.magnitude > 5f)
        {
            float mag = rb.velocity.magnitude;
            rb.velocity = Vector3.zero;
            exit.GetComponent<WarpTrigger>().PlayerList(other.gameObject);
            other.transform.position = exit.Find("Exit_Point").transform.position;
            rb.AddForce(exit.forward * mag * 10f, ForceMode.Impulse);
        }

        else
        {
            rb.velocity = Vector3.zero;
            exit.GetComponent<WarpTrigger>().PlayerList(other.gameObject);
            other.transform.position = exit.Find("Exit_Point").transform.position;
            rb.AddForce(exit.forward * exitWarpForce, ForceMode.Impulse);
        }        
    }

    public void PlayerList(GameObject player)
    {
        teleportingPlayers.Add(player);
    }
}