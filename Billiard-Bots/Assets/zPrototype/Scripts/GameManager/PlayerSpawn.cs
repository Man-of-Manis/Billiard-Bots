using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    public List<Transform> spawnPoints;

    public bool randomSpawns;

    public GameObject UI;

    public GameObject tags;

    //[Header("Scripts")]
    //[SerializeField] private PlayerTurn turn;

    //[SerializeField] private PlayerTurnInput input;

    //[SerializeField] private ProtoCameraController camControl;

    //[SerializeField] private PlayerButtonsUI buttonUI;

    [Header("Health")]
    [SerializeField] private GameObject p1Health;

    [SerializeField] private GameObject p2Health;

    [SerializeField] private GameObject p3Health;

    [SerializeField] private GameObject p4Health;

    private void Awake()
    {
        if(CharacterLockIn.Instance.PlayerCharacters.Count != 0)
        {
            GetSpawnPoints();
            Spawn();
        }

        else
        {
            Debug.Log("Player character data wasn't loaded properly. Enter this scene through the Main Menu to load selected characters.");
            GetSpawnPoints();
            RandomCharacters();
        }
    }


    void RandomCharacters()
    {
        string[] names = Input.GetJoystickNames();

        if(names.Length != 0)
        {
            if (randomSpawns)
            {
                for (int i = 0; i < names.Length; i++)
                {
                    int randomSpawn = Random.Range(0, spawnPoints.Count);
                    GameObject player = Instantiate(CharacterLockIn.Instance.playerOptions[Random.Range(0, CharacterLockIn.Instance.playerOptions.Length)], spawnPoints[randomSpawn].position, spawnPoints[randomSpawn].rotation);
                    player.GetComponent<PlayerIdentifier>().player = (PlayerIdentifier.PlayerNumber)i;
                    spawnPoints.RemoveAt(randomSpawn);
                    SpawnTags(i, player.transform);
                }
                StartGame();
            }

            else
            {
                List<int> bots = new List<int>();

                for (int i = 0; i < names.Length; i++)
                {
                    bool needBot = true;

                    while(needBot)
                    {
                        int bot = Random.Range(0, CharacterLockIn.Instance.playerOptions.Length);

                        if(!bots.Contains(bot))
                        {
                            GameObject player = Instantiate(CharacterLockIn.Instance.playerOptions[bot], spawnPoints[i].position, spawnPoints[i].rotation);
                            player.GetComponent<PlayerIdentifier>().player = (PlayerIdentifier.PlayerNumber)i;
                            SpawnTags(i, player.transform);
                            bots.Add(bot);
                            needBot = false;
                        }
                    }
                }
                StartGame();
            }
        }

        else
        {
            Debug.Log("No controllers are connected. No characters were spawned.");
        }
    }

    void GetSpawnPoints()
    {
        GameObject[] spawns = GameObject.FindGameObjectsWithTag("SpawnPoint");

        if(spawns.Length != 0)
        {
            for(int i = 0; i < spawns.Length; i++)
            {
                spawnPoints.Add(spawns[i].transform);
            }
        }

        else
        {
            throw new System.Exception("There are no SpawnPoints in this scene. Please add some to play.");
        }
    }

    void Spawn()
    {
        if(randomSpawns)
        {
            for (int i = 0; i < CharacterLockIn.Instance.PlayerCharacters.Count; i++)
            {
                int randomSpawn = Random.Range(0, spawnPoints.Count);
                GameObject player = Instantiate(CharacterLockIn.Instance.PlayerCharacters[i], spawnPoints[randomSpawn].position, spawnPoints[randomSpawn].rotation);
                player.GetComponent<PlayerIdentifier>().player = (PlayerIdentifier.PlayerNumber)i;
                spawnPoints.RemoveAt(randomSpawn);
                SpawnTags(i, player.transform);
            }
            StartGame();
        }

        else
        {
            for(int i = 0; i < CharacterLockIn.Instance.PlayerCharacters.Count; i++)
            {
                GameObject player = Instantiate(CharacterLockIn.Instance.PlayerCharacters[i], spawnPoints[i].position, spawnPoints[i].rotation);
                player.GetComponent<PlayerIdentifier>().player = (PlayerIdentifier.PlayerNumber)i;
                SpawnTags(i, player.transform);
            }
            StartGame();
        }
    }

    private void Destroy()
    {
        Destroy(CharacterLockIn.Instance.gameObject);
    }

    void SetPlayers(int num)
    {
        p1Health.SetActive(true);
        p2Health.SetActive(num > 1);
        p3Health.SetActive(num > 2);
        p4Health.SetActive(num > 3);        
    }

    void SpawnTags(int num, Transform playerT)
    {
        GameObject tag = Instantiate(tags, UI.transform);
        tag.GetComponent<PlayerUITag>().SetTag(num, playerT);
    }

    public void StartGame()
    {
        //turn.enabled = true;
        //camControl.enabled = true;
        //buttonUI.enabled = true;
        //input.enabled = true;
        SetPlayers(FindObjectsOfType<PlayerIdentifier>().Length);

        Destroy();
    }
}
