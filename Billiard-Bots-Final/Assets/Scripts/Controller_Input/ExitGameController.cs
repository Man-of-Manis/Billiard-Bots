using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitGameController : MonoBehaviour
{
    public Button Yes;
    public Button No;

    int position = 0;

    // Start is called before the first frame update
    void OnEnable()
    {
        position = 0;
        Yes.OnSelect(null);
    }

    // Update is called once per frame
    void Update()
    {
        if (position == 0)
        {
            if (PlayerInputManager.Instance.players[1].Movement.x > 0.25f)
            {
                position = 1;
                No.OnSelect(null);
                Yes.OnDeselect(null);
            }

            else if (PlayerInputManager.Instance.players[1].A_Button)
            {
                Yes.onClick.Invoke();
            }
        }

        if (position == 1)
        {
            if (PlayerInputManager.Instance.players[1].Movement.x < -0.25f)
            {
                position = 0;
                Yes.OnSelect(null);
                No.OnDeselect(null);
            }

            else if (PlayerInputManager.Instance.players[1].A_Button)
            {
                No.onClick.Invoke();
            }
        }
    }
}
