using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUITag : MonoBehaviour
{
    public TMP_Text tagName;

    public Transform player;

    public Image bg;

    private Vector3 playerPos;

    private Vector3 offset = new Vector3(0f, 1.5f, 0f);

    private void Start()
    {
        Hide();
    }

    // Start is called before the first frame update
    void Update()
    {
        Position();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        playerPos = player.position;
        
    }

    void Position()
    {
        Vector3 newPosition = ProtoCameraController.Instance.GetComponent<Camera>().WorldToScreenPoint(playerPos + offset);

        transform.position = newPosition;
    }

    public void SetTag(int num, Transform playerTrans)
    {
        player = playerTrans;

        string name = "Player " + (num + 1).ToString();
        tagName.text = name;
    }

    public void Hide()
    {
        bg.enabled = false;
        tagName.enabled = false;
    }

    public void Show()
    {
        bg.enabled = true;
        tagName.enabled = true;
    }
}
