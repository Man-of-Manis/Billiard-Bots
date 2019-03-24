using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickupHolder : MonoBehaviour
{

    // Update is called once per frame
    void LateUpdate()
    {
        transform.rotation = Quaternion.Euler(0f, 0f, 0f);   
    }
}
