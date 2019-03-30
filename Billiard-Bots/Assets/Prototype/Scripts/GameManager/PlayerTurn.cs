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
        GameObject[] p = GameObject.FindGameObjectsWithTag("Player");

        for (int i = 0; i < p.Length; i++)
        {    
            for(int j = 0; j < p.Length; j++)
            {
                if(p[j].name.Equals("Player" + (i + 1).ToString()))
                {
                    players.Insert(i, p[j]);
                    turns.Add(p[i], 0);
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
        if(PlayerInput.Instance.players["player1"].startButton)
        //if(pInput.players["player1"].startButton)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void EndTurn()
    {
        if(playerAmount <= 1)
        {
            //Debug.Log("Game Over!");
            gameOverText.text = "Game Over\n\n" + players[0].name + " Wins!";
            gameOver.SetActive(true);
            playerObjTurn.GetComponent<PlayerController>().turnEnabled = false;
            return;
        }
        else
        {

            playerObjTurn.GetComponent<PlayerController>().turnEnabled = false;
            turns[playerObjTurn] += 1;
            playerObjTurn = players[playerNumTurn + 1 <= playerAmount - 1 ? playerNumTurn + 1 : 0];
            playerNumTurn = playerNumTurn + 1 <= playerAmount - 1 ? playerNumTurn + 1 : 0;
            NextTurn();
        }
        
    }

    private void NextTurn()
    {
        //Enable next player
        playerObjTurn.GetComponent<PlayerController>().turnEnabled = true;
    }

    public void PlayerDestroyed(GameObject player)
    {
        players.Remove(player);
        turns.Remove(player);
        playerAmount = players.Count;
    }

    void SceneActivity()
    {
        prevSceneInactive = SceneInactive;
        SceneInactive = !playerMovement.Any(x => x) && ObjectsActivated == 0;

        if(prevSceneInactive != SceneInactive && SceneInactive && playerObjTurn.GetComponent<PlayerController>().UsedTurn)
        {
            inactivityTimer = 0;
            startTimer = true;
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
        for(int i = 0; i < playerAmount; i++)
        {
            playerMovement[i] = players[i].GetComponent<Rigidbody>().velocity.magnitude > 0.001f;
        }        
    }

    public void ObjectActivated(bool value)
    {
        ObjectsActivated += value ? 1 : -1;
    }
}
