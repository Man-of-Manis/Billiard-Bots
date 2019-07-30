using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPlayerFlippers : MonoBehaviour
{
    public BilliardFlipper[] flippers = new BilliardFlipper[8];

    // Start is called before the first frame update
    void Awake()
    {
        int playerAmount = 0;

        if(CharacterLockIn.Instance.PlayerCharacters.Count != 0 )
        {
            playerAmount = CharacterLockIn.Instance.PlayerCharacters.Count;
        }

        else
        {
            playerAmount = PlayerInputManager.Instance.ControllersConnected;
        }

        for(int i = 0; i < flippers.Length; i++)
        {
            flippers[i].num = (BilliardFlipper.PlayerInputNum)(i % playerAmount);
            Debug.Log(i % playerAmount);
        }
    }
}
