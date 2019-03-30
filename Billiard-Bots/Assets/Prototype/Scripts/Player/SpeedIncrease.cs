using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedIncrease : MonoBehaviour
{
    private PlayerStats stats;

    public int turnAmount = 1;
    public float maxPowerIncrease = 1.5f;

    private int startedTurns;

    // Start is called before the first frame update
    void Start()
    {
        stats = GetComponent<PlayerStats>();
        stats.currentMaxPower = maxPowerIncrease;
        startedTurns = stats.completedTurns;
    }

    // Update is called once per frame
    void Update()
    {
        if(stats.completedTurns == startedTurns + turnAmount)
        {
            stats.currentMaxPower = stats.baseMaxPower;
            Destroy(this);
        }
    }

    public void SetAmounts(int turns, float maxPower)
    {
        turnAmount = turns;
        maxPowerIncrease = maxPower;
    }
}
