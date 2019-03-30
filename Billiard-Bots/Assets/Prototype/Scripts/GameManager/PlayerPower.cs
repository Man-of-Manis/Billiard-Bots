using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPower : MonoBehaviour
{
    [SerializeField] private GameObject currentPlayer;

    private GameObject previousPlayer;

    public float power;

    public float minPower = 0.1f;

    public float maxPower = 1f;

    public float powerMultiplier = 1000f;

    public bool oscillatorActive;

    public bool prevOscillatorActive;

    private bool direction = true;

    public static PlayerPower Instance
    {
        get { return s_Instance; }
    }

    protected static PlayerPower s_Instance;

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


    void Update()
    {
        CurrentPlayer();
        Oscillator();
    }

    void CurrentPlayer()
    {
        previousPlayer = currentPlayer;

        currentPlayer = PlayerTurn.Instance.playerObjTurn;

        if (previousPlayer != currentPlayer)
        {
            UpdatePower();
        }
    }

    void UpdatePower()
    {
        PlayerStats stats = currentPlayer.GetComponent<PlayerStats>();
        powerMultiplier = stats.currentPowerMultiplier;
        maxPower = stats.currentMaxPower;
    }

    public void SendPower()
    {
        //current player power & power multiplier
        currentPlayer.GetComponent<PlayerController>().Launch(power, powerMultiplier);
    }

    void Oscillator()
    {
        if (oscillatorActive)
        {
            prevOscillatorActive = oscillatorActive;

            if (direction)
            {
                power += Time.deltaTime * maxPower;

                if (power >= maxPower)
                {
                    direction = false;
                }
            }

            else
            {
                power -= Time.deltaTime * maxPower;

                if (power <= minPower)
                {
                    direction = true;
                }
            }
        }

        if (prevOscillatorActive != oscillatorActive)
        {
            power = minPower;
            direction = true;
            prevOscillatorActive = oscillatorActive;
        }
    }
}
