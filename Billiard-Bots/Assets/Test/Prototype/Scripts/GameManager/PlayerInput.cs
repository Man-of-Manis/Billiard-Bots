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
            players[i].leftStick.x = Input.GetAxisRaw("P" + i + "_L_Horizontal");
            players[i].leftStick.y = Input.GetAxisRaw("P" + i + "_L_Vertical");
            players[i].rightStick.x = Input.GetAxisRaw("P" + i + "_R_Horizontal");
            players[i].rightStick.y = Input.GetAxisRaw("P" + i + "_R_Vertical");
            players[i].aButton = Input.GetButtonDown("P" + i + "_A_Button");
            players[i].bButton = Input.GetButtonDown("P" + i + "_B_Button");
            players[i].xButton = Input.GetButtonDown("P" + i + "_X_Button");
            players[i].lBumper = Input.GetButtonDown("P" + i + "_L_Bumper");
            players[i].rBumper = Input.GetButtonDown("P" + i + "_R_Bumper");
            players[i].backButton = Input.GetButtonDown("P" + i + "_Back_Button");
            players[i].startButton = Input.GetButtonDown("P" + i + "_Start_Button");
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

    public bool aButton;

    public bool bButton;

    public bool xButton;

    public bool lBumper;

    public bool rBumper;

    public bool startButton;

    public bool backButton;
}
