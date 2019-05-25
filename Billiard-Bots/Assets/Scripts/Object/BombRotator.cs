using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombRotator : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.eulerAngles = new Vector3(Random.Range(-25f, 25f), Random.Range(0f, 359f), Random.Range(-25f, 25f));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
