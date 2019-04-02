using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterLockIn : MonoBehaviour
{
    [Tooltip("Order: Blue, Green, Violet, Red")]
    public GameObject[] playerOptions;

    //The characters locked in by players in order of the player number. Eg. Player 1 is 0
    public List<GameObject> PlayerCharacters;

    public static CharacterLockIn Instance
    {
        get { return s_Instance; }
    }

    protected static CharacterLockIn s_Instance;

    void Awake()
    {
        if (s_Instance == null)
        {
            s_Instance = this;
        }
        else if (s_Instance != this)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }

    public void PlayerChoice(int botNum, int playerspot)
    {
        if(botNum.Equals(0))
        {
            PlayerCharacters[playerspot] = playerOptions[0];
        }

        if (botNum.Equals(1))
        {
            PlayerCharacters[playerspot] = playerOptions[1];
        }

        if (botNum.Equals(2))
        {
            PlayerCharacters[playerspot] = playerOptions[2];
        }

        if (botNum.Equals(3))
        {
            PlayerCharacters[playerspot] = playerOptions[3];
        }
    }

    public void PlayerCancel(int playerspot)
    {
        PlayerCharacters[playerspot] = null;
    }

    public void PlayerCount(int amount)
    {
        PlayerCharacters.Clear();

        for (int i = 0; i < amount; i++)
        {
            PlayerCharacters.Insert(i, null);
        }
    }
}
