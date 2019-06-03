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
        //CurrentHealth = CurrentHealth + amount <= MaxHealth ? CurrentHealth + amount : MaxHealth;

        int healAmount = CurrentHealth + amount <= MaxHealth ? amount : CurrentHealth + amount - MaxHealth;

        CurrentHealth += healAmount;

        GetComponent<PlayerStats>().playerStatistics.damageHealed += healAmount;

        GetComponentInChildren<HealthChangeIndicator>().healthChange(healAmount);

        UpdateHealth();
    }

    public void SubHealth(int amount)
    {
        CurrentHealth = CurrentHealth - amount >= 0 ? CurrentHealth - amount : 0;

        GetComponentInChildren<HealthChangeIndicator>().healthChange(-amount);

        if (CurrentHealth == 0)
        {
            Death();            
        }

        UpdateHealthDamaged();
    }

    public void SetHealth()
    {
        UpdateHealth();
    }

    void UpdateHealth()
    {
        UI.UpdatePlayerHealth((int)gameObject.GetComponent<PlayerIdentifier>().player, CurrentHealth, MaxHealth);
    }

    void UpdateHealthDamaged()
    {
        UI.UpdatePlayerHealth((int)gameObject.GetComponent<PlayerIdentifier>().player, CurrentHealth, MaxHealth, true);
    }

    void Death()
    {
        Debug.Log(gameObject.name + "'s health has been reduced to 0!");

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

        GetComponent<AudioSource>().Stop();
        AudioManager.instance.Play("PlayerDeath");
        Instantiate(deathExplosion, transform.position, Quaternion.identity);
    }
}
