using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVelocity : MonoBehaviour
{

    private GameObject[] go;

    public Vector3[] velocity = new Vector3[4];

    // Start is called before the first frame update
    void Start()
    {
        go = GameObject.FindGameObjectsWithTag("Player");

        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < go.Length; i++)
        {
            velocity[i] = go[i].GetComponent<Rigidbody>().velocity;
        }
    }
}
