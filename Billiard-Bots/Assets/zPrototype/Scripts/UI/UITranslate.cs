using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITranslate : MonoBehaviour
{
    private float timer = 0;
    
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime * 2f;

        transform.localPosition = new Vector3(125f, Mathf.Lerp(0f, 200f, Mathf.Sin(timer * Mathf.PI * 0.5f)), 0f);

        if(timer > 1f)
        {
            Destroy(gameObject);
        }
    }
}
