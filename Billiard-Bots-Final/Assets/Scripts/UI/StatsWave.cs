using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsWave : MonoBehaviour
{
    public float scaleTo = 1.025f;

    public float speedMultiplier = 1f;

    float wave = 1f;

    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime * speedMultiplier;
        wave = 1f + ((scaleTo - 1) * Mathf.PingPong(timer, 1f));
        transform.localScale = new Vector3(wave, wave, 1f);
    }

    private void OnDisable()
    {
        transform.localScale = Vector3.one;
        timer = 0f;
    }
}
