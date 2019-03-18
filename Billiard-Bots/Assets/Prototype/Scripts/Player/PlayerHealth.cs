using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int CurrentHealth = 10;

    public int MaxHealth = 10;

    private PlayerUI UI;

    void Start()
    {
        UI = FindObjectOfType<PlayerUI>();
        UI.UpdatePlayerHealth(gameObject.name[gameObject.name.Length - 1].ToString(), CurrentHealth, MaxHealth);
    }


    public void AddHealth(int amount)
    {
        CurrentHealth = CurrentHealth + amount <= MaxHealth ? CurrentHealth + amount : MaxHealth;

        UpdateHealth();
    }

    public void SubHealth(int amount)
    {
        CurrentHealth = CurrentHealth - amount >= 0 ? CurrentHealth - amount : 0;

        if(CurrentHealth == 0)
        {
            Debug.Log(gameObject.name + "'s health has been reduced to 0!");
            
            gameObject.GetComponent<Rigidbody>().isKinematic = true;
            gameObject.GetComponent<SphereCollider>().isTrigger = true;
            PlayerTurn.Instance.PlayerDestroyed(gameObject);
            
        }

        UpdateHealth();
    }

    void UpdateHealth()
    {
        UI.UpdatePlayerHealth(gameObject.name[gameObject.name.Length - 1].ToString(), CurrentHealth, MaxHealth);
    }
}
