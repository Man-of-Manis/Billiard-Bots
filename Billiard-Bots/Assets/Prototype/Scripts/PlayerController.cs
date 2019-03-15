using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public string playerNumber;

    public bool turnEnabled;

    public Canvas playerCanvas;

    public RectTransform arrowRotator;

    public Image powerMeter;

    public GameObject powerMeterObj;

    public Transform cameraTransform;

    [SerializeField] private readonly float xRotationSpeed = 3f;

    [SerializeField] private readonly float powerMultiplier = 1000f;

    [SerializeField] private readonly float maxPower = 1f;

    [SerializeField] private float power;

    private Vector2 leftInput;

    private float currentX;

    private bool aButton;

    private bool bButton;

    private bool xButton;

    private bool lBumper;

    private bool rBumper;

    private bool arrowActive;

    private bool oscillatorActive;

    private bool prevOscillatorActive;

    public bool freecamActive { get; private set; }

    private bool direction;

    public bool noMovement;

    public bool launchReset { get; private set; }

    public bool UsedTurn;

    public bool ResetCam { get; private set; } = true;

    private Rigidbody rb;

    private Transform cam;

    [Header("UI")]
    public GameObject startAiming;

    public GameObject setPower;

    public GameObject lockPower;

    public GameObject freecamObj;

    public GameObject cancel;

    public GameObject lBumperObj;

    public GameObject rBumperObj;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cam = FindObjectOfType<Camera>().transform;

        startAiming.SetActive(false);
        setPower.SetActive(false);
        lockPower.SetActive(false);
        freecamObj.SetActive(false);
        cancel.SetActive(false);
        lBumperObj.SetActive(false);
        rBumperObj.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(turnEnabled)
        {
            if (arrowActive)
            {
                currentX += Input.GetAxisRaw("P" + playerNumber + "_L_Horizontal") * xRotationSpeed;
            }

            PlayerInput();

            

            Activation();

            Oscillator();

            PowerMeter();

            

            GetVelocity();

            if (arrowActive)
            {
                if (!playerCanvas.enabled)
                {
                    playerCanvas.enabled = true;
                }
            }

            else
            {
                if (playerCanvas.enabled)
                {
                    playerCanvas.enabled = false;
                }
            }
        }       
    }

    void FixedUpdate()
    {
        GetVelocity();

        TurnComplete();
    }

    void LateUpdate()
    {
        if(turnEnabled)
        {
            if (ResetCam && !UsedTurn)
            {
                currentX = cam.transform.eulerAngles.y;
                transform.rotation = Quaternion.Euler(new Vector3(0f, cam.transform.eulerAngles.y, 0f));
                freecamActive = false;
                launchReset = false;
                ResetCam = false;
            }

            if (arrowActive)
            {
                transform.rotation = Quaternion.Euler(new Vector3(0f, currentX, 0f));
            }
        }               
    }


    void PlayerInput()
    {
        leftInput = new Vector2(Input.GetAxisRaw("P" + playerNumber + "_L_Horizontal"), Input.GetAxisRaw("P" + playerNumber + "_L_Vertical"));

        aButton = Input.GetButtonDown("P" + playerNumber + "_A_Button");

        bButton = Input.GetButtonDown("P" + playerNumber + "_B_Button");

        xButton = Input.GetButtonDown("P" + playerNumber + "_X_Button");

        lBumper = Input.GetButtonDown("P" + playerNumber + "_L_Bumper");

        rBumper = Input.GetButtonDown("P" + playerNumber + "_R_Bumper");
    }

    void Activation()
    {
        prevOscillatorActive = oscillatorActive;

        #region A Button Conditions
        if (aButton && oscillatorActive) //A Buttons
        {
            Launch();

            setPower.SetActive(false);
            lockPower.SetActive(false);
            freecamObj.SetActive(false);
            cancel.SetActive(false);

            arrowActive = false;
            oscillatorActive = false;
        }

        else if(aButton && arrowActive && !freecamActive)
        {
            freecamActive = false;
            oscillatorActive = true;

            setPower.SetActive(false);
            lockPower.SetActive(true);
            freecamObj.SetActive(false);
        }

        else if (aButton && !arrowActive && noMovement && freecamActive)
        {
            arrowActive = true;

            startAiming.SetActive(false);
            setPower.SetActive(true);
            freecamObj.SetActive(true);
            cancel.SetActive(true);


        }

        else if(aButton && !arrowActive && noMovement)
        {
            arrowActive = true;
            launchReset = false;
            freecamActive = false;    
            

            startAiming.SetActive(false);
            setPower.SetActive(true);
            freecamObj.SetActive(true);
            cancel.SetActive(true);

            
        }

        else if(!arrowActive && noMovement)
        {
            startAiming.SetActive(true);            
        }
        #endregion


        if (bButton && oscillatorActive) //B Buttons
        {
            lockPower.SetActive(false);
            setPower.SetActive(true);
            freecamObj.SetActive(true);
            cancel.SetActive(true);

            oscillatorActive = false;
        }

        else if (bButton && arrowActive && freecamActive)
        {
            freecamActive = false;

            setPower.SetActive(true);
            lockPower.SetActive(false);
            freecamObj.SetActive(true);
            cancel.SetActive(true);
            lBumperObj.SetActive(false);
            rBumperObj.SetActive(false);
            startAiming.SetActive(false);
        }

        else if(bButton && arrowActive)
        {
            freecamActive = false;

            setPower.SetActive(false);
            lockPower.SetActive(false);
            freecamObj.SetActive(false);
            cancel.SetActive(false);
            startAiming.SetActive(true);

            arrowActive = false;
        }

        if(xButton && arrowActive && !oscillatorActive && !freecamActive) //X Buttons
        {
            setPower.SetActive(false);
            lockPower.SetActive(false);
            freecamObj.SetActive(false);
            cancel.SetActive(true);
            lBumperObj.SetActive(true);
            rBumperObj.SetActive(true);

            freecamActive = true;
        }

        if(lBumper && freecamActive && !UsedTurn)
        {
            cam.GetComponent<ProtoCameraController>().GetLeftOpponent();
        }

        if (rBumper && freecamActive && !UsedTurn)
        {
            cam.GetComponent<ProtoCameraController>().GetRightOpponent();
        }
    }

    void GetVelocity()
    {
        noMovement = rb.velocity.Equals(Vector3.zero);
    }

    void Oscillator()
    {
        if(oscillatorActive)
        {
            if(prevOscillatorActive != oscillatorActive)
            {
                powerMeterObj.SetActive(true);
                power = 0f;
                direction = true;
                powerMeter.color = Color.red;
            }

            if(direction)
            {
                power += Time.deltaTime;

                if(power >= maxPower)
                {
                    direction = false;
                }
            }

            else
            {
                power -= Time.deltaTime;

                if (power <= 0f)
                {
                    direction = true;
                }
            }
        }

        else
        {
            if (prevOscillatorActive != oscillatorActive)
            {
                powerMeterObj.SetActive(false);
                powerMeter.color = Color.clear;
            }            
        }
    }

    void PowerMeter()
    {
        powerMeter.fillAmount = power;
    }

    void Launch()
    {
        ResetCam = true;

        launchReset = true;

        freecamActive = true;

        rb.AddForce( transform.forward  * power * powerMultiplier, ForceMode.Impulse);

        rb.AddTorque(transform.right * power * powerMultiplier, ForceMode.VelocityChange);

        UsedTurn = true;
    }

    void TurnComplete()
    {
        if(UsedTurn && PlayerTurn.Instance.PlayerMovement())
        {
            Debug.Log("No players moving");
            PlayerTurn.Instance.EndTurn(gameObject.name);
            freecamActive = false;
            UsedTurn = false;
        }
    }
}
