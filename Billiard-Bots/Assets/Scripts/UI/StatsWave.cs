using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsWave : MonoBehaviour
{
    float wave = 1f;

    void Update()
    {
        wave = 1f + (0.025f * Mathf.PingPong(Time.time, 1f));
        transform.localScale = new Vector3(wave, wave, 1f);
    }
}
