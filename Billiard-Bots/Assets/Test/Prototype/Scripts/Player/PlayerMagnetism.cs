using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMagnetism : MonoBehaviour
{
    public bool positiveMagnetism;

    public Gradient posPolarity = new Gradient()
    {
        colorKeys = new GradientColorKey[2] 
        {
            new GradientColorKey(new Color(1f, 0f, 0f), 0f),
            new GradientColorKey(new Color(1f, 0f, 0f), 1f)
        },
        alphaKeys = new GradientAlphaKey[2] 
        {
            new GradientAlphaKey(0.25f, 0f),
            new GradientAlphaKey(0f, 1f)
        }
    };

    public Gradient negPolarity = new Gradient()
    {
        colorKeys = new GradientColorKey[2]
        {
            new GradientColorKey(new Color(0f, 0.5f, 1f), 0f),
            new GradientColorKey(new Color(0f, 0.5f, 1f), 1f)
        },
        alphaKeys = new GradientAlphaKey[2]
        {
            new GradientAlphaKey(0.25f, 0f),
            new GradientAlphaKey(0f, 1f)
        }
    };

    // Start is called before the first frame update
    void Start()
    {
        positiveMagnetism = Random.value > 0.5f;
        ChangeColor();
    }

    public void ChangeMagnetism()
    {
        positiveMagnetism = !positiveMagnetism;
        ChangeColor();
    }

    private void ChangeColor()
    {
        MeshRenderer[] rods = transform.GetChild(0).transform.Find("Cylinders").transform.GetComponentsInChildren<MeshRenderer>();

        foreach(MeshRenderer r in rods)
        {
            r.material.color = positiveMagnetism ? new Color(1f, 0.1f, 0.1f) : new Color(0.1f, 0.3f, 9f);
        }

        ParticleSystem ps = GetComponentInChildren<ParticleSystem>();

        var main = ps.main;
        main.startColor = positiveMagnetism ? new Color(1f, 0.1f, 0.1f, 0.0075f) : new Color(0.1f, 0.3f, 9f, 0.0075f);

        var col = ps.colorOverLifetime;
        col.enabled = false;
        /*
        posPolarity.SetKeys(new GradientColorKey[] { new GradientColorKey(new Color(1f, 0f, 0f), 0.0f), new GradientColorKey(new Color(1f, 0f, 0f), 1.0f) }, new GradientAlphaKey[] { new GradientAlphaKey(0.25f, 0.0f), new GradientAlphaKey(0.0f, 1.0f) });

        negPolarity.SetKeys(new GradientColorKey[] { new GradientColorKey(new Color(0f, 0.5f, 1f), 0.0f), new GradientColorKey(new Color(0f, 0.5f, 1f), 1.0f) }, new GradientAlphaKey[] { new GradientAlphaKey(0.25f, 0.0f), new GradientAlphaKey(0.0f, 1.0f) });

        col.color = positiveMagnetism ? posPolarity : negPolarity;
        */
    }
}
