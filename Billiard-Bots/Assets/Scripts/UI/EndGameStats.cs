using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class EndGameStats : MonoBehaviour
{
    [HideInInspector] public GameObject winner;

    public TextMeshProUGUI winnerName;

    public Transform winnerPosition;

    public GameObject winnerBG;

    public GameObject StatsMenu;

    public Transform[] PlayerStatsParent = new Transform[4];

    public TextBoxes[] PlayerStatsText = new TextBoxes[4];

    public PlayerStats[] PlayerStatistics = new PlayerStats[4];

    public Transform bestStatsParent;

    //public Stats bestStats = new Stats();

    private int[] bestStatsPlayer = new int[14];

    public TextBoxes[] bestText = new TextBoxes[2];

    private int numPlayers;

    private EndGameMenu menu;

    private void Start()
    {
        menu = GetComponent<EndGameMenu>();

        numPlayers = PlayerTurn.Instance.PlayerIdentity.Count;

        for(int i = 0; i < numPlayers; i++)
        {
            if(PlayerTurn.Instance.PlayerIdentity[i] != null)
            {
                PlayerStatistics[i] = PlayerTurn.Instance.PlayerIdentity[i].GetComponent<PlayerStats>();
            }
        }

        for(int i = 0; i < PlayerStatsParent.Length; i++)
        {
            if(i < numPlayers)
            {
                PlayerStatsParent[i].transform.localPosition =
                new Vector3(200f * (numPlayers % 2 > 0 ? (i - numPlayers / 2) : (i * 2 - (numPlayers - 1)) * (numPlayers > 2 ? 1 : 1.5f)), 380f, 0f);

                Color col = PlayerTurn.Instance.PlayerIdentity[i].GetComponent<MeshRenderer>().material.color;

                PlayerStatsParent[i].GetComponent<TextMeshProUGUI>().faceColor = col; //Change Player name text color to player color

                for (int j = 0; j < PlayerStatsText[i].stats.Length; j++)
                {
                    PlayerStatsText[i].stats[j] = PlayerStatsParent[i].GetChild(j).GetChild(0).GetComponent<TextMeshProUGUI>();
                    PlayerStatsParent[i].GetChild(j).GetComponent<TextMeshProUGUI>().faceColor = col; //Change stat name text color to player color
                }
            }
            
            if(i >=  numPlayers)
            {
                PlayerStatsParent[i].gameObject.SetActive(false);
            }
        }

        GetBestStats();
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

    public void EndGame()
    {
        if(winner != null)
        {
            winner.transform.position = winnerPosition.position;

            winner.AddComponent<Rotator>();
        }

        else
        {
            winnerBG.SetActive(false);
        }

        UpdateStats();
        StatBoardActivation();
        menu.enabled = true;
    }

    public void StatBoardActivation()
    {
        StatsMenu.SetActive(StatsMenu.activeSelf ? false : true);
    }

    public void UpdateStats()
    {
        GetBaseStats();
        GetBestStats();
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
            case 12: return (Mathf.RoundToInt((PlayerStatistics[player].playerStatistics.distanceTraveled) * 100f) / 100f).ToString() + " m";
            case 13: return (Mathf.RoundToInt((PlayerStatistics[player].playerStatistics.topSpeed) * 100f) / 100f).ToString() + " m/s";
            default: return "0";
        }
    }

    private string GetBestFields(int i, int field)
    {
        switch (field)
        {
            case 0: return PlayerStatistics[i].playerStatistics.damageDealt.ToString();
            case 1: return PlayerStatistics[i].playerStatistics.damageTaken.ToString();
            case 2: return PlayerStatistics[i].playerStatistics.damageHealed.ToString();
            case 3: return PlayerStatistics[i].playerStatistics.turnsTaken.ToString();
            case 4: return PlayerStatistics[i].playerStatistics.itemsPickedup.ToString();
            case 5: return PlayerStatistics[i].playerStatistics.rocketsUsed.ToString();
            case 6: return PlayerStatistics[i].playerStatistics.timesBlownUp.ToString();
            case 7: return PlayerStatistics[i].playerStatistics.timesSpiked.ToString();
            case 8: return PlayerStatistics[i].playerStatistics.enemiesHit.ToString();
            case 9: return PlayerStatistics[i].playerStatistics.wallBounces.ToString();
            case 10: return PlayerStatistics[i].playerStatistics.flipperHits.ToString();
            case 11: return PlayerStatistics[i].playerStatistics.timesWarped.ToString();
            case 12: return (Mathf.RoundToInt((PlayerStatistics[i].playerStatistics.distanceTraveled) * 100f) / 100f).ToString() + " m";
            case 13: return (Mathf.RoundToInt((PlayerStatistics[i].playerStatistics.topSpeed) * 100f) / 100f).ToString() + " m/s";
            default: return "0";
        }
    }

    private void GetBaseStats()
    {
        for (int i = 0; i < PlayerStatistics.Length; i++)
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

    private void GetBestStats()
    {
        for(int i = 0; i < numPlayers; i++)
        {
            bestStatsPlayer[0] = (bestStatsPlayer[0] >= PlayerStatistics[i].playerStatistics.damageDealt ? bestStatsPlayer[0] : i);
            bestStatsPlayer[1] = (bestStatsPlayer[1] >= PlayerStatistics[i].playerStatistics.damageTaken ? bestStatsPlayer[1] : i);
            bestStatsPlayer[2] = (bestStatsPlayer[2] >= PlayerStatistics[i].playerStatistics.damageHealed ? bestStatsPlayer[2] : i);
            bestStatsPlayer[3] = (bestStatsPlayer[3] >= PlayerStatistics[i].playerStatistics.turnsTaken ? bestStatsPlayer[3] : i);
            bestStatsPlayer[4] = (bestStatsPlayer[4] >= PlayerStatistics[i].playerStatistics.itemsPickedup ? bestStatsPlayer[4] : i);
            bestStatsPlayer[5] = (bestStatsPlayer[5] >= PlayerStatistics[i].playerStatistics.rocketsUsed ? bestStatsPlayer[5] : i);
            bestStatsPlayer[6] = (bestStatsPlayer[6] >= PlayerStatistics[i].playerStatistics.timesBlownUp ? bestStatsPlayer[6] : i);
            bestStatsPlayer[7] = (bestStatsPlayer[7] >= PlayerStatistics[i].playerStatistics.timesSpiked ? bestStatsPlayer[7] : i);
            bestStatsPlayer[8] = (bestStatsPlayer[8] >= PlayerStatistics[i].playerStatistics.enemiesHit ? bestStatsPlayer[8] : i);
            bestStatsPlayer[9] = (bestStatsPlayer[9] >= PlayerStatistics[i].playerStatistics.wallBounces ? bestStatsPlayer[9] : i);
            bestStatsPlayer[10] = (bestStatsPlayer[10] >= PlayerStatistics[i].playerStatistics.flipperHits ? bestStatsPlayer[10] : i);
            bestStatsPlayer[11] = (bestStatsPlayer[11] >= PlayerStatistics[i].playerStatistics.timesWarped ? bestStatsPlayer[11] : i);
            bestStatsPlayer[12] = (bestStatsPlayer[12] >= PlayerStatistics[i].playerStatistics.distanceTraveled ? bestStatsPlayer[12] : i);
            bestStatsPlayer[13] = (bestStatsPlayer[13] >= PlayerStatistics[i].playerStatistics.topSpeed ? bestStatsPlayer[13] : i);
        }

        for (int i = 0; i < bestStatsPlayer.Length; i++)
        {
            for(int j = 0; j < 2; j++)
            {
                bestText[j].stats[i] = bestStatsParent.GetChild(i).GetChild(j).GetComponent<TextMeshProUGUI>();
            }            

            bestText[0].stats[i].text = GetBestFields(bestStatsPlayer[i], i);
            bestText[1].stats[i].text = "Player " + (bestStatsPlayer[i] + 1);
            bestText[1].stats[i].faceColor = PlayerTurn.Instance.PlayerIdentity[bestStatsPlayer[i]].GetComponent<MeshRenderer>().material.color;
        }
    }
}

[System.Serializable]
public class TextBoxes
{
    public TextMeshProUGUI[] stats = new TextMeshProUGUI[14];
}
