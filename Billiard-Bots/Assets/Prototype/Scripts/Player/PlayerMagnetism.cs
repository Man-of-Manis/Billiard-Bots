using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMagnetism : MonoBehaviour
{
    public bool positiveMagnetism;

    // Start is called before the first frame update
    void Start()
    {
        positiveMagnetism = Random.value > 0.5f;
    }

    public void ChangeMagnetism()
    {
        positiveMagnetism = !positiveMagnetism;
    }
}
