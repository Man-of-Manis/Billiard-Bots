using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerTurnTimer : MonoBehaviour
{
    public float turnDuration = 30f;

    public float timer;

    public bool startTimer;

    private bool prevStartTimer;

    public bool startNext;

    public TMP_Text timerText;

    public static PlayerTurnTimer Instance
    {
        get { return s_Instance; }
    }

    protected static PlayerTurnTimer s_Instance;

    private float turnTimerDuration = 30f;

    private float turnTimer = 0f;

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

    // Start is called before the first frame update
    void Start()
    {
        timer = turnDuration;
        timerText.text = Mathf.CeilToInt(timer).ToString();
    }

    // Update is called once per frame
    void Update()
    {     
        if (startTimer)
        {
            CurrentTimer();
            UpdateText();
            prevStartTimer = startTimer;
        }

        else if(prevStartTimer != startTimer)
        {
            timer = turnDuration;
            prevStartTimer = startTimer;
        }
    }

    void CurrentTimer()
    {
        timer -= Time.deltaTime;

        if(timer <= 0f)
        {
            OutOfTime();
        }
    }

    void TurnTimer()
    {
        turnTimer += Time.deltaTime;

        if (turnTimer >= turnTimerDuration)
        {
            //OutOfTime();
        }
    }

    void OutOfTime()
    {
        startNext = true;
        startTimer = false;
        timer = turnDuration;
        FindObjectOfType<PlayerTurnInput>().HideTags();
        PlayerTurn.Instance.playerObjTurn.GetComponent<PlayerController>().TimeUp();
    }

    public void UpdateText()
    {
        timerText.text = Mathf.CeilToInt(timer).ToString();
    }

    public void RestartTimer()
    {
        startTimer = true;
    }

    public void StopTimer()
    {
        startTimer = false;
        timerText.text = "0";
    }
}
