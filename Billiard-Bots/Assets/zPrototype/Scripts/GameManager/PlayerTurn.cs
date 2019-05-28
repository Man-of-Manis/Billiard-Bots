using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerTurn : MonoBehaviour
{
    public static PlayerTurn Instance
    {
        get { return s_Instance; }
    }

    protected static PlayerTurn s_Instance;

    public PlayerInput pInput;

    public List<GameObject> players;

    public List<bool> playerMovement;

    public Dictionary<GameObject, int> turns = new Dictionary<GameObject, int>();

    public GameObject playerObjTurn;

    public int playerNumTurn = 0;

    public int playerAmount = 0;

    public int ObjectsActivated= 0;

    public float inactivityTimer = 0;

    public bool startTimer;

    public bool SceneInactive = true;

    public bool prevSceneInactive = true;

    public GameObject gameOver;

    public TMP_Text gameOverText;

    public int totalTurns = 0;

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

        //DontDestroyOnLoad(this);

        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        PlayerIdentifier[] p = FindObjectsOfType<PlayerIdentifier>();

        for (int i = 0; i < p.Length; i++)
        {    
            for(int j = 0; j < p.Length; j++)
            {
                if((int)(p[j].player) == i)
                {
                    players.Insert(i, p[j].gameObject);
                    turns.Add(p[i].gameObject, 0);
                }
            }
        }

        playerAmount = players.Count;

        for (int i = 0; i < playerAmount; i++)
        {
            playerMovement.Insert(i, players[i].GetComponent<Rigidbody>().velocity.magnitude > 0.1f);
        }

        playerObjTurn = players[playerNumTurn];

        playerObjTurn.GetComponent<PlayerController>().turnEnabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
        SceneActivity();
        SceneActivityTimer();
        Restart();
    }

    void Restart()
    {
        if(PlayerInput.Instance.players[1].backButton)
        //if(pInput.players["player1"].startButton)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
    }

    public void EndTurn()
    {
        if(playerAmount == 0)
        {
            gameOverText.text = "Game Over\n\n" + "All players have been destroyed!";
            gameOver.SetActive(true);
            playerObjTurn.GetComponent<PlayerController>().turnEnabled = false;
            return;
        }

        else if(playerAmount == 1)
        {
            //Debug.Log("Game Over!");
            gameOverText.text = "Game Over\n\n" + players[0].GetComponent<PlayerIdentifier>().player.ToString() + " Wins!";
            gameOver.SetActive(true);
            playerObjTurn.GetComponent<PlayerController>().turnEnabled = false;
            return;
        }

        else
        {
            if(playerObjTurn.GetComponent<PlayerHealth>().CurrentHealth > 0)
            {
                playerObjTurn.GetComponent<PlayerController>().turnEnabled = false;
                turns[playerObjTurn] += 1;
                playerObjTurn = players[playerNumTurn + 1 <= playerAmount - 1 ? playerNumTurn + 1 : 0];
                playerNumTurn = playerNumTurn + 1 <= playerAmount - 1 ? playerNumTurn + 1 : 0;
                NextTurn();
            }

            else
            {
                playerObjTurn = players[playerNumTurn];
                NextTurn();
            }
        }
        
    }

    private void NextTurn()
    {
        totalTurns++;
        PlayerUI ui = FindObjectOfType<PlayerUI>();
        ui.JoystickAnim(false);
        //Enable next player
        playerObjTurn.GetComponent<PlayerController>().turnEnabled = true;
        PlayerTurnTimer.Instance.UpdateText();
    }

    public void PlayerDestroyed(GameObject player)
    {
        
        int index = players.IndexOf(player);
        playerMovement.RemoveAt(index);
        players.Remove(player);
        turns.Remove(player);
        playerAmount = players.Count;

        for(int i = 0; i < playerAmount - 1; i++)
        {
            if(players[i] == null)
            {
                players.Insert(i, players[i + 1]);
                players.RemoveAt(i + 1);
            }
        }
    }

    void SceneActivity()
    {
        prevSceneInactive = SceneInactive;
        SceneInactive = (!playerMovement.Any(x => x) && ObjectsActivated == 0);

        if ((prevSceneInactive != SceneInactive && SceneInactive && playerObjTurn.GetComponent<PlayerController>().UsedTurn) || PlayerTurnTimer.Instance.startNext)
        {
            inactivityTimer = 0;
            startTimer = true;
            PlayerTurnTimer.Instance.startNext = false;
        }

        if(SceneInactive && playerObjTurn.GetComponent<PlayerController>().UsedTurn && inactivityTimer >= 1f)
        {
            Debug.Log("Next player's turn");
            startTimer = false;
            EndTurn();
            inactivityTimer = 0f;
        }
    }

    void SceneActivityTimer()
    {
        if (startTimer)
        {
            inactivityTimer += Time.deltaTime;
        }
    }

    public void PlayerMovement()
    {
        if(playerAmount > 0)
        {
            for (int i = 0; i < playerAmount; i++)
            {
                playerMovement[i] = players[i].GetComponent<Rigidbody>().velocity.magnitude > 0.001f;
            }
        }

        else
        {
            playerMovement.Clear();
        }
    }

    public void ObjectActivated(bool value)
    {
        ObjectsActivated += value ? 1 : -1;
    }
}
