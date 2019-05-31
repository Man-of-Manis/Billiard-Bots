using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTransition : MonoBehaviour
{
    public Material TransitionMaterial;

    public float speed;

    private void Start()
    {
        TransitionMaterial.SetFloat("_Cutoff", 1f);

        StartCoroutine(TransitionTo(0f));
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            StartCoroutine(TransitionTo(1f));
        }

        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            StartCoroutine(TransitionTo(0f));
        }
    }

    IEnumerator TransitionTo(float cutoff)
    {
        float current = TransitionMaterial.GetFloat("_Cutoff");

        float timer = 0;

        while(timer <= speed)
        {
            TransitionMaterial.SetFloat("_Cutoff", Mathf.Lerp(current, cutoff, ((timer += Time.deltaTime) / speed)));
            yield return null;
        }
    }
}
