using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPickupUI : MonoBehaviour
{
    [SerializeField] GameObject itemUI;

    [SerializeField] private Sprite[] spriteUI = new Sprite[5];

    Dictionary<int, Color> itemColor = new Dictionary<int, Color>()
    {
        {0, Color.green},
        {1, Color.blue},
        {2, Color.grey},
        {3, Color.magenta},
        {4, Color.red}
    };

    public static PlayerPickupUI Instance
    {
        get { return s_Instance; }
    }

    protected static PlayerPickupUI s_Instance;

    void Awake()
    {
        if (s_Instance == null)
        {
            s_Instance = this;
        }

        else if (s_Instance != this)
        {
            Destroy(this.gameObject);
        }
    }

    public void PickedUp(int item)
    {
        GameObject itemPanel = Instantiate(itemUI, transform);
        Image img = itemPanel.GetComponent<Image>();
        img.sprite = spriteUI[item];
        img.color = itemColor[item];
    }
}
