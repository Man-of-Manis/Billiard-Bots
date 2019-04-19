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

    [SerializeField] private Image powerMeterBG;

    [SerializeField] private Image powerMeter;

    [Header("Current Player")]
    public GameObject currentPlayer;

    private GameObject previousPlayer;

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
        PowerMeterHeight();
        PowerMeter();
    }

    void Input()
    {
        aButton = pInput.players[(int)turn.playerObjTurn.GetComponent<PlayerIdentifier>().player + 1].aButton;

        bButton = pInput.players[(int)turn.playerObjTurn.GetComponent<PlayerIdentifier>().player + 1].bButton;

        xButton = pInput.players[(int)turn.playerObjTurn.GetComponent<PlayerIdentifier>().player + 1].xButton;

        lBumper = pInput.players[(int)turn.playerObjTurn.GetComponent<PlayerIdentifier>().player + 1].lBumper;

        rBumper = pInput.players[(int)turn.playerObjTurn.GetComponent<PlayerIdentifier>().player + 1].rBumper;
    }

    void Player()
    {
        previousPlayer = currentPlayer;
        currentPlayer = turn.playerObjTurn;
        //arrowActive = currentPlayer.GetComponent<PlayerController>().arrowActive;
        oscillatorActive = PlayerPower.Instance.oscillatorActive;
        //prevOscillatorActive = PlayerPower.Instance.prevOscillatorActive;
        //freecamActive = currentPlayer.GetComponent<PlayerController>().freecamActive;
        //launchReset = currentPlayer.GetComponent<PlayerController>().launchReset;
        //UsedTurn = currentPlayer.GetComponent<PlayerController>().UsedTurn;
        power = PlayerPower.Instance.power;
        minPower = PlayerPower.Instance.minPower;
        maxPower = PlayerPower.Instance.maxPower;
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

    void PowerMeterHeight()
    {
        powerMeterBG.rectTransform.sizeDelta = new Vector2(powerMeterBG.rectTransform.sizeDelta.x, maxPower * 200f);
        powerMeter.rectTransform.sizeDelta = new Vector2(powerMeter.rectTransform.sizeDelta.x, maxPower * 200f - maxPower * 10f);      
    }

    void PowerMeter()
    {
        powerMeter.fillAmount = (power - minPower) / (maxPower - minPower);
    }
}
