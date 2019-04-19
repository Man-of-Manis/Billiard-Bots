using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenuNavigator : MonoBehaviour
{
    [Header("Menu GameObjects")]
    public GameObject mainMenu;
    public GameObject mapSelection;
    public GameObject numberOfPlayers;
    public GameObject characterSelect;
    public GameObject characterSelectReady;
    public GameObject exitGame;
    
    [Header("Selected Map String")]
    [SerializeField] private string map;

    [Header("Player Character UI")]
    public GameObject player1;
    public GameObject player2;
    public GameObject player3;
    public GameObject player4;

    //[Header("Selected Player Characters")]
    private GameObject character1;
    private GameObject character2;
    private GameObject character3;
    private GameObject character4;

    [Header("Bot Renderers")]
    public Transform p1Renderer;
    public Transform p2Renderer;
    public Transform p3Renderer;
    public Transform p4Renderer;

    private bool cancel;
    private bool start;

    private int playerAmount;

    public List<bool> playerReady;


    // Start is called before the first frame update
    void Start()
    {
        mainMenu.SetActive(true);
        mapSelection.SetActive(false);
        numberOfPlayers.SetActive(false);
        characterSelect.SetActive(false);
        exitGame.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        cancel = Input.GetButtonDown("Menu_Cancel");
        start = Input.GetButtonDown("Menu_Start");

        Navigator();
    }

    void ReadyCount(int amount)
    {
        playerReady.Clear();

        for(int i = 0; i < amount; i++)
        {
            playerReady.Insert(i,false);
        }        
    }

    bool AnyReady()
    {
        return !playerReady.Any(x => x);
    }

    bool AllReady()
    {
        return playerReady.All(x => x);
    }

    private void Navigator()
    {
        if (cancel)
        {
            if (exitGame.activeSelf)
            {
                mainMenu.SetActive(true);
                exitGame.SetActive(false);
            }

            else if (characterSelect.activeSelf && AnyReady())
            {
                numberOfPlayers.SetActive(true);
                characterSelect.SetActive(false);
            }

            else if (numberOfPlayers.activeSelf)
            {
                mapSelection.SetActive(true);
                numberOfPlayers.SetActive(false);
            }

            else if (mapSelection.activeSelf)
            {
                mainMenu.SetActive(true);
                mapSelection.SetActive(false);
            }

            else if (mainMenu.activeSelf)
            {
                exitGame.SetActive(true);
                mainMenu.SetActive(false);
            }
        }

        if(characterSelect.activeSelf)
        {
            characterSelectReady.SetActive(AllReady() ? true : false);
        }        

        if(start)
        {
            if(AllReady())
            {
                LoadLevel();
            }
        }
    }

    public void MapSelection(string mapName)
    {
        map = mapName;
    }

    public void PlayerCount(int number)
    {
        ReadyCount(number);
        CharacterLockIn.Instance.PlayerCount(number);

        switch(number)
        {
            case 1:
                {
                    player1.SetActive(true);
                    player2.SetActive(false);
                    player3.SetActive(false);
                    player4.SetActive(false);

                    player1.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, -125f);
                }
                break;
            case 2:
                {
                    player1.SetActive(true);
                    player2.SetActive(true);
                    player3.SetActive(false);
                    player4.SetActive(false);

                    player1.GetComponent<RectTransform>().anchoredPosition = new Vector2(-200f, -125f);
                    player2.GetComponent<RectTransform>().anchoredPosition = new Vector2(200f, -125f);
                }
                break;
            case 3:
                {
                    player1.SetActive(true);
                    player2.SetActive(true);
                    player3.SetActive(true);
                    player4.SetActive(false);

                    player1.GetComponent<RectTransform>().anchoredPosition = new Vector2(-400f, -125f);
                    player2.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, -125f);
                    player3.GetComponent<RectTransform>().anchoredPosition = new Vector2(400f, -125f);
                }
                break;
            case 4:
                {
                    player1.SetActive(true);
                    player2.SetActive(true);
                    player3.SetActive(true);
                    player4.SetActive(true);

                    player1.GetComponent<RectTransform>().anchoredPosition = new Vector2(-600f, -125f);
                    player2.GetComponent<RectTransform>().anchoredPosition = new Vector2(-200f, -125f);
                    player3.GetComponent<RectTransform>().anchoredPosition = new Vector2(200f, -125f);
                    player4.GetComponent<RectTransform>().anchoredPosition = new Vector2(600f, -125f);
                }
                break;
        }
    }

    public void CharacterSelect(int player, GameObject chracter)
    {
        switch (player)
        {
            case 1:
                {
                    character1 = chracter;
                }
                break;
            case 2:
                {
                    character2 = chracter;
                }
                break;
            case 3:
                {
                    character3 = chracter;
                }
                break;
            case 4:
                {
                    character4 = chracter;
                }
                break;
        }
    }

    public void LoadLevel()
    {
        if(map != null)
        {
            SceneManager.LoadScene(map);
        }

        else
        {
            SceneManager.LoadScene("Prototype_Scene");
        }
    }

    public void Quit()
    {
        Application.Quit();
    }
}
