using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text;

public class CharacterSelection : MonoBehaviour
{
    public enum PlayerNumber { Player1, Player2, Player3, Player4};

    public PlayerNumber player;

    private string playerString;

    private int playerNum;

    public RectTransform botRenderer;

    public Color botColor;

    public Image botColorImg;

    public TextMeshProUGUI botNum;

    public int currentBotNum;

    public TMP_Text botName;

    public Image readyIMG;

    public MainMenuNavigator menu;

    public float inputActionsPerSecond = 4f;

    public bool moveLeft;

    public bool moveRight;

    public bool lBumper;

    public bool rBumper;

    public bool accept;

    public bool cancel;

    public bool ready;    

    private readonly float deadzone = 0.25f;

    public List<GameObject> characterList;

    public int currentSelection;

    private float nextAction;

    // Start is called before the first frame update
    void OnEnable()
    {
        ControllingPlayerNumber();

        GameObject[] list = (GameObject[]) FindObjectOfType<UsableCharacters>().characters.Clone();

        for(int i = 0; i < list.Length; i++)
        {
            //GameObject character = Instantiate(list[i], botRenderer.TransformPoint(botRenderer.anchoredPosition3D), Quaternion.identity);

            //character.transform.SetParent(botRenderer.transform);

            //GameObject character = Instantiate(list[i], botRenderer.position, Quaternion.identity, botRenderer);
            GameObject character = Instantiate(list[i], botRenderer.position, botRenderer.rotation, botRenderer);

            characterList.Add(character);

            if(i != 0)
            {
                character.SetActive(false);
            }
        }
        currentSelection = 0;

        //botName.text = FixedName(characterList[currentSelection].name);

        ready = false;
    }

    private void OnDisable()
    {
        for(int i = 0; i < characterList.Count; i++)
        {
            Destroy(characterList[i]);
        }

        characterList.Clear();
    }

    void Update()
    {
        menu.playerReady[playerNum] = ready;
        readyIMG.color = ready ? Color.green : Color.red;

        PlayerInput();
        SelectingCharacter();
    }

    string FixedName(string name)
    {
        string fixedName = name;
        int start = fixedName.IndexOf('(');
        StringBuilder sb = new StringBuilder(fixedName)
        {
            Length = start
        };
        return sb.ToString();
    }

    void ControllingPlayerNumber()
    {
        if (player.Equals(PlayerNumber.Player1))
        {
            playerString = "P1";
            playerNum = 0;
        }

        else if (player.Equals(PlayerNumber.Player2))
        {
            playerString = "P2";
            playerNum = 1;
        }

        else if (player.Equals(PlayerNumber.Player3))
        {
            playerString = "P3";
            playerNum = 2;
        }

        else if (player.Equals(PlayerNumber.Player4))
        {
            playerString = "P4";
            playerNum = 3;
        }
    }

    void SelectingCharacter()
    {
        if(moveLeft && AllowAction() && !ready)
        {
            int nextCharacter = currentSelection == 0 ? (characterList.Count - 1) : currentSelection - 1;
            characterList[currentSelection].SetActive(false);
            currentSelection = nextCharacter;
            characterList[currentSelection].SetActive(true);
            botName.text = FixedName(characterList[currentSelection].name);
            //botColor = characterList[currentSelection].GetComponent<MeshRenderer>().material.color;
            botColorImg.color = botColor;

            ActionMade();
        }

        if(moveRight && AllowAction() && !ready)
        {
            int nextCharacter = currentSelection == (characterList.Count - 1) ? 0 : currentSelection + 1;
            characterList[currentSelection].SetActive(false);
            currentSelection = nextCharacter;
            characterList[currentSelection].SetActive(true);
            botName.text = FixedName(characterList[currentSelection].name);
            //botColor = characterList[currentSelection].transform.Find("BilliardBot_Mesh").GetComponent<SkinnedMeshRenderer>().materials[0].color;
            botColorImg.color = botColor;

            ActionMade();
        }

        if (!ready && accept)
        {
            CharacterLockIn.Instance.PlayerChoice(currentSelection , playerNum);
            ready = true;
        }

        if (ready && cancel)
        {
            CharacterLockIn.Instance.PlayerCancel(playerNum);
            ready = false;
        }

        if(lBumper)
        {
            currentBotNum = currentBotNum > 1 ? currentBotNum-1 : 15;
            botNum.text = currentBotNum.ToString();
        }

        else if(rBumper)
        {
            currentBotNum = currentBotNum < 15 ? currentBotNum+1 : 1;
            botNum.text = currentBotNum.ToString();
        }
    }

    void PlayerInput()
    {
        moveLeft = Input.GetAxisRaw(playerString + "_L_Horizontal") < -deadzone;
        moveRight = Input.GetAxisRaw(playerString + "_L_Horizontal") > deadzone;
        lBumper = Input.GetButtonDown(playerString + "_L_Bumper");
        rBumper = Input.GetButtonDown(playerString + "_R_Bumper");
        accept = Input.GetButtonDown(playerString + "_A_Button");
        cancel = Input.GetButtonDown(playerString + "_B_Button");
    }

    bool AllowAction()
    {
        float time = Time.unscaledTime;

        if(time > nextAction)
        {
            return true;
        }

        return false;
    }

    void ActionMade()
    {
        float time = Time.unscaledTime;

        nextAction = time + 1f / inputActionsPerSecond;
    }
}
