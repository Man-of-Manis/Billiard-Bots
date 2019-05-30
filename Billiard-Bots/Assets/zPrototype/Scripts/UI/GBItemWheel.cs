﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GBItemWheel : MonoBehaviour
{
    [Range(1, 11)]
    [SerializeField] private int itemNum = 6;

    [Range(300f, 500f)]
    public float offset = 375f;

    public float wheelRot = 0f;

    private float newRot = 0f;

    public GameObject itemBox;

    public List<RectTransform> itemBoxes = new List<RectTransform>();

    private RectTransform wheel;

    private float timer = 0f;

    private float vel = 0f;

    public GBItem.GBItemType currentItem;

    float lowest = 0f;

    GameObject item = null;

    private void OnValidate()
    {
        Position();
        Rotation();
        ItemSelection();
        ItemSetting();
        ItemOpacity();
        
    }

    // Start is called before the first frame update
    void Start()
    {
        Position();
        Rotation();
        ItemSelection();
        ItemSetting();
    }

    

    // Update is called once per frame
    void Update()
    {
        Rotation();
        ItemOpacity();
        Rotating();
    }


    private void Position()
    {
        if (itemBox != null)
        {
            wheel = GetComponent<RectTransform>();

            float angle = 360f / itemNum;

            int children = wheel.gameObject.transform.childCount;

            for (int i = 0; i < children; i++)
            {
                StartCoroutine(DestroyItems(wheel.GetChild(i).gameObject));
            }

            itemBoxes.Clear();

            for (int i = 0; i < itemNum; i++)
            {
                float x = (offset / 2) * Mathf.Cos((270f - (angle * i)) * Mathf.Deg2Rad);
                float y = (offset / 2) * Mathf.Sin((270f - (angle * i)) * Mathf.Deg2Rad);

                RectTransform rec = Instantiate(itemBox, Vector3.zero, Quaternion.identity, transform).GetComponent<RectTransform>();
                itemBoxes.Insert(i, rec);

                rec.localPosition = new Vector3(x, y, 0f);
            }
        }
    }

    private void Rotation()
    {
        wheel = GetComponent<RectTransform>();

        float parRot = wheelRot;

        //RectTransform[] items = transform.GetComponentsInChildren<RectTransform>();

        RectTransform[] items = new RectTransform[transform.childCount];

        for(int i =0; i < items.Length; i++)
        {
            items[i] = transform.GetChild(i).GetComponent<RectTransform>();
        }

        foreach (RectTransform item in items)
        {
            item.localEulerAngles = new Vector3(0f, 0f, parRot);
        }

        //wheel.localEulerAngles = new Vector3(0f, 0f, -parRot);
        wheel.localRotation = Quaternion.Euler(new Vector3(0f, 0f, -parRot));
    }

    IEnumerator DestroyItems(GameObject go)
    {
        yield return null;
        DestroyImmediate(go);
    }

    private void ItemSelection()
    {
        for(int i = 0; i < itemBoxes.Count; i++)
        {
            GBItem itemInst = itemBoxes[i].GetComponent<GBItem>();
            itemInst.item = (GBItem.GBItemType)(i % System.Enum.GetValues(typeof(GBItem.GBItemType)).Length);
            itemInst.ColorChange();
        }
    }

    private void ItemOpacity()
    {      

        for (int i = 0; i < itemBoxes.Count; i++)
        {
            Vector3 worPos = itemBoxes[i].GetComponent<RectTransform>().position;
            Vector3 locVec = wheel.parent.transform.InverseTransformPoint(worPos);

            //Debug.Log(wheel.position + " / " + wheel.parent.transform.InverseTransformPoint(worPos));

            float opac = (locVec.y) / (-offset/2);
            Mathf.Clamp01(opac);
            float expo = opac * opac;
            itemBoxes[i].GetComponent<GBItem>().OpacityChange(expo);

            if(locVec.y < lowest)
            {
                lowest = itemBoxes[i].GetComponent<RectTransform>().localPosition.y;
                item = itemBoxes[i].gameObject;
            }
        }

        currentItem = item.GetComponent<GBItem>().item;
    }

    public void RotateWheel()
    {
        newRot += (360f / itemNum);
    }

    private void Rotating()
    {
        wheelRot = Mathf.SmoothDamp(wheelRot, newRot, ref vel, 0.3f);
    }

    private void ItemSetting()
    {
        Vector3 worPos = itemBoxes[0].GetComponent<RectTransform>().position;
        Vector3 locVec = wheel.parent.transform.InverseTransformPoint(worPos);
        lowest = locVec.y;

        item = itemBoxes[0].gameObject;
    }
}
