using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITranslate : MonoBehaviour
{
    [HideInInspector] public float timer = 0;

    public bool randomX = false;

    private float posX = 125f;

    public Vector2 offset = Vector2.zero;

    public float startPos = 0f;

    public float endPos = 200f;

    public float destroyTimer = 1.25f;

    private void Start()
    {
        posX = randomX ? Random.Range(-75f, 75f) : 125f;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime * 2f;

        transform.localPosition = new Vector3(posX + offset.x, Mathf.Lerp(startPos + offset.y, endPos + offset.y, Mathf.Sin(timer * Mathf.PI * 0.5f)), 0f);

        if(timer > destroyTimer)
        {
            Destroy(gameObject);
        }
    }
}
