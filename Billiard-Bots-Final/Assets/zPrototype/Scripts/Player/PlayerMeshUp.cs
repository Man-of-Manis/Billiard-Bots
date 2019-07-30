using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeshUp : MonoBehaviour
{
    public Camera cam;

    private void Start()
    {
        cam = FindObjectOfType<Camera>();
    }

    void LateUpdate()
    {
        transform.rotation = Quaternion.Euler(0f, cam.transform.eulerAngles.y, 0f);
    }
}
