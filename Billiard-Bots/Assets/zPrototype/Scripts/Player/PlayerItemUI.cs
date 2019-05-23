using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemUI : MonoBehaviour
{
    private PlayerItemBar itemBar;

    // Start is called before the first frame update
    void Start()
    {
        itemBar = FindObjectOfType<PlayerUI>().PlayerBar((int)GetComponent<PlayerIdentifier>().player);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
