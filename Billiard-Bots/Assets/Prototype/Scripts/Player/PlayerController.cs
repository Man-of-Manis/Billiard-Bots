using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public bool turnEnabled;

    private PlayerInput pInput;

    private PlayerButtonsUI buttonUI;

    public GameObject arrow;

    public float power { get; private set; }

    public float minPower { get; [SerializeField] private set; } = 0.1f;

    public float maxPower { get; [SerializeField] private set; } = 1f;

    public float xRotationSpeed { get; [SerializeField] private set; } = 3f;

    public float powerMultiplier { get; [SerializeField] private set; } = 1000f;

    private bool direction = true;

    private float currentX;

    public bool arrowActive { get; private set; }

    public bool oscillatorActive { get; private set; }

    public bool prevOscillatorActive { get; private set; }

    public bool freecamActive { get; private set; }

    public bool launchReset { get; private set; }

    public bool UsedTurn { get; private set; } = false;

    public bool ResetCam { get; private set; } = true;

    private bool aButton;

    private bool bButton;

    private bool xButton;

    private bool lBumper;

    private bool rBumper;

    private Rigidbody rb;

    private Transform cam;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cam = FindObjectOfType<Camera>().transform;
        pInput = FindObjectOfType<PlayerInput>();
        buttonUI = FindObjectOfType<PlayerButtonsUI>();
    }

    void Update()
    {
        TurnEnabledUpdate();
    }

    void LateUpdate()
    {
        TurnEnabledLateUpdate();
    }

    void TurnEnabledUpdate()
    {
        if (turnEnabled && !UsedTurn)
        {
            PlayerInput();
            Activation();
            Oscillator();

            if (arrowActive)
            {
                currentX += pInput.players[gameObject.name.ToLower()].leftStick.x * xRotationSpeed;

                if (!arrow.activeSelf)
                {
                    arrow.SetActive(true);
                }
            }

            else
            {
                if (arrow.activeSelf)
                {
                    arrow.SetActive(false);
                }
            }
        }

        if (!turnEnabled && UsedTurn)
        {
            freecamActive = false;

            UsedTurn = false;
        }
    }

    void TurnEnabledLateUpdate()
    {
        if (turnEnabled && !UsedTurn)
        {
            if (ResetCam && !UsedTurn)
            {
                freecamActive = false;
                launchReset = false;
                ResetCam = false;
            }

            if (arrowActive)
            {
                transform.rotation = Quaternion.Euler(new Vector3(0f, currentX, 0f));
            }

            else
            {
                currentX = cam.transform.eulerAngles.y;
            }
        }
    }

    void PlayerInput()
    {
        aButton = pInput.players[gameObject.name.ToLower()].aButton;

        bButton = pInput.players[gameObject.name.ToLower()].bButton;

        xButton = pInput.players[gameObject.name.ToLower()].xButton;

        lBumper = pInput.players[gameObject.name.ToLower()].lBumper;

        rBumper = pInput.players[gameObject.name.ToLower()].rBumper;
    }

    void Activation()
    {
        prevOscillatorActive = oscillatorActive;

        #region A Button Conditions
        if (aButton && oscillatorActive) //A Buttons
        {
            arrowActive = false;
            Launch();

            buttonUI.UIActivation(false, false, false, false, false, false, false, false);
            oscillatorActive = false;
        }

        else if (aButton && arrowActive && !freecamActive)
        {
            oscillatorActive = true;

            buttonUI.UIActivation(false, false, true, false, true, false, false, true);
        }

        else if (aButton && !arrowActive && freecamActive)
        {
            oscillatorActive = false;
            arrowActive = true;

            buttonUI.UIActivation(false, false, true, true, true, false, false, false);
        }

        else if (aButton && !arrowActive)
        {
            arrowActive = true;
            launchReset = false;
            freecamActive = false;

            buttonUI.UIActivation(false, true, false, true, true, false, false, false);
        }

        else if (!arrowActive)
        {
            arrowActive = false;

            buttonUI.UIActivation(true, false, false, false, false, false, false, false);
        }
        #endregion

        #region B Button Conditions
        if (bButton && oscillatorActive) //B Buttons
        {
            oscillatorActive = false;
            buttonUI.UIActivation(false, true, false, true, true, false, false, false);
        }

        else if (bButton && arrowActive && freecamActive)
        {
            freecamActive = false;
            buttonUI.UIActivation(false, true, false, true, true, false, false, false);
        }

        else if (bButton && arrowActive)
        {
            arrowActive = false;
            buttonUI.UIActivation(true, false, false, false, false, false, false, false);
        }
        #endregion

        #region X Button Conditions
        if (xButton && arrowActive && !oscillatorActive && !freecamActive) //X Buttons
        {
            freecamActive = true;
            buttonUI.UIActivation(false, false, false, false, true, true, true, false);
        }
        #endregion

        #region Bumper Conditions
        if (lBumper && freecamActive && !UsedTurn)
        {
            cam.GetComponent<ProtoCameraController>().GetLeftOpponent();
        }

        if (rBumper && freecamActive && !UsedTurn)
        {
            cam.GetComponent<ProtoCameraController>().GetRightOpponent();
        }
        #endregion
    }

    void Oscillator()
    {
        if (oscillatorActive)
        {
            if (prevOscillatorActive != oscillatorActive)
            {
                power = minPower;
                direction = true;
            }

            if (direction)
            {
                power += Time.deltaTime;

                if (power >= maxPower)
                {
                    direction = false;
                }
            }

            else
            {
                power -= Time.deltaTime;

                if (power <= minPower)
                {
                    direction = true;
                }
            }
        }        
    }

    void Launch()
    {
        ResetCam = true;

        launchReset = true;

        freecamActive = true;

        UsedTurn = true;

        rb.AddForce( transform.forward  * power * powerMultiplier, ForceMode.Impulse);

        rb.AddTorque(transform.right * power * powerMultiplier, ForceMode.VelocityChange);
    }

}
