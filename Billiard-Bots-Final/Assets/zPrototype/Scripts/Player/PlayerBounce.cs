using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBounce : MonoBehaviour
{
    public bool gBItem = false;

    public GameObject[] itemPickup = new GameObject[3];

    private GBItemWheel wheel;

    private Stats ps;

    void Start()
    {
        wheel = FindObjectOfType<GBItemWheel>();
        ps = GetComponent<PlayerStats>().playerStatistics;
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
                ps.wallBounces++;
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
                PlayerWallHit.Instance.Hit();
                wheel.RotateWheel();
                AudioManager.instance.Play("WallHit");
            }

            if (other.collider.CompareTag("Wall"))
            {
                AudioManager.instance.Play("WallHit");
                ps.wallBounces++;
            }
        }        
    }
}
