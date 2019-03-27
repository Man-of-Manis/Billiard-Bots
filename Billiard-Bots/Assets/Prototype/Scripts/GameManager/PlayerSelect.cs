using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelect : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] private PlayerTurn turn;

    [SerializeField] private ProtoCameraController camControl;

    [SerializeField] private PlayerButtonsUI buttonUI;

    [Header("UI")]
    [SerializeField] private GameObject select;


    [Header("Players")]
    [SerializeField] private GameObject p1;

    [SerializeField] private GameObject p2;

    [SerializeField] private GameObject p3;

    [SerializeField] private GameObject p4;

    [Header("Spawns")]
    [SerializeField] private Transform p1Spawn;

    [SerializeField] private Transform p2Spawn;

    [SerializeField] private Transform p3Spawn;

    [SerializeField] private Transform p4Spawn;

    [Header("Health")]
    [SerializeField] private GameObject p1Health;

    [SerializeField] private GameObject p2Health;

    [SerializeField] private GameObject p3Health;

    [SerializeField] private GameObject p4Health;


    public void OnePlayer()
    {
        SetPlayers(false, false, false);
    }

    public void TwoPlayer()
    {
        SetPlayers(true, false, false);
    }

    public void ThreePlayer()
    {
        SetPlayers(true, true, false);
    }

    public void FourPlayer()
    {
        SetPlayers(true, true, true);
    }

    void SetPlayers(bool player2, bool player3, bool player4)
    {
        p1.SetActive(true);
        p2.SetActive(player2);
        p3.SetActive(player3);
        p4.SetActive(player4);
        p1Health.SetActive(true);
        p2Health.SetActive(player2);
        p3Health.SetActive(player3);
        p4Health.SetActive(player4);
        StartGame();
    }

    void StartGame()
    {
        turn.enabled = true;
        camControl.enabled = true;
        buttonUI.enabled = true;
        select.SetActive(false);
    }
}
