using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMusic : MonoBehaviour
{
    private AudioSource src;

    public AudioClip[] music;

    // Start is called before the first frame update
    void Start()
    {
        src = GetComponent<AudioSource>();

        src.clip = music[Random.Range(0, music.Length)];

        src.Play();
    }
}
