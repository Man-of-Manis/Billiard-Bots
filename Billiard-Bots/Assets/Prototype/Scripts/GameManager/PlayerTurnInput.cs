using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurnInput : MonoBehaviour
{
    private PlayerButtonsUI buttonUI;

    private GameObject currentPlayer;

    private GameObject previousPlayer;

    private PlayerController currentController;

    private bool usedTurn;

    private bool aButton;

    private bool bButton;

    private bool xButton;

    private bool lBumper;

    private bool rBumper;

    // Start is called before the first frame update
    void Start()
    {
        buttonUI = GetComponent<PlayerButtonsUI>();
    }

    // Update is called once per frame
    void Update()
    {
        CurrentPlayer();
        CurrentPlayerInput();
        PlayerActions();
    }

    void CurrentPlayer()
    {
        previousPlayer = currentPlayer;
        currentPlayer = PlayerTurn.Instance.playerObjTurn;

        if (previousPlayer != currentPlayer)
        {
            currentController = currentPlayer.GetComponent<PlayerController>();
            usedTurn = false;
        }
    }

    void CurrentPlayerInput()
    {
        aButton = PlayerInput.Instance.players[currentPlayer.name.ToLower()].aButton;

        bButton = PlayerInput.Instance.players[currentPlayer.name.ToLower()].bButton;

        xButton = PlayerInput.Instance.players[currentPlayer.name.ToLower()].xButton;

        lBumper = PlayerInput.Instance.players[currentPlayer.name.ToLower()].lBumper;

        rBumper = PlayerInput.Instance.players[currentPlayer.name.ToLower()].rBumper;
    }

    void PlayerActions()
    {
        if(!usedTurn)
        {
            if (aButton) { AButtonActions(); }

            if (bButton) { BButtonActions(); }

            if (xButton) { XButtonActions(); }

            if (lBumper) { LBumperActions(); }

            if (rBumper) { RBumperActions(); }

            if (!currentController.arrowActive && !usedTurn)
            {
                currentController.arrowActive = false;

                buttonUI.UIActivation(true, false, false, false, false, false, false, false);
            }
        }
    }

    void AButtonActions()
    {
        //Launch bot at current power level
        if (PlayerPower.Instance.oscillatorActive)
        {
            currentController.arrowActive = false;
            PlayerPower.Instance.SendPower();
            buttonUI.UIActivation(false, false, false, false, false, false, false, false);
            PlayerPower.Instance.oscillatorActive = false;
        }

        //Start oscillator power
        else if (currentController.arrowActive && !ProtoCameraController.Instance.freecamActive)
        {
            PlayerPower.Instance.oscillatorActive = true;

            buttonUI.UIActivation(false, false, true, false, true, false, false, true);
        }

        //Start turn
        else if (!currentController.arrowActive)
        {
            currentController.arrowActive = true;
            //launchReset = false;
            ProtoCameraController.Instance.freecamActive = false;

            buttonUI.UIActivation(false, true, false, true, true, false, false, false);
        }
    }

    void BButtonActions()
    {
        //Cancel power meter
        if (PlayerPower.Instance.oscillatorActive) //B Buttons
        {
            PlayerPower.Instance.oscillatorActive = false;
            buttonUI.UIActivation(false, true, false, true, true, false, false, false);
        }

        //Exit freecam mode
        else if (currentController.arrowActive && ProtoCameraController.Instance.freecamActive)
        {
            ProtoCameraController.Instance.freecamActive = false;
            buttonUI.UIActivation(false, true, false, true, true, false, false, false);
        }
    }

    void XButtonActions()
    {
        //Activate freecam mode during turn
        if (currentController.arrowActive && !PlayerPower.Instance.oscillatorActive && !ProtoCameraController.Instance.freecamActive)
        {
            ProtoCameraController.Instance.freecamActive = true;
            buttonUI.UIActivation(false, false, false, false, true, true, true, false);
        }
    }

    void LBumperActions()
    {
        //Move camera to left bot while in freecam and haven't used turn
        if (ProtoCameraController.Instance.freecamActive && !usedTurn)
        {
            ProtoCameraController.Instance.GetLeftOpponent();
        }
    }

    void RBumperActions()
    {
        //Move camera to right bot while in freecam and haven't used turn
        if (ProtoCameraController.Instance.freecamActive && !usedTurn)
        {
            ProtoCameraController.Instance.GetRightOpponent();
        }
    }
}
