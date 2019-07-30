using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapChoiceController : MonoBehaviour
{
    public Button map;

    // Start is called before the first frame update
    void OnEnable()
    {
        map.OnSelect(null);
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerInputManager.Instance.players[1].A_Button)
        {
            map.onClick.Invoke();
        }
    }
}
