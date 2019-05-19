using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlusOne : MonoBehaviour
{
    private TextMeshProUGUI text;

    private RectTransform trans;

    private float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        trans = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime * 2f;

        trans.localPosition = new Vector3(125f, Mathf.Lerp(0f, 200f, Mathf.Sin(timer * Mathf.PI * 0.5f)), 0f);

        if(timer > 1f)
        {
            Destroy(gameObject);
        }
    }
}
