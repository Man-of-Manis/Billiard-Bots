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

    private AudioSource audioSrc;

    bool rolling = false;

    bool on = false;

    bool off = true;

    private PlayerStats stats;

    private Vector3 prevPos;

    private Transform cam;

    private Vector3 zero;

    private Animator anim;

    private bool enableAnim = false;

    Coroutine co;

    bool runningCo = false;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSrc = GetComponent<AudioSource>();
        anim = transform.Find("P_BilliardBot").GetComponent<Animator>();
        stats = GetComponent<PlayerStats>();
        cam = FindObjectOfType<CameraController>().transform;

        torqueSpeed = 100f;
        prevPos = transform.position;
    }

    void Update()
    {
        TurnEnabledUpdate();
        
    }

    private void FixedUpdate()
    {
        Rolling();
        Distance();
        Velocity();
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
                transform.rotation = Quaternion.Euler(new Vector3(0f, cam.eulerAngles.y, 0f));
            }
        }
    }
   

    public void Launch(float power, float powerMultiplier)
    {
        enableAnim = false;

        ResetCam = true;

        launchReset = true;

        freecamActive = true;

        UsedTurn = true;

        PlayerTurnTimer.Instance.StopTimer();

        stats.completedTurns++;

        rb.AddForce( transform.forward  * power * powerMultiplier, ForceMode.Impulse);

        rb.AddTorque(transform.right * power * powerMultiplier, ForceMode.VelocityChange);
    }



    private void Turn()
    {
        if(!on && launchReset)
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
            //rb.velocity = rb.velocity.magnitude * Vector3.Lerp(rb.velocity.normalized, Camera.main.transform.right * ((inputs.x) > 0f ? 1f : -1f), Mathf.Abs((inputs.x)) * 0.02f);
            //Vector3 newVelocity = cam.right * ((inputs.x) > 0f ? 1f : -1f);

            //Vector3 newVelocity =
            /*
            if (!inputs.Equals(Vector2.zero))
            {
                rb.AddForce(newVelocity - rb.velocity, ForceMode.VelocityChange);
            }
            */


            float ang = Mathf.Atan2(inputs.y, inputs.x) * Mathf.Rad2Deg;

            Vector3 flatCam = Quaternion.AngleAxis(cam.eulerAngles.y, Vector3.up) * Vector3.forward;

            Vector3 dir = Quaternion.AngleAxis(-ang + 90f, Vector3.up) * flatCam;

            Vector3 SetDir = inputs.Equals(Vector2.zero) ? flatCam : dir;
            
            if (!inputs.Equals(Vector2.zero))
            {
                rb.velocity = rb.velocity.magnitude * (Vector3.SmoothDamp(rb.velocity.normalized, SetDir, ref zero, 0.75f)).normalized;
            }
        }


    }

    public void TimeUp()
    {
        UsedTurn = true;
        //turnEnabled = false;
        arrowActive = false;

        PlayerPower.Instance.oscillatorActive = false;

        PlayerUI ui = FindObjectOfType<PlayerUI>();
        ui.JoystickAnim(false);
    }

    void Rolling()
    {
        float vel = rb.velocity.magnitude;
        
        if(stats.playerStatistics.topSpeed < vel)
        {
            stats.playerStatistics.topSpeed = vel;
        }

        audioSrc.volume = vel / 7.5f;

        if (vel > 0.1f && !rolling)
        {
            audioSrc.Play();
            rolling = true;
        }

        else if(vel < 0.1f && rolling)
        {
            audioSrc.Stop();
            rolling = false;
        }
    }

    void Distance()
    {
        if (prevPos != transform.position)
        {
            float distTrav = Vector3.Distance(prevPos, transform.position);
            stats.playerStatistics.distanceTraveled += distTrav;
            prevPos = transform.position;
        }
    }

    IEnumerator AnimWait()
    {
        yield return new WaitForSeconds(0.5f);
        Debug.Log(gameObject.name + " - Enabled Anim");
        enableAnim = true;
    }

    void Velocity()
    {
        //rb.velocity = Mathf.Clamp(rb.velocity.magnitude, 0.0f, 60f) * rb.velocity.normalized; 

        if (!turnEnabled || (turnEnabled && UsedTurn))
        {
            if (rb.velocity.magnitude < 0.001f && !runningCo)
            {
                co = StartCoroutine(AnimWait());
                runningCo = true;
            }

            else if (rb.velocity.magnitude > 0.01f && runningCo)
            {
                StopCoroutine(co);
                //enableAnim = false;
                runningCo = false;
            }

            if (enableAnim)
            {
                if ((anim.GetCurrentAnimatorStateInfo(0).IsName("Expand") || anim.GetCurrentAnimatorStateInfo(0).IsName("Idle")) && rb.velocity.magnitude > 0.01f)
                {
                    Debug.Log(gameObject.name + " - Ball Up");
                    anim.SetTrigger("BallUp");
                }

                if (anim.GetCurrentAnimatorStateInfo(0).IsName("BallUp") && rb.velocity.magnitude < 0.001f)
                {
                    Debug.Log(gameObject.name + " - Expand");
                    transform.rotation = Quaternion.Euler(0f, transform.eulerAngles.y, 0f);
                    anim.SetTrigger("Expand");
                }
            }
        } 
    }

    public void CharAnimations(string animation)
    {
        anim.SetTrigger(animation);
    }
}
