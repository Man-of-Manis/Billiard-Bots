using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallHit : MonoBehaviour
{
    public GameObject text;

    public static PlayerWallHit Instance
    {
        get { return s_Instance; }
    }

    protected static PlayerWallHit s_Instance;

    void Awake()
    {
        if (s_Instance == null)
        {
            s_Instance = this;
        }

        else if (s_Instance != this)
        {
            Destroy(this.gameObject);
        }
    }

    public void Hit()
    {
        Instantiate(text, transform);
    }
}
