using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemBar : MonoBehaviour
{
    [SerializeField] private RectTransform[] items = new RectTransform[5];

    [Range(0, 5)]
    [SerializeField] private int activeItems;

    void OnValidate()
    {
        AmountChange();
    }

    private void AmountChange() //Change amount of itemcards visible
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (i < activeItems)
            {
                items[i].localPosition = Vector3.right * 30f * (activeItems % 2 > 0 ?((i - activeItems / 2) * 2) : (i * 2 - (activeItems - 1)));

                if (!items[i].gameObject.activeSelf)
                {
                    items[i].gameObject.SetActive(true);
                }
            }

            else
            {
                items[i].gameObject.SetActive(false);
            }
        }
    }

    public void RemoveItem(PlayerCollectedItem.CollecedItem item) //Removes item from cards
    {
        int itemNum = FindItem(item);

        ReplaceItem(itemNum, item);
    }

    private int FindItem(PlayerCollectedItem.CollecedItem item) //Finds position of active item card
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i].gameObject.activeSelf)
            {
                if (items[i].GetComponent<PlayerCollectedItem>().item.Equals(item))
                {
                    return i;
                }
            }
        }

        return items.Length - 1;
    }

    private void ReplaceItem(int num, PlayerCollectedItem.CollecedItem item) //changes item card at a specified index
    {
        PlayerCollectedItem.CollecedItem tempHolder = item;

        for(int i = num; i < items.Length; i++)
        {
            if(i + 1 < items.Length)
            {
                items[i].GetComponent<PlayerCollectedItem>().ItemChange(items[i+1].GetComponent<PlayerCollectedItem>().item);
            }

            else
            {
                activeItems--;
                AmountChange();
            }
        }
    }

    public void AddItem(PlayerCollectedItem.CollecedItem item) //adds an item card at the end of the active cards
    {
        GameObject itemCard = GetActiveItems(item);

        if (itemCard != null)
        {
            itemCard.GetComponent<PlayerCollectedItem>().ItemChange(item);
            activeItems++;
            AmountChange();
        }
    }

    private GameObject GetActiveItems(PlayerCollectedItem.CollecedItem item) //Gets currently active item cards and returns the first inactive
    {
        for(int i = 0; i < items.Length; i++)
        {
            if(items[i].gameObject.activeSelf)
            {
                if(items[i].GetComponent<PlayerCollectedItem>().item.Equals(item))
                {
                    return null;
                }
            }

            else
            {
                return items[i].gameObject;
            }
        }

        return null;
    }
}
