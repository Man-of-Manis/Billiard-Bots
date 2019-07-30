using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipperHit : MonoBehaviour
{
    [HideInInspector] public int playerNum;

    private Stats stats;

    private void Start()
    {/*
        if(PlayerTurn.Instance.PlayerIdentity[playerNum] != null)
        {
            stats = PlayerTurn.Instance.PlayerIdentity[playerNum].GetComponent<PlayerStats>().playerStatistics;
        }   */     
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Player"))
        {
            AudioManager.instance.Play("FlipperHit" + Random.Range(1, 3).ToString());

            if(transform.localEulerAngles.y > 5f || transform.localEulerAngles.y < -5f)
            {
                if(stats != null)
                {
                    stats.flipperHits++;
                }                
            }
        }
    }
}
