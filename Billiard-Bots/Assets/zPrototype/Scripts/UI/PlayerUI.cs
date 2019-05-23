using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private TMP_Text[] playerHp = new TMP_Text[4];

    [SerializeField] private Image[] playerHpBar = new Image[4];

    [Header("ItemUI")]
    [SerializeField] private PlayerItemBar[] playerBar = new PlayerItemBar[4];


    public void UpdatePlayerHealth(int playerNum, int newHealth, int maxHealth)
    {
        playerHp[playerNum].text = newHealth.ToString() + " / " + maxHealth.ToString();
        playerHpBar[playerNum].fillAmount = (float)newHealth / maxHealth;
    }

    public PlayerItemBar PlayerBar(int playerNum)
    {
        return playerBar[playerNum];
    }
}
