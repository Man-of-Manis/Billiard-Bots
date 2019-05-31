using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSelector : MonoBehaviour
{
    public enum Item { ReparKit, SpeedBoost, DietOil, PolarityReverser, HomingBomb, Random };

    public Item activeItem;

    public GameObject[] meshObj = new GameObject[6];

    public GameObject pickupSphere;

    //public float respawnTimer = 120f;

    //private float pickupTime;

    public int respawnTurns = 3;

    private int turnTaken = 0;

    private bool itemTaken = false;

    private bool prevRandom = false;


    void OnValidate()
    {
        SetMesh();
        Name();
    }

    void Start()
    {
        RandomBool();
    }

    void Update()
    {
        Timer();
    }

    public void Taken() //I have a particular set of skills
    {
        pickupSphere.SetActive(false);
        //pickupTime = Time.realtimeSinceStartup;
        turnTaken = PlayerTurn.Instance.totalTurns;
        itemTaken = true;
    }

    private void Timer()
    {
        if (itemTaken && PlayerTurn.Instance.totalTurns >= turnTaken + respawnTurns)
        {
            SetRandomItem();
            SetMesh();
            itemTaken = false;
        }
    }

    void Name()
    {
        gameObject.name = "Interactable_" + activeItem.ToString();
    }

    public void RandomItem()
    {
        activeItem = (Item)Random.Range(0, 5);
        SetMesh();
    }

    void RandomBool()
    {
        if (activeItem.Equals(Item.Random))
        {
            prevRandom = true;
        }
    }

    void SetRandomItem()
    {
        if (prevRandom)
        {
            activeItem = Item.Random;
        }
    }

    private void SetMesh()
    {
        Transform rotator = transform.GetChild(0).transform;

        for (int i = 0; i < meshObj.Length; i++)
        {
            if (i == (int)activeItem)
            {
                meshObj[i].gameObject.SetActive(true);
            }

            else
            {
                meshObj[i].gameObject.SetActive(false);
            }
        }

        pickupSphere.SetActive(true);
    }
}
