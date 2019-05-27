using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public bool turnEnabled;

    public GameObject arrow;

    public float xRotationSpeed { get; [SerializeField] private set; } = 3f;

    [SerializeField] private float torqueSpeed = 10000f;

    private float currentX;

    public bool arrowActive;

    public bool freecamActive { get; private set; }

    public bool launchReset { get; private set; }

    public bool UsedTurn { get; private set; } = false;

    public bool ResetCam { get; private set; } = true;

    public Rigidbody rb { get; private set; }

    bool on = false;

    bool off = true;

    //private Transform cam;

    private PlayerStats stats;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //cam = FindObjectOfType<Camera>().transform;
        stats = GetComponent<PlayerStats>();

        torqueSpeed = 100f;
    }

    void Update()
    {
        TurnEnabledUpdate();
    }

    void LateUpdate()
    {
        TurnEnabledLateUpdate();

        if(turnEnabled && UsedTurn)
        {
            Turn();
        }
        
    }

    void TurnEnabledUpdate()
    {
        if (turnEnabled && !UsedTurn)
        {
            if (arrowActive)
            {
                float playerX = PlayerInput.Instance.players[(int)gameObject.GetComponent<PlayerIdentifier>().player + 1].leftStick.x;
                currentX += playerX * Mathf.Abs(playerX) * xRotationSpeed;
            }
        }

        if (!turnEnabled && UsedTurn)
        {
            freecamActive = false;

            UsedTurn = false;

            on = false;

            off = true;
        }

        arrow.SetActive(arrowActive ? true : false);
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
                transform.rotation = Quaternion.Euler(new Vector3(0f, Camera.main.transform.eulerAngles.y, 0f));
            }

            else
            {
                //currentX = cam.transform.eulerAngles.y;
            }
        }
    }
   

    public void Launch(float power, float powerMultiplier)
    {
        ResetCam = true;

        launchReset = true;

        freecamActive = true;

        UsedTurn = true;

        //turnEnabled = false;

        PlayerTurnTimer.Instance.StopTimer();

        stats.completedTurns++;

        rb.AddForce( transform.forward  * power * powerMultiplier, ForceMode.Impulse);

        rb.AddTorque(transform.right * power * powerMultiplier, ForceMode.VelocityChange);
    }

    private void Turn()
    {
        if(!on)
        {
            PlayerUI ui = FindObjectOfType<PlayerUI>();
            ui.JoystickAnim(true);
            off = false;
            on = true;
        }

        Vector2 inputs = new Vector2(PlayerInput.Instance.players[(int)gameObject.GetComponent<PlayerIdentifier>().player + 1].leftStick.x,
            PlayerInput.Instance.players[(int)gameObject.GetComponent<PlayerIdentifier>().player + 1].leftStick.y);

        if(!inputs.Equals(Vector2.zero) && !off)
        {
            PlayerUI ui = FindObjectOfType<PlayerUI>();
            ui.JoystickAnim(false);
            off = true;
        }


        if (turnEnabled && UsedTurn)
        {
            rb.velocity = rb.velocity.magnitude * Vector3.Lerp(rb.velocity.normalized, Camera.main.transform.right *
                ((PlayerInput.Instance.players[(int)gameObject.GetComponent<PlayerIdentifier>().player + 1].leftStick.x) > 0f ? 1f : -1f),
                Mathf.Abs((PlayerInput.Instance.players[(int)gameObject.GetComponent<PlayerIdentifier>().player + 1].leftStick.x)) * 0.01f);
        }


    }

    public void TimeUp()
    {
        UsedTurn = true;
        PlayerPower.Instance.oscillatorActive = false;
    }
}
