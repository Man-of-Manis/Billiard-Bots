using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public Dictionary<int, InputManager> players = new Dictionary<int, InputManager>();

    public InputManager CurrentPlayer
    {
        get;
        protected set;
    }

    public static PlayerInput Instance
    {
        get { return s_Instance; }
    }

    protected static PlayerInput s_Instance;

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
        for (int i = 1; i <= 4; i++)
        {
            players[i] = new InputManager();
        }
    }

    void Update()
    {
        SetPlayerInputs();
        CurrentPlayerInputs();
    }

    void SetPlayerInputs()
    {
        for (int i = 1; i <= 4; i++)
        {
            players[i].leftStick.x = PlayerInputManager.Instance.players[i].Movement.x;
            players[i].leftStick.y = PlayerInputManager.Instance.players[i].Movement.y;
            players[i].rightStick.x = PlayerInputManager.Instance.players[i].Look.x;
            players[i].rightStick.y = PlayerInputManager.Instance.players[i].Look.y;
            players[i].lTrigger = PlayerInputManager.Instance.players[i].L_Trigger;
            players[i].rTrigger = PlayerInputManager.Instance.players[i].R_Trigger;
            players[i].aButton = PlayerInputManager.Instance.players[i].A_Button;
            players[i].bButton = PlayerInputManager.Instance.players[i].B_Button;
            players[i].xButton = PlayerInputManager.Instance.players[i].X_Button;
            players[i].yButton = PlayerInputManager.Instance.players[i].Y_Button;
            players[i].lBumper = PlayerInputManager.Instance.players[i].L_Bumper;
            players[i].rBumper = PlayerInputManager.Instance.players[i].R_Bumper;
            players[i].backButton = PlayerInputManager.Instance.players[i].Back_Button;
            players[i].startButton = PlayerInputManager.Instance.players[i].Start_Button;
        }
    }

    void CurrentPlayerInputs()
    {
        if(PlayerTurn.Instance != null)
        {
            if(PlayerTurn.Instance.playerObjTurn != null)
            {
                CurrentPlayer = players[(int)PlayerTurn.Instance.playerObjTurn.GetComponent<PlayerIdentifier>().player + 1];
            }            
        }        
    }
}



public class InputManager
{
    public Vector2 leftStick;

    public Vector2 rightStick;

    public float lTrigger;

    public float rTrigger;

    public bool aButton;

    public bool bButton;

    public bool xButton;

    public bool yButton;

    public bool lBumper;

    public bool rBumper;

    public bool startButton;

    public bool backButton;
}
