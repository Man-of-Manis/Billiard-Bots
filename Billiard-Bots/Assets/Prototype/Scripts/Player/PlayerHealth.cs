using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int CurrentHealth = 20;

    public int MaxHealth = 20;

    private PlayerUI UI;

    void Start()
    {
        UI = FindObjectOfType<PlayerUI>();
        UI.UpdatePlayerHealth((int)gameObject.GetComponent<PlayerIdentifier>().player + 1, CurrentHealth, MaxHealth);
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
            Debug.Log(gameObject.name + "'s health has been reduced to 0!");
            
            gameObject.GetComponent<Rigidbody>().isKinematic = true;
            gameObject.GetComponent<SphereCollider>().isTrigger = true;
            PlayerTurn.Instance.PlayerDestroyed(gameObject);
            
        }

        UpdateHealth();
    }

    void UpdateHealth()
    {
        UI.UpdatePlayerHealth((int)gameObject.GetComponent<PlayerIdentifier>().player + 1, CurrentHealth, MaxHealth);
    }
}
