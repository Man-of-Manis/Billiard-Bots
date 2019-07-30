using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GBItem : MonoBehaviour
{
    public enum GBItemType {Health, SpeedBoost, HomingBomb };

    public GBItemType item;

    private TextMeshProUGUI text;

    [SerializeField]private Sprite[] spriteUI = new Sprite[3];

    
    public Dictionary<GBItemType, Vector3> itemColor = new Dictionary<GBItemType, Vector3>()
    {
        { GBItemType.Health, Vector3.up},
        { GBItemType.SpeedBoost, Vector3.forward},
        { GBItemType.HomingBomb, Vector3.right}
    };
    

    private void OnValidate()
    {
        ColorChange();
        TextChange();
    }

    // Start is called before the first frame update
    void Start()
    {
        ColorChange();
        TextChange();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ColorChange()
    {
        GetComponent<Image>().color = new Color(itemColor[item].x, itemColor[item].y, itemColor[item].z, 1f);
        GetComponent<Image>().sprite = spriteUI[(int)item];
    }

    public void OpacityChange(float opac)
    {
        Color current = GetComponent<Image>().color;
        GetComponent<Image>().color = new Color(current.r, current.g, current.b, opac);
    }

    public void TextChange()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();

        text.text = item.ToString();
    }
}
