using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class SkipCutscene : MonoBehaviour
{
    public PlayableDirector playable;

    public GameObject yButton;

    private bool activate = false;

    Coroutine co;

    // Update is called once per frame
    void Update()
    {
        if(playable.time <= 18)
        {
            if (PlayerInput.Instance.players[1].yButton && activate)
            {
                playable.time = 18;
                yButton.SetActive(false);
                StopCoroutine(co);
            }

            else if (PlayerInput.Instance.players[1].yButton)
            {
                activate = true;
                yButton.SetActive(true);
                co = StartCoroutine(DeactivateButton());
            }
        }

        else if(playable.time > 18)
        {
            yButton.SetActive(false);
        }

        else if(playable.time >= 23)
        { 
            Destroy(gameObject);
        }
    }

    IEnumerator DeactivateButton()
    {
        yield return new WaitForSeconds(3.5f);
        activate = false;
        yButton.SetActive(false);
    }
}
