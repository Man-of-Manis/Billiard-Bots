using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleporterTexture : MonoBehaviour
{
    float i = 0;

    MeshRenderer mesh;

    // Start is called before the first frame update
    void Start()
    {
        mesh = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //transform.localRotation = Quaternion.Euler(0f, 0f, i++);

        i += Time.deltaTime;

        if(i >= 6.25f)
        {
            i = 0.0f;
        }

        mesh.material.SetFloat("_Angle", i);
    }
}
