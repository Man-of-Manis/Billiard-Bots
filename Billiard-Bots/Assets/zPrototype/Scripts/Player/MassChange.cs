using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MassChange : MonoBehaviour
{
    private PlayerStats stats;

    public int turnAmount = 1;
    public float newMass = 10f;

    private int startedTurns;

    // Start is called before the first frame update
    void Start()
    {
        stats = GetComponent<PlayerStats>();
        stats.currentMass = newMass;
        startedTurns = stats.completedTurns;
    }

    // Update is called once per frame
    void Update()
    {
        if (stats.completedTurns == startedTurns + turnAmount)
        {
            stats.currentMass = stats.baseMass;
            Destroy(this);
        }
    }

    public void SetAmounts(int turns, float maxPower)
    {
        turnAmount = turns;
        newMass = maxPower;
    }
}
