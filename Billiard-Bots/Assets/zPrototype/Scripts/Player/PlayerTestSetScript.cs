using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTestSetScript : MonoBehaviour
{
    public int playerHealth;

    // Start is called before the first frame update
    void Start()
    {
        PlayerHealth[] players = FindObjectsOfType<PlayerHealth>();

        foreach(PlayerHealth p in players)
        {
            p.CurrentHealth = playerHealth;
            p.MaxHealth = playerHealth;

            p.SetHealth();
        }
    }

}
