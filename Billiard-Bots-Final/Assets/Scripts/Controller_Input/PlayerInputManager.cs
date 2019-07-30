using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class PlayerInputManager : MonoBehaviour
{
    public Dictionary<int, ControllerInputs> players = new Dictionary<int, ControllerInputs>(); //Dictionary for 4 player input

    public ControllerInputs CurrentPlayer
    {
        get;
        protected set;
    }

    public static PlayerInputManager Instance
    {
        get
        {
            return s_Instance;
        }
    }

    protected static PlayerInputManager s_Instance;



    GamePadState playerOne; //Player 1's current controller state
    GamePadState playerTwo; //Player 2's current controller state
    GamePadState playerThree; //Player 3's current controller state
    GamePadState playerFour; //Player 4's current controller state

    GamePadState prevplayerOne; //Player 1's controller state 1 frame before
    GamePadState prevplayerTwo; //Player 2's controller state 1 frame before
    GamePadState prevplayerThree; //Player 3's controller state 1 frame before
    GamePadState prevplayerFour; //Player 4's controller state 1 frame before


    public int ControllersConnected; //Number of usable controllers currently connected

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

        ControllerStates();
        ConnectedControllers();
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 1; i <= 4; i++)
        {
            players[i] = new ControllerInputs();
        }

        ControllerStates();

        Debug.Log("Player 1" + (playerOne.IsConnected ? " is " : " isn't ") + "connected");
        Debug.Log("Player 2" + (playerTwo.IsConnected ? " is " : " isn't ") + "connected");
        Debug.Log("Player 3" + (playerThree.IsConnected ? " is " : " isn't ") + "connected");
        Debug.Log("Player 4" + (playerFour.IsConnected ? " is " : " isn't ") + "connected");
    }

    // Update is called once per frame
    void Update()
    {
        ControllerStates();

        PlayerInputs(1, playerOne, prevplayerOne);
        PlayerInputs(2, playerTwo, prevplayerTwo);
        PlayerInputs(3, playerThree, prevplayerThree);
        PlayerInputs(4, playerFour, prevplayerFour);
    }

    void ConnectedControllers()
    {
        ControllersConnected = 0;
        for (int i = 0; i < 4; ++i)
        {
            PlayerIndex testPlayerIndex = (PlayerIndex)i;
            GamePadState testState = GamePad.GetState(testPlayerIndex);
            if (testState.IsConnected)
            {
                ControllersConnected += 1;
            }
        }
    }

    void ControllerStates()
    {
        prevplayerOne = playerOne;
        prevplayerTwo = playerTwo;
        prevplayerThree = playerThree;
        prevplayerFour = playerFour;

        playerOne = GamePad.GetState(PlayerIndex.One);
        playerTwo = GamePad.GetState(PlayerIndex.Two);
        playerThree = GamePad.GetState(PlayerIndex.Three);
        playerFour = GamePad.GetState(PlayerIndex.Four);
    }

    void PlayerInputs(int i, GamePadState player, GamePadState prevPlayer)
    {
        players[i].Movement = new Vector2(player.ThumbSticks.Left.X, player.ThumbSticks.Left.Y);
        players[i].Look = new Vector2(player.ThumbSticks.Right.X, player.ThumbSticks.Right.Y);
        players[i].L_Trigger = player.Triggers.Left;
        players[i].R_Trigger = player.Triggers.Right;
        players[i].A_Button = (player.Buttons.A == ButtonState.Pressed && prevPlayer.Buttons.A == ButtonState.Released);
        players[i].B_Button = (player.Buttons.B == ButtonState.Pressed && prevPlayer.Buttons.B == ButtonState.Released);
        players[i].X_Button = (player.Buttons.X == ButtonState.Pressed && prevPlayer.Buttons.X == ButtonState.Released);
        players[i].Y_Button = (player.Buttons.Y == ButtonState.Pressed && prevPlayer.Buttons.Y == ButtonState.Released);
        players[i].L_Bumper = (player.Buttons.LeftShoulder == ButtonState.Pressed && prevPlayer.Buttons.LeftShoulder == ButtonState.Released);
        players[i].R_Bumper = (player.Buttons.RightShoulder == ButtonState.Pressed && prevPlayer.Buttons.RightShoulder == ButtonState.Released);
        players[i].Start_Button = (player.Buttons.Start == ButtonState.Pressed && prevPlayer.Buttons.Start == ButtonState.Released);
        players[i].Back_Button = (player.Buttons.Back == ButtonState.Pressed && prevPlayer.Buttons.Back == ButtonState.Released);
        players[i].L_Stick = (player.Buttons.LeftStick == ButtonState.Pressed && prevPlayer.Buttons.LeftStick == ButtonState.Released);
    }
}

public class ControllerInputs
{
    public Vector2 Movement;
    public Vector2 Look;
    public float L_Trigger;
    public float R_Trigger;
    public bool A_Button;
    public bool B_Button;
    public bool X_Button;
    public bool Y_Button;
    public bool L_Bumper;
    public bool R_Bumper;
    public bool Start_Button;
    public bool Back_Button;
    public bool L_Stick;
}
