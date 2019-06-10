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

    [SerializeField] private Image[] playerDamaged = new Image[4];

    [SerializeField] private GameObject DamagedText;

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
    }

    public void UpdatePlayerHealth(int playerNum, int newHealth, int maxHealth, int amount)
    {
        playerHp[playerNum].text = newHealth.ToString() + " / " + maxHealth.ToString();
        playerHpBar[playerNum].fillAmount = (float)newHealth / maxHealth;

        SpawnHealthText(playerNum, amount, amount < 0 ? Color.red : Color.green);

        if (newHealth == 0)
        {
            StartCoroutine(DamageFlash(playerNum));
            StartCoroutine(UIDestroyer(playerNum));
        }

        else
        {
            StartCoroutine(DamageFlash(playerNum));
            StartCoroutine(UIShaker(playerNum));
        }
    }

    private void SpawnHealthText(int playerNum, int amount, Color col)
    {
        GameObject dt = Instantiate(DamagedText, playerUIBar[playerNum].transform.position, playerUIBar[playerNum].transform.rotation, playerUIBar[playerNum].transform.parent);
        dt.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-2f, 2f), 5f), ForceMode2D.Impulse);

        TextMeshProUGUI spawn = dt.GetComponent<TextMeshProUGUI>();
        spawn.text = (amount).ToString();
        spawn.faceColor = col;
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
        //joystick.SetActive(value);
    }

    IEnumerator DamageFlash(int playerNum)
    {
        playerDamaged[playerNum].enabled = true;
        yield return new WaitForSeconds(0.05f);
        playerDamaged[playerNum].enabled = false;
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

    IEnumerator UIDestroyer(int playerNum)
    {
        Rigidbody2D rb = playerUIBar[playerNum].GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.AddForce(new Vector2(Random.Range(-5f, 5f), 5f), ForceMode2D.Impulse);
        //rb.AddTorque(Random.Range(-10f, 10f), ForceMode2D.Impulse);

        yield return new WaitForSeconds(3f);

        rb.bodyType = RigidbodyType2D.Kinematic;
        playerUIBar[playerNum].gameObject.SetActive(false);
    }
}
