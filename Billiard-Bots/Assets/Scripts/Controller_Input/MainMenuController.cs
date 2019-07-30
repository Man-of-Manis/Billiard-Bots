using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public Button Versus;
    public Button Exit;

    int position = 0;

    // Start is called before the first frame update
    private void OnEnable()
    {
        position = 0;
        Versus.OnSelect(null);
    }

    // Update is called once per frame
    void Update()
    {
        if(position == 0)
        {
            if(PlayerInputManager.Instance.players[1].Movement.y < -0.25f)
            {
                position = 1;
                Exit.OnSelect(null);
                Versus.OnDeselect(null);
            }

            else if(PlayerInputManager.Instance.players[1].A_Button)
            {
                Versus.onClick.Invoke();
            }
        }

        if(position == 1)
        {
            if (PlayerInputManager.Instance.players[1].Movement.y > 0.25f)
            {
                position = 0;
                Versus.OnSelect(null);
                Exit.OnDeselect(null);
            }

            else if (PlayerInputManager.Instance.players[1].A_Button)
            {
                Exit.onClick.Invoke();
            }
        }
    }
}
