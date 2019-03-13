using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Canvas playerCanvas;

    public RectTransform arrowRotator;

    public Image powerMeter;

    public GameObject powerMeterObj;

    public Transform cameraTransform;

    [SerializeField] private float xRotationSpeed;

    [SerializeField] private float powerMultiplier;

    [SerializeField] private float maxPower;

    [SerializeField] private float power;

    private Vector2 leftInput;

    private float currentX;

    private bool aButton;

    private bool bButton;

    private bool xButton;

    private bool arrowActive;

    private bool oscillatorActive;

    private bool prevOscillatorActive;

    public bool freecamActive { get; private set; }

    private bool direction;

    private bool noMovement;

    private bool reset;

    private Rigidbody rb;

    private Transform cam;

    [Header("UI")]
    public GameObject startAiming;

    public GameObject setPower;

    public GameObject lockPower;

    public GameObject freecamObj;

    public GameObject cancel;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cam = FindObjectOfType<Camera>().transform;

        startAiming.SetActive(false);
        setPower.SetActive(false);
        lockPower.SetActive(false);
        freecamObj.SetActive(false);
        cancel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (arrowActive)
        {
            currentX += Input.GetAxisRaw("P1_L_Horizontal") * xRotationSpeed;
        }        

        PlayerInput();

        GetVelocity();

        Activation();

        Oscillator();

        PowerMeter();

        if (arrowActive)
        {
            if (!playerCanvas.enabled)
            {
                playerCanvas.enabled = true;
            }            
        }        

        else
        {
            if(playerCanvas.enabled)
            {
                playerCanvas.enabled = false;
            }
        }
    }

    void LateUpdate()
    {
        if (arrowActive)
        {
            if(startAiming.activeSelf)
            {
                transform.rotation = Quaternion.Euler(new Vector3(0f, cam.transform.eulerAngles.y, 0f));
            }
            else
            {
                transform.rotation = Quaternion.Euler(new Vector3(0f, currentX, 0f));
            }            
        }        
    }

    void PlayerInput()
    {
        leftInput = new Vector2(Input.GetAxisRaw("P1_L_Horizontal"), Input.GetAxisRaw("P1_L_Vertical"));

        aButton = Input.GetButtonDown("P1_A_Button");

        bButton = Input.GetButtonDown("P1_B_Button");

        xButton = Input.GetButtonDown("P1_X_Button");
    }

    void Activation()
    {
        prevOscillatorActive = oscillatorActive;

        if(aButton && oscillatorActive)
        {
            Launch();

            setPower.SetActive(false);
            lockPower.SetActive(false);
            freecamObj.SetActive(false);
            cancel.SetActive(false);

            arrowActive = false;
            oscillatorActive = false;
        }

        else if(aButton && arrowActive)
        {
            freecamActive = false;
            oscillatorActive = true;

            setPower.SetActive(false);
            lockPower.SetActive(true);
            freecamObj.SetActive(false);
        }

        else if(aButton && !arrowActive && noMovement)
        {
            arrowActive = true;
            freecamActive = false;            

            startAiming.SetActive(false);
            setPower.SetActive(true);
            freecamObj.SetActive(true);
            cancel.SetActive(true);           
        }

        else if(!arrowActive && noMovement)
        {
            startAiming.SetActive(true);
            reset = true;
        }


        if(bButton && oscillatorActive)
        {
            lockPower.SetActive(false);
            setPower.SetActive(true);
            freecamObj.SetActive(true);
            cancel.SetActive(true);

            oscillatorActive = false;
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

        if(xButton && arrowActive && !oscillatorActive)
        {
            freecamActive = !freecamActive;
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
        freecamActive = true;

        rb.AddForce( transform.forward  * power * powerMultiplier, ForceMode.Impulse);

        rb.AddTorque(transform.right * power * powerMultiplier, ForceMode.VelocityChange);
    }


}
