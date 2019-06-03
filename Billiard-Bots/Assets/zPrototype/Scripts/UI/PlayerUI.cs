using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    [Header("PlayerUIBar")]
    [SerializeField] private GameObject[] playerUIBar = new GameObject[4];

    [Header("BGColor")]
    [SerializeField] private Image[] playerBGColor = new Image[4];

    [Header("Health")]
    [SerializeField] private TMP_Text[] playerHp = new TMP_Text[4];

    [SerializeField] private Image[] playerHpBar = new Image[4];

    [Header("ItemUI")]
    [SerializeField] private PlayerItemBar[] playerBar = new PlayerItemBar[4];

    [Header("Joystick")]
    [SerializeField] private GameObject joystick;


    public void PlayerTurn(int playerNum)
    {
        for (int i = 0; i < playerUIBar.Length; i++)
        {
            if (playerUIBar[i].activeSelf)
            {
                playerUIBar[i].GetComponent<StatsWave>().enabled = (i == playerNum);
            }
        }
    }

    public void UpdatePlayerHealth(int playerNum, int newHealth, int maxHealth)
    {
        playerHp[playerNum].text = newHealth.ToString() + " / " + maxHealth.ToString();
        playerHpBar[playerNum].fillAmount = (float)newHealth / maxHealth;
        StartCoroutine(UIShaker(playerNum));
    }

    public PlayerItemBar PlayerBar(int playerNum)
    {
        return playerBar[playerNum];
    }

    public void UIColor(int player, Color color)
    {
        playerBGColor[player].color = new Color(color.r, color.g, color.b, 0.5f);
    }

    public void JoystickAnim(bool value)
    {
        joystick.SetActive(value);
    }


    IEnumerator UIShaker(int playerNum)
    {
        float playerX = playerUIBar[playerNum].transform.localPosition.x;

        float shakeAmount = playerX < 0f ? 75f : -75f;
        float timer = 0f;        

        while (timer <= 1f)
        {
            float shake = playerX + (shakeAmount * (Mathf.PingPong(timer * 20f, 1f)));

            playerUIBar[playerNum].transform.localPosition = new Vector3(shake, playerUIBar[playerNum].transform.localPosition.y, playerUIBar[playerNum].transform.localPosition.z);
            timer += Time.deltaTime / 1f;
            shakeAmount -= shakeAmount * timer;
            yield return null;
        }
    }
}
