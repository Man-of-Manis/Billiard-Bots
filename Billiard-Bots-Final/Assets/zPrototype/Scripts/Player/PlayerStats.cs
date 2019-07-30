using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    //Values
    public int completedTurns;
    private int prevCompletedTurns;

    public float basePowerMultiplier = 500f;
    public float currentPowerMultiplier = 500f;

    public float baseMaxPower = 1f;
    public float currentMaxPower = 1f;

    public float baseMass = 10f;
    public float currentMass = 10f;

    //Items
    private float repairKitDuration = 0f;
    private float polarityReverserDuration = 0f;

    private int speedBoostTurns = 0;
    private int dietOilTurns = 0;

    private bool homingMissileActive = false;

    //Components
    private PlayerItemBar itemBar;
    private Rigidbody rb;

    public Stats playerStatistics = new Stats();

    // Start is called before the first frame update
    void Start()
    {
        baseMass = 10f;
        currentMass = 10f;

        itemBar = FindObjectOfType<PlayerUI>().PlayerBar((int)GetComponent<PlayerIdentifier>().player);

        rb = GetComponent<Rigidbody>();
        playerStatistics = new Stats();
    }

    // Update is called once per frame
    void Update()
    {
        if(rb.mass != currentMass)
        {
            rb.mass = currentMass;
        }

        ItemDuration();

        TurnCompleted();
    }

    public void UsedItem()
    {
        itemBar.RemoveItem(PlayerCollectedItem.CollecedItem.HomingBomb);
        playerStatistics.rocketsUsed++;
    }

    public void PickupItem(PlayerCollectedItem.CollecedItem item)
    {
        playerStatistics.itemsPickedup++;

        if (item.Equals(PlayerCollectedItem.CollecedItem.HomingBomb))
        {
            itemBar.AddItem(item);
        }
    }

    public void PickupItem(PlayerCollectedItem.CollecedItem item, int turns, float amount)
    {
        playerStatistics.itemsPickedup++;

        if (item.Equals(PlayerCollectedItem.CollecedItem.SpeedBoost))
        {
            if(speedBoostTurns < 1)
            {
                itemBar.AddItem(item);
                ItemTurns(PlayerCollectedItem.CollecedItem.SpeedBoost, turns, amount);
            }

            else
            {
                ItemTurns(PlayerCollectedItem.CollecedItem.SpeedBoost, turns, amount);
            }
            
        }

        else if (item.Equals(PlayerCollectedItem.CollecedItem.DietOil))
        {
            if (dietOilTurns < 1)
            {
                itemBar.AddItem(item);
                ItemTurns(PlayerCollectedItem.CollecedItem.DietOil, turns, amount);
            }

            else
            {
                ItemTurns(PlayerCollectedItem.CollecedItem.DietOil, turns, amount);
            }
        }
    }

    public void PickupItem(PlayerCollectedItem.CollecedItem item, float duration)
    {
        playerStatistics.itemsPickedup++;

        if (item.Equals(PlayerCollectedItem.CollecedItem.ReparKit))
        {
            itemBar.AddItem(item);
            repairKitDuration = duration;
        }

        else if (item.Equals(PlayerCollectedItem.CollecedItem.PolarityReverser))
        {
            itemBar.AddItem(item);
            polarityReverserDuration = duration;
        }
    }

    private void ItemTurns(PlayerCollectedItem.CollecedItem item, int turns, float amount)
    {
        if (item.Equals(PlayerCollectedItem.CollecedItem.SpeedBoost))
        {
            speedBoostTurns += turns;
            currentMaxPower = amount;
        }

        if (item.Equals(PlayerCollectedItem.CollecedItem.DietOil))
        {
            dietOilTurns += turns;
            currentMass = amount;
        }
    }

    private void ItemDuration()
    {
        if (repairKitDuration > 0f)
        {
            repairKitDuration -= Time.deltaTime;

            if(repairKitDuration <= 0f)
            {
                itemBar.RemoveItem(PlayerCollectedItem.CollecedItem.ReparKit);
            }
        }

        if (polarityReverserDuration > 0f)
        {
            polarityReverserDuration -= Time.deltaTime;

            if (polarityReverserDuration <= 0f)
            {
                itemBar.RemoveItem(PlayerCollectedItem.CollecedItem.PolarityReverser);
            }
        }
    }

    private void TurnCompleted()
    {
        if(prevCompletedTurns != completedTurns)
        {
            playerStatistics.turnsTaken++;

            if (speedBoostTurns > 0)
            {
                speedBoostTurns--;

                if(speedBoostTurns == 0)
                {
                    currentMaxPower = baseMaxPower;
                    itemBar.RemoveItem(PlayerCollectedItem.CollecedItem.SpeedBoost);
                }
            }

            if (dietOilTurns > 0)
            {
                dietOilTurns--;

                if (dietOilTurns == 0)
                {
                    currentMass = baseMass;
                    itemBar.RemoveItem(PlayerCollectedItem.CollecedItem.DietOil);
                }
            }

            prevCompletedTurns = completedTurns;
        }
    }
}
