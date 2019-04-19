using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public bool turnEnabled;

    public GameObject arrow;

    public float xRotationSpeed { get; [SerializeField] private set; } = 3f;

    private float currentX;

    public bool arrowActive;

    public bool freecamActive { get; private set; }

    public bool launchReset { get; private set; }

    public bool UsedTurn { get; private set; } = false;

    public bool ResetCam { get; private set; } = true;

    private Rigidbody rb;

    private Transform cam;

    private PlayerStats stats;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cam = FindObjectOfType<Camera>().transform;
        stats = GetComponent<PlayerStats>();
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
            if (arrowActive)
            {
                currentX += PlayerInput.Instance.players[(int)gameObject.GetComponent<PlayerIdentifier>().player + 1].leftStick.x * xRotationSpeed;
            }
        }

        if (!turnEnabled && UsedTurn)
        {
            freecamActive = false;

            UsedTurn = false;
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
                transform.rotation = Quaternion.Euler(new Vector3(0f, currentX, 0f));
            }

            else
            {
                currentX = cam.transform.eulerAngles.y;
            }
        }
    }
   

    public void Launch(float power, float powerMultiplier)
    {
        ResetCam = true;

        launchReset = true;

        freecamActive = true;

        UsedTurn = true;

        PlayerTurnTimer.Instance.StopTimer();

        stats.completedTurns++;

        rb.AddForce( transform.forward  * power * powerMultiplier, ForceMode.Impulse);

        rb.AddTorque(transform.right * power * powerMultiplier, ForceMode.VelocityChange);
    }

    public void TimeUp()
    {
        UsedTurn = true;
    }
}
