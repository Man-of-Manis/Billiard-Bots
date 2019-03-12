using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Canvas playerCanvas;

    public RectTransform arrowRotator;

    public Image powerMeter;

    public Transform cameraTransform;

    public float powerMultiplier;

    public float maxPower;

    private Vector2 leftInput;

    private bool aButton;

    private bool bButton;

    private bool arrowActive;

    private bool oscillatorActive;

    private bool prevOscillatorActive;

    private bool launchActive;

    public float power;

    private bool direction;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerInput();

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
            if (!leftInput.Equals(Vector2.zero))
            {
                //arrowRotator.localEulerAngles = new Vector3(0f, 0f, Mathf.Atan2(leftInput.x, leftInput.y) * -180 / Mathf.PI);

                //arrowRotator.rotation = Quaternion.Euler(new Vector3(0f, 0f, -cameraTransform.eulerAngles.y)) * Quaternion.Euler(new Vector3(0f, 0f, Mathf.Atan2(leftInput.x, leftInput.y) * -180 / Mathf.PI));

                //transform.rotation = Quaternion.Euler(new Vector3(0f, cameraTransform.eulerAngles.y, 0f));

            }

            transform.rotation = Quaternion.Euler(new Vector3(0f, cameraTransform.eulerAngles.y, 0f));
        }
        
    }

    void PlayerInput()
    {
        leftInput = new Vector2(Input.GetAxisRaw("P1_L_Horizontal"), Input.GetAxisRaw("P1_L_Vertical"));

        aButton = Input.GetButtonDown("P1_A_Button");

        bButton = Input.GetButtonDown("P1_B_Button");
    }

    void Activation()
    {
        prevOscillatorActive = oscillatorActive;

        if(aButton && oscillatorActive)
        {
            Launch();
            arrowActive = false;
            oscillatorActive = false;
        }

        else if(aButton && arrowActive)
        {
            oscillatorActive = true;
        }

        else if(aButton && !arrowActive)
        {
            arrowActive = true;
        }

        if(bButton && oscillatorActive)
        {
            oscillatorActive = false;
        }

        else if(bButton && arrowActive)
        {
            arrowActive = false;
        }
    }

    void Oscillator()
    {
        if(oscillatorActive)
        {
            if(prevOscillatorActive != oscillatorActive)
            {
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
            powerMeter.color = Color.clear;
        }
    }

    void PowerMeter()
    {
        powerMeter.fillAmount = power;
    }

    void Launch()
    {
        rb.AddForce( transform.forward  * power * powerMultiplier, ForceMode.Impulse);
    }


}
