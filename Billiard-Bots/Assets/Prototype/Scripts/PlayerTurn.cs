using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurn : MonoBehaviour
{
    public List<GameObject> players;

    public GameObject playerObjTurn;

    public int playerNumTurn = 0;

    public int playerAmount = 0;

    // Start is called before the first frame update
    void Start()
    {
        GameObject[] p = GameObject.FindGameObjectsWithTag("Player");

        for (int i = 0; i < p.Length; i++)
        {    
            for(int j = 0; j < p.Length; j++)
            {
                if(p[j].name.Equals("Player" + (i + 1).ToString()))
                {
                    players.Insert(i, p[j]);
                }
            }
        }

        playerAmount = players.Count;

        playerObjTurn = players[playerNumTurn];

        playerObjTurn.GetComponent<PlayerController>().turnEnabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EndTurn(string name)
    {
        if(name.Equals(playerObjTurn.name))
        {
            playerObjTurn.GetComponent<PlayerController>().turnEnabled = false;
            playerObjTurn = players[playerNumTurn + 1 <= playerAmount ? playerNumTurn + 1 : 0];
            NextTurn();
        }
    }

    private void NextTurn()
    {
        //Enable next player
        playerObjTurn.GetComponent<PlayerController>().turnEnabled = true;
    }
}
