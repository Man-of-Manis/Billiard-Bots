using System.Collections;
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

    public GameObject playerObjTurn;

    public int playerNumTurn = 0;

    public int playerAmount = 0;

    public int PlayersMoving= 0;

    public int ObjectsActivated= 0;

    public bool SceneInactive;

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

        DontDestroyOnLoad(this);

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

        playerObjTurn = players[playerNumTurn];

        playerObjTurn.GetComponent<PlayerController>().turnEnabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        SceneActivity();
        Restart();
    }

    void Restart()
    {
        if(Input.GetButtonDown("P1_Start_Button"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void EndTurn(string name)
    {
        if(playerAmount <= 1)
        {
            Debug.Log("Game Over!");
            playerObjTurn.GetComponent<PlayerController>().turnEnabled = false;
            return;
        }
        else
        {
            if (name.Equals(playerObjTurn.name))
            {
                playerObjTurn.GetComponent<PlayerController>().turnEnabled = false;
                playerObjTurn = players[playerNumTurn + 1 <= playerAmount - 1 ? playerNumTurn + 1 : 0];
                playerNumTurn = playerNumTurn + 1 <= playerAmount - 1 ? playerNumTurn + 1 : 0;
                NextTurn();
            }
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
        SceneInactive = PlayersMoving == 0 && ObjectsActivated == 0;
    }

    public void PlayerMovement(bool value)
    {
        PlayersMoving += value ? 1 : -1;
    }

    public void ObjectActivated(bool value)
    {
        ObjectsActivated += value ? 1 : -1;
    }
}
