using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameActivator : MonoBehaviour
{
    public bool cam = false;

    public PlayerTurn turn;
    public PlayerInput input;
    public PlayerTurnInput turnInput;
    public PlayerTurnTimer turnTimer;
    public PlayerPower power;
    public PlayerButtonsUI buttonUI;
    public CinemachineBrain brain;
    public ProtoCameraController camController;
    public CameraController camCtrl;
    public TestInputControl inputCtrl;
    public GameObject mapTitle;
    public Canvas UI;

    // Start is called before the first frame update
    void Start()
    {
        turn.enabled = true;
        input.enabled = true;
        turnInput.enabled = true;
        turnTimer.enabled = true;
        power.enabled = true;
        buttonUI.enabled = true;
        brain.enabled = false;

        if(cam)
        {
            camCtrl.enabled = true;
            inputCtrl.enabled = true;
        }

        else
        {
            camController.enabled = true;
        }

        
        mapTitle.SetActive(false);
        UI.enabled = true;
        this.enabled = false;
    }
}
