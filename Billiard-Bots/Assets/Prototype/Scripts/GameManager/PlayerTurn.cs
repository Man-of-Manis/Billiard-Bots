using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerTurn : MonoBehaviour
{
    public static PlayerTurn Instance
    {
        get { return s_Instance; }
    }

    protected static PlayerTurn s_Instance;


    public List<GameObject> players;

    public List<bool> playerMovement;

    public GameObject playerObjTurn;

    public int playerNumTurn = 0;

    public int playerAmount = 0;

    public int ObjectsActivated= 0;

    public float inactivityTimer = 0;

    public bool startTimer;

    public bool SceneInactive = true;

    public bool prevSceneInactive = true;

    void Awake()
    {
        if (s_Instance == null)
        {
            s_Instance = this;
        }
        else if (s_Instance != this)
        {
            //throw new UnityException("There cannot be more than one PlayerInput script.  The instances are " + s_Instance.name + " and " + name + ".");
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
        if(Input.GetButtonDown("P1_Start_Button"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void EndTurn()
    {
        if(playerAmount <= 1)
        {
            Debug.Log("Game Over!");
            playerObjTurn.GetComponent<PlayerController>().turnEnabled = false;
            return;
        }
        else
        {

            playerObjTurn.GetComponent<PlayerController>().turnEnabled = false;
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
