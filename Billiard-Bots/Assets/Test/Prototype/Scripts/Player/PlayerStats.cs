using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int completedTurns;
    public float basePowerMultiplier;
    public float currentPowerMultiplier;
    public float baseMaxPower;
    public float currentMaxPower;
    public float baseMass = 10f;
    public float currentMass = 10f;

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        baseMass = 10f;
        currentMass = 10f;

        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(rb.mass != currentMass)
        {
            rb.mass = currentMass;
        }        
    }
}
