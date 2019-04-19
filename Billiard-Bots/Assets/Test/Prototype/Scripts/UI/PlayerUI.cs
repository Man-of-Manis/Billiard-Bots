using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private TMP_Text p1Hp;

    [SerializeField] private TMP_Text p2Hp;

    [SerializeField] private TMP_Text p3Hp;

    [SerializeField] private TMP_Text p4Hp;


    public void UpdatePlayerHealth(int playerNum, int newHealth, int maxHealth)
    {
        switch(playerNum)
        {
            case 1:
                p1Hp.text = newHealth.ToString() + " / " + maxHealth.ToString();
                break;

            case 2:
                p2Hp.text = newHealth.ToString() + " / " + maxHealth.ToString();
                break;

            case 3:
                p3Hp.text = newHealth.ToString() + " / " + maxHealth.ToString();
                break;

            case 4:
                p4Hp.text = newHealth.ToString() + " / " + maxHealth.ToString();
                break;
        }
    }
}
