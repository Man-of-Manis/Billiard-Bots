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

    private RectTransform playerTag;

    private RectTransform canvas;

    private Camera cam;

    private void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        playerTag = GetComponent<RectTransform>();

        canvas = GameObject.Find("Player_UI").GetComponent<RectTransform>();

        Hide();
    }

    // Start is called before the first frame update
    void Update()
    {
        Position();
    }

    void Position()
    {
        Vector3 ViewportPosition = cam.WorldToViewportPoint(player.transform.position);

        if (ViewportPosition.z < 0)
        {
            ViewportPosition *= -1;
        }

        Vector2 WorldObject_ScreenPosition = new Vector2(
        ((ViewportPosition.x * canvas.sizeDelta.x) - (canvas.sizeDelta.x * 0.5f)),
        ((ViewportPosition.y * canvas.sizeDelta.y) - (canvas.sizeDelta.y * 0.5f)));

        WorldObject_ScreenPosition += new Vector2(0f, 1.5f);

        playerTag.anchoredPosition = WorldObject_ScreenPosition;
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
