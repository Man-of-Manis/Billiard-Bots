using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCollectedItem : MonoBehaviour
{
    public enum CollecedItem { ReparKit, SpeedBoost, DietOil, PolarityReverser, HomingBomb};

    public CollecedItem item;

    public Dictionary<CollecedItem, Color> itemColor = new Dictionary<CollecedItem, Color>()
    {
        { CollecedItem.ReparKit, Color.green},
        { CollecedItem.SpeedBoost, Color.blue},
        { CollecedItem.DietOil, Color.grey},
        { CollecedItem.PolarityReverser, Color.magenta},
        { CollecedItem.HomingBomb, Color.red}
    };

    void OnValidate()
    {
        ColorChange();
    }

    public void ItemChange(CollecedItem newItem)
    {
        item = newItem;
        ColorChange();
    }

    public void ColorChange()
    {
        GetComponent<Image>().color = itemColor[item];
    }
}
