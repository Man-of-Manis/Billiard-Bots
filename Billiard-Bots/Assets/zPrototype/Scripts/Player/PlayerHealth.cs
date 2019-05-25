using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int CurrentHealth = 20;

    public int MaxHealth = 20;

    private PlayerUI UI;

    public GameObject deathExplosion;

    void Start()
    {
        UI = FindObjectOfType<PlayerUI>();
        UI.UpdatePlayerHealth((int)gameObject.GetComponent<PlayerIdentifier>().player, CurrentHealth, MaxHealth);
        SetUIColor();
    }

    private void SetUIColor()
    {
        UI.UIColor((int)gameObject.GetComponent<PlayerIdentifier>().player, GetComponent<MeshRenderer>().material.color);
    }

    public void AddHealth(int amount)
    {
        CurrentHealth = CurrentHealth + amount <= MaxHealth ? CurrentHealth + amount : MaxHealth;

        UpdateHealth();
    }

    public void SubHealth(int amount)
    {
        CurrentHealth = CurrentHealth - amount >= 0 ? CurrentHealth - amount : 0;

        if (CurrentHealth == 0)
        {
            

            Death();            
        }

        UpdateHealth();
    }

    public void SetHealth()
    {
        UpdateHealth();
    }

    void UpdateHealth()
    {
        UI.UpdatePlayerHealth((int)gameObject.GetComponent<PlayerIdentifier>().player, CurrentHealth, MaxHealth);
    }

    void Death()
    {
        Debug.Log(gameObject.name + "'s health has been reduced to 0!");

        
        //gameObject.GetComponent<SphereCollider>().isTrigger = true;
        PlayerTurn.Instance.PlayerDestroyed(gameObject);

        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
        rb.isKinematic = true;
        rb.velocity = Vector3.zero;

        gameObject.GetComponent<SphereCollider>().enabled = false;

        Component[] meshes = gameObject.GetComponentsInChildren<MeshRenderer>();

        foreach(MeshRenderer mesh in meshes)
        {
            mesh.enabled = false;
        }
        //gameObject.GetComponent<MeshRenderer>().enabled = false;

        Instantiate(deathExplosion);
    }
}
