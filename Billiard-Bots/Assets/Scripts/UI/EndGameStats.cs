using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class EndGameStats : MonoBehaviour
{
    public GameObject StatsMenu;

    public Transform[] PlayerStatsParent = new Transform[4];

    public TextBoxes[] PlayerStatsText = new TextBoxes[4];

    public PlayerStats[] PlayerStatistics = new PlayerStats[4];

    private void Start()
    {
        int num = PlayerTurn.Instance.PlayerIdentity.Count;

        for(int i = 0; i < num; i++)
        {
            if(PlayerTurn.Instance.PlayerIdentity[i] != null)
            {
                PlayerStatistics[i] = PlayerTurn.Instance.PlayerIdentity[i].GetComponent<PlayerStats>();
            }
            
        }

        for(int i = 0; i < PlayerStatsParent.Length; i++)
        {
            if(i < num)
            {
                PlayerStatsParent[i].transform.localPosition =
                new Vector3(200f * (num % 2 > 0 ? (i - num / 2) : (i * 2 - (num - 1)) * (num > 2 ? 1 : 1.5f)), 380f, 0f);

                Color col = PlayerTurn.Instance.PlayerIdentity[i].GetComponent<MeshRenderer>().material.color;

                PlayerStatsParent[i].GetComponent<TextMeshProUGUI>().faceColor = col; //Change Player name text color to player color

                for (int j = 0; j < PlayerStatsText[i].stats.Length; j++)
                {
                    PlayerStatsText[i].stats[j] = PlayerStatsParent[i].GetChild(j).GetChild(0).GetComponent<TextMeshProUGUI>();
                    PlayerStatsParent[i].GetChild(j).GetComponent<TextMeshProUGUI>().faceColor = col; //Change stat name text color to player color
                }
            }
            
            if(i >=  num)
            {
                PlayerStatsParent[i].gameObject.SetActive(false);
            }
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.U))
        {
            UpdateStats();
        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            StatBoardActivation();
        }
    }

    public void StatBoardActivation()
    {
        StatsMenu.SetActive(StatsMenu.activeSelf ? false : true);
    }

    public void UpdateStats()
    {
        for(int i = 0; i < PlayerStatistics.Length; i++)
        {
            

            if (PlayerStatistics[i] != null)
            {
                for (int j = 0; j < 14; j++)
                {
                    PlayerStatsText[i].stats[j].text = GetFields(i, j);
                    //PlayerStatsText[i].stats[j].faceColor = col; //Change value text color to player color
                }
            }            
        }
    }

    private string GetFields(int player, int field)
    {
        switch(field)
        {
            case 0: return PlayerStatistics[player].playerStatistics.damageDealt.ToString();
            case 1: return PlayerStatistics[player].playerStatistics.damageTaken.ToString();
            case 2: return PlayerStatistics[player].playerStatistics.damageHealed.ToString();
            case 3: return PlayerStatistics[player].playerStatistics.turnsTaken.ToString();
            case 4: return PlayerStatistics[player].playerStatistics.itemsPickedup.ToString();
            case 5: return PlayerStatistics[player].playerStatistics.rocketsUsed.ToString();
            case 6: return PlayerStatistics[player].playerStatistics.timesBlownUp.ToString();
            case 7: return PlayerStatistics[player].playerStatistics.timesSpiked.ToString();
            case 8: return PlayerStatistics[player].playerStatistics.enemiesHit.ToString();
            case 9: return PlayerStatistics[player].playerStatistics.wallBounces.ToString();
            case 10: return PlayerStatistics[player].playerStatistics.flipperHits.ToString();
            case 11: return PlayerStatistics[player].playerStatistics.timesWarped.ToString();
            case 12: return PlayerStatistics[player].playerStatistics.distanceTraveled.ToString();
            case 13: return PlayerStatistics[player].playerStatistics.topSpeed.ToString();
            default: return "0";
        }
    }
}

[System.Serializable]
public class TextBoxes
{
    public TextMeshProUGUI[] stats = new TextMeshProUGUI[14];
}
