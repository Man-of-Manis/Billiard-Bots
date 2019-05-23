using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTimer : MonoBehaviour
{
    public float timer = 4f;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, timer);
    }
}
