using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelect : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] private PlayerTurn turn;

    [SerializeField] private ProtoCameraController camControl;

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
        p1.SetActive(true);
        p1Health.SetActive(true);
        StartGame();
    }

    public void TwoPlayer()
    {
        p1.SetActive(true);
        p2.SetActive(true);
        p1Health.SetActive(true);
        p2Health.SetActive(true);
        StartGame();
    }

    public void ThreePlayer()
    {
        p1.SetActive(true);
        p2.SetActive(true);
        p3.SetActive(true);
        p1Health.SetActive(true);
        p2Health.SetActive(true);
        p3Health.SetActive(true);
        StartGame();
    }

    public void FourPlayer()
    {
        p1.SetActive(true);
        p2.SetActive(true);
        p3.SetActive(true);
        p4.SetActive(true);
        p1Health.SetActive(true);
        p2Health.SetActive(true);
        p3Health.SetActive(true);
        p4Health.SetActive(true);
        StartGame();
    }

    void StartGame()
    {
        turn.enabled = true;
        camControl.enabled = true;
        select.SetActive(false);
    }
}
