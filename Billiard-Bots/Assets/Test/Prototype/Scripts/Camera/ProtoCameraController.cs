using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtoCameraController : MonoBehaviour
{
    public string playerNumber;

    private const float minY = 0f;
    private const float maxY = 85f;
    private const float distance = -8f;

    private float currentX;
    private float currentY;

    private float velocity = 0f;

    public bool freecamActive;
    public bool prevFreecamActive;

    public Transform currentPlayerTarget;

    [SerializeField] private Transform prevPlayerTarget;

    [SerializeField] private Transform opponentTarget;

    [SerializeField] private int opponentTargetNum;

    [SerializeField] private PlayerController currentController;

    public float xRotationSpeed;
    public float yRotationSpeed;

    public static ProtoCameraController Instance
    {
        get { return s_Instance; }
    }

    protected static ProtoCameraController s_Instance;

    void Awake()
    {
        if (s_Instance == null)
        {
            s_Instance = this;
        }

        else if (s_Instance != this)
        {
            Destroy(this.gameObject);
        }
    }

    void Start()
    {
        currentPlayerTarget = PlayerTurn.Instance.playerObjTurn.transform;

        playerNumber = PlayerTurn.Instance.playerObjTurn.name[PlayerTurn.Instance.playerObjTurn.name.Length - 1].ToString();

        currentY = 20f;

        currentController = currentPlayerTarget.GetComponent<PlayerController>();
    }
        
    void Update()
    {
        CameraSettings();
    }    

    void LateUpdate()
    {
        if(freecamActive || currentController.launchReset)
        {
            FreeCam();
        }

        else
        {
            ForwardCam();
        }
    }


    private void CameraSettings()
    {
        prevPlayerTarget = currentPlayerTarget;

        currentPlayerTarget = PlayerTurn.Instance.playerObjTurn.transform;

        playerNumber = ((int)PlayerTurn.Instance.playerObjTurn.GetComponent<PlayerIdentifier>().player + 1).ToString();

        if (currentPlayerTarget != prevPlayerTarget)
        {
            currentController = currentPlayerTarget.GetComponent<PlayerController>();

            freecamActive = currentController.freecamActive;
        }


        //freecamActive = currentController.freecamActive;

        if (freecamActive)
        {
            PlayerInput();
        }
    }

    private void PlayerInput()
    {
        currentX += Input.GetAxisRaw("P" + playerNumber + "_R_Horizontal") * xRotationSpeed;

        currentY += Input.GetAxisRaw("P" + playerNumber + "_R_Vertical") * yRotationSpeed;

        currentY = Mathf.Clamp(currentY, minY, maxY);
    }

    private void FreeCam()
    {
        Vector3 dir = new Vector3(0f, 0f, distance);

        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0f);

        transform.position = opponentTarget.position + rotation * dir;

        transform.LookAt(opponentTarget);
    }

    private void ForwardCam()
    {
        if (opponentTarget != currentPlayerTarget)
        {
            opponentTarget = currentPlayerTarget;

            opponentTargetNum = PlayerTurn.Instance.playerNumTurn;
        }

        currentX = transform.eulerAngles.y;

        currentY = transform.eulerAngles.x;

        Vector3 dir = new Vector3(0f, 0f, distance);

        Quaternion rotation = Quaternion.Euler(Mathf.SmoothDamp(transform.eulerAngles.x, 20f, ref velocity, 0.05f), currentPlayerTarget.eulerAngles.y, 0f);

        transform.position = currentPlayerTarget.position + rotation * dir;

        transform.LookAt(currentPlayerTarget);
    }

    public void GetLeftOpponent()
    {
        opponentTarget = PlayerTurn.Instance.players[opponentTargetNum - 1 < 0 ? PlayerTurn.Instance.playerAmount - 1 : opponentTargetNum - 1].transform;
        opponentTargetNum = opponentTargetNum - 1 < 0 ? PlayerTurn.Instance.playerAmount - 1 : opponentTargetNum - 1;
        currentX = opponentTarget.transform.eulerAngles.y;
    }

    public void GetRightOpponent()
    {
        opponentTarget = PlayerTurn.Instance.players[opponentTargetNum + 1 > PlayerTurn.Instance.playerAmount - 1 ? 0 : opponentTargetNum + 1].transform;
        opponentTargetNum = opponentTargetNum + 1 > PlayerTurn.Instance.playerAmount - 1 ? 0 : opponentTargetNum + 1;
        currentX = opponentTarget.transform.eulerAngles.y;
    }
}
