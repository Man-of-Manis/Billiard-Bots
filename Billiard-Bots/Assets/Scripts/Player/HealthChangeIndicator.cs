using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthChangeIndicator : MonoBehaviour {
    
    public Text text;
    private Vector2 baseTextOffset;
    public float displacement = 5f;
    public float time = 0.5f;

    void Start ()
    {
        baseTextOffset = new Vector2(text.rectTransform.localPosition.x, text.rectTransform.localPosition.y);
    }

    void Update()
    {
        
        transform.LookAt(Camera.main.transform);
        transform.Rotate(Vector3.up, 180);

    }

    public void healthChange(float difference)
    {
        if (text.IsActive())
        {
            textPopup(difference).Reset();
        }

        else
        {
            StartCoroutine(textPopup(difference));
        }
        
    }

    IEnumerator textPopup(float difference) {

        text.gameObject.SetActive(true);
        
        float x = Random.Range(baseTextOffset.x - displacement, baseTextOffset.x + displacement);
        float y = Random.Range(baseTextOffset.y - displacement, baseTextOffset.y + displacement);

        text.rectTransform.localPosition = new Vector3(x, y, text.rectTransform.localPosition.z);

        string mod;

        if (difference > 0)
        {
            text.color = Color.green;
            mod = "+";
        }

        else
        {
            text.color = Color.red;
            mod = "-";
        }
        
        text.text = mod + Mathf.Abs(difference);

        InvokeRepeating("raiseText", 0f, 0.1f);

        yield return new WaitForSeconds(time);

        text.gameObject.SetActive(false);

    }

    private void raiseText()
    {
        text.rectTransform.localPosition = new Vector3(text.rectTransform.localPosition.x, text.rectTransform.localPosition.y + 2, text.rectTransform.localPosition.z);
    }

}
