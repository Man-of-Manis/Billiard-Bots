using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSelector : MonoBehaviour
{
    public enum Item { Health, Speed, DietOil, MagSwapper, Missile, Bomb, Random };

    public Item spawnItem;

    public GameObject[] mesh = new GameObject[6];

    private SphereCollider col;

    private Transform rotator;

    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<SphereCollider>();

        rotator = transform.GetChild(0).transform;

        if(spawnItem.Equals(Item.Random))
        {
            RandomItem();
        }

        SetItem();
    }

    private void SetItem()
    {
        switch (spawnItem)
        {
            case Item.Health: Health();
                break;
            case Item.Speed: Speed();
                break;
            case Item.DietOil: DietOil();
                break;
            case Item.MagSwapper: MagSwapper();
                break;
            case Item.Missile: Missile();
                break;
            case Item.Bomb: Bomb();
                break;
        }
    }

    void RandomItem()
    {
        spawnItem = (Item)Random.Range(0, 6);
    }

    void Health()
    {
        Instantiate(mesh[0], rotator);
        gameObject.AddComponent<HealthPickup>();
    }

    void Speed()
    {
        Instantiate(mesh[1], rotator);
        gameObject.AddComponent<SpeedPickup>();
    }

    void DietOil()
    {
        Instantiate(mesh[2], rotator);
        gameObject.AddComponent<DietOilPickup>();
    }

    void MagSwapper()
    {
        Instantiate(mesh[3], rotator);
        gameObject.AddComponent<MagnetismSwitch>();
    }

    void Missile()
    {
        Instantiate(mesh[4], rotator);
        gameObject.AddComponent<MissilePickup>();
    }

    void Bomb()
    {
        col.isTrigger = false;
        Instantiate(mesh[5], rotator);
        gameObject.AddComponent<BombExplosion>();
    }
}
