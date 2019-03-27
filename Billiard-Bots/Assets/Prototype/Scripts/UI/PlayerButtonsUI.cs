using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerButtonsUI : MonoBehaviour
{
    private PlayerTurn turn;

    private PlayerInput pInput;

    private Camera cam;

    private bool aButton;

    private bool bButton;

    private bool xButton;

    private bool lBumper;

    private bool rBumper;

    private float power;

    private float minPower;

    private float maxPower;
    

    [Header("Controls")]
    [SerializeField] private GameObject startAiming;

    [SerializeField] private GameObject setPower;

    [SerializeField] private GameObject lockPower;

    [SerializeField] private GameObject freecamObj;

    [SerializeField] private GameObject cancelObj;

    [SerializeField] private GameObject lBumperObj;

    [SerializeField] private GameObject rBumperObj;

    [SerializeField] private GameObject powerMeterObj;

    [SerializeField] private Image powerMeter;

    [Header("Current Player")]
    public GameObject currentPlayer;

    public bool arrowActive;

    private bool oscillatorActive;

    private bool prevOscillatorActive;

    private bool freecamActive;

    private bool launchReset;

    private bool UsedTurn;

    // Start is called before the first frame update
    void Start()
    {
        turn = GetComponent<PlayerTurn>();
        pInput = GetComponent<PlayerInput>();
        cam = FindObjectOfType<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        Input();
        Player();
        PowerMeter();
    }

    void Input()
    {
        aButton = pInput.players[turn.playerObjTurn.name.ToLower()].aButton;

        bButton = pInput.players[turn.playerObjTurn.name.ToLower()].bButton;

        xButton = pInput.players[turn.playerObjTurn.name.ToLower()].xButton;

        lBumper = pInput.players[turn.playerObjTurn.name.ToLower()].lBumper;

        rBumper = pInput.players[turn.playerObjTurn.name.ToLower()].rBumper;
    }

    void Player()
    {
        currentPlayer = turn.playerObjTurn;
        arrowActive = currentPlayer.GetComponent<PlayerController>().arrowActive;
        oscillatorActive = currentPlayer.GetComponent<PlayerController>().oscillatorActive;
        prevOscillatorActive = currentPlayer.GetComponent<PlayerController>().prevOscillatorActive;
        freecamActive = currentPlayer.GetComponent<PlayerController>().freecamActive;
        launchReset = currentPlayer.GetComponent<PlayerController>().launchReset;
        UsedTurn = currentPlayer.GetComponent<PlayerController>().UsedTurn;
        power = currentPlayer.GetComponent<PlayerController>().power;
        minPower = currentPlayer.GetComponent<PlayerController>().minPower;
        maxPower = currentPlayer.GetComponent<PlayerController>().maxPower;
    }

    public void UIActivation(bool aiming, bool setpower, bool lockpower, bool freecam, bool cancel, bool lbumper, bool rbumper, bool powermeter)
    {
        startAiming.SetActive(aiming);

        setPower.SetActive(setpower);

        lockPower.SetActive(lockpower);

        freecamObj.SetActive(freecam);

        cancelObj.SetActive(cancel);

        lBumperObj.SetActive(lbumper);

        rBumperObj.SetActive(rbumper);

        powerMeterObj.SetActive(powermeter);
    }

    void PowerMeter()
    {
        powerMeter.fillAmount = (power - minPower) / (maxPower - minPower);
    }
}
