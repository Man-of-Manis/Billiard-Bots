using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBounce : MonoBehaviour
{
    public int bounces = 0;

    public int turns = 0;

    public bool gBItem = false;

    public GameObject[] itemPickup = new GameObject[3];

    private GBItemWheel wheel;

    // Start is called before the first frame update
    void Start()
    {
        wheel = FindObjectOfType<GBItemWheel>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.collider.name != "Table" && !other.gameObject.Equals(gameObject) 
            && !other.collider.gameObject.Equals(PlayerTurn.Instance.playerObjTurn)
            && gameObject.Equals(PlayerTurn.Instance.playerObjTurn))
        {
            if (other.collider.CompareTag("GBMachine"))
            {
                gBItem = true;
            }

            if (other.collider.CompareTag("Player") && gBItem)
            {
                GameObject pickup = Instantiate(itemPickup[(int)wheel.currentItem], transform.position, Quaternion.identity);
                pickup.SendMessage("SetPlayer", GetComponent<PlayerIdentifier>().player.ToString());
                Debug.Log(GetComponent<PlayerIdentifier>().player.ToString() + " has gotten " + wheel.currentItem.ToString());
                gBItem = false;
            }

            if (other.collider.CompareTag("Wall") && gBItem)
            {
                //bounces++;
                PlayerWallHit.Instance.Hit();
                Debug.Log(other.gameObject.name);
                wheel.RotateWheel();
            }
        }        
    }
}
