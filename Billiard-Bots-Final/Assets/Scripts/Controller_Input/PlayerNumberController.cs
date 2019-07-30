using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerNumberController : MonoBehaviour
{
    public Button p1;
    public Button p2;
    public Button p3;
    public Button p4;

    int position = 0;

    public float inputActionsPerSecond = 4f;

    private float nextAction;

    // Start is called before the first frame update
    void OnEnable()
    {
        position = 0;
        p1.OnSelect(null);
    }

    // Update is called once per frame
    void Update()
    {
        if(position == 0)
        {
            if (PlayerInputManager.Instance.players[1].Movement.x > 0.25f && AllowAction())
            {
                position = 1;
                p2.OnSelect(null);
                p1.OnDeselect(null);
                ActionMade();
            }

            else if (PlayerInputManager.Instance.players[1].A_Button)
            {
                p1.onClick.Invoke();
            }
        }

        else if (position == 1)
        {
            if (PlayerInputManager.Instance.players[1].Movement.x > 0.25f && AllowAction())
            {
                position = 2;
                p3.OnSelect(null);
                p2.OnDeselect(null);
                ActionMade();
            }

            else if (PlayerInputManager.Instance.players[1].Movement.x < -0.25f && AllowAction())
            {
                position = 0;
                p1.OnSelect(null);
                p2.OnDeselect(null);
                ActionMade();
            }

            else if (PlayerInputManager.Instance.players[1].A_Button)
            {
                p2.onClick.Invoke();
            }
        }

        else if (position == 2)
        {
            if (PlayerInputManager.Instance.players[1].Movement.x > 0.25f && AllowAction())
            {
                position = 3;
                p4.OnSelect(null);
                p3.OnDeselect(null);
                ActionMade();
            }

            else if (PlayerInputManager.Instance.players[1].Movement.x < -0.25f && AllowAction())
            {
                position = 1;
                p2.OnSelect(null);
                p3.OnDeselect(null);
                ActionMade();
            }

            else if (PlayerInputManager.Instance.players[1].A_Button)
            {
                p3.onClick.Invoke();
            }
        }

        else if (position == 3)
        {
            if (PlayerInputManager.Instance.players[1].Movement.x < -0.25f && AllowAction())
            {
                position = 2;
                p3.OnSelect(null);
                p4.OnDeselect(null);
                ActionMade();
            }

            else if (PlayerInputManager.Instance.players[1].A_Button)
            {
                p4.onClick.Invoke();
            }
        }
    }


    bool AllowAction()
    {
        float time = Time.unscaledTime;

        if (time > nextAction)
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
