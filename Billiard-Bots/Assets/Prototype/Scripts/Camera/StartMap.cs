using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMap : MonoBehaviour
{
    bool start;

    public Animator anim;
    public PlayerSpawn spawn;

    // Update is called once per frame
    void Update()
    {
        start = Input.GetButtonDown("Menu_Start");

        if(start)
        {
            Animation();
            StartGame();
        }
    }

    void Animation()
    {
        anim.SetTrigger("end");
    }

    public void StartGame()
    {
        spawn.StartGame();
    }
}
