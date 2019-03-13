using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtoCameraController : MonoBehaviour
{
    public string playerNumber;

    private const float minY = 0f;
    private const float maxY = 85f;
    private const float distance = -8f;

    private float currentX;
    private float currentY;

    private float velocity = 0f;

    private bool freecamActive;
    private bool prevFreecamActive;

    public Transform currentPlayerTarget;

    private Transform prevPlayerTartget;

    private PlayerController currentController;

    public float xRotationSpeed;
    public float yRotationSpeed;

    void Start()
    {
        currentY = 20f;
        currentController = currentPlayerTarget.GetComponent<PlayerController>();
    }
        
    void Update()
    {
        prevPlayerTartget = currentPlayerTarget;

        if(currentPlayerTarget != prevPlayerTartget)
        {
            currentController = currentPlayerTarget.GetComponent<PlayerController>();
        }


        freecamActive = currentController.freecamActive;

        if (freecamActive)
        {
            currentX += Input.GetAxisRaw("P" + playerNumber + "_R_Horizontal") * xRotationSpeed;

            currentY += Input.GetAxisRaw("P" + playerNumber + "_R_Vertical") * yRotationSpeed;

            currentY = Mathf.Clamp(currentY, minY, maxY);
        }
    }

    void LateUpdate()
    {
        if(freecamActive || currentController.launchReset)
        {
            Vector3 dir = new Vector3(0f, 0f, distance);

            Quaternion rotation = Quaternion.Euler(currentY, currentX, 0f);

            transform.position = currentPlayerTarget.position + rotation * dir;

            transform.LookAt(currentPlayerTarget);
        }

        else
        {
            currentX = transform.eulerAngles.y;

            currentY = transform.eulerAngles.x;

            Vector3 dir = new Vector3(0f, 0f, distance);

            Quaternion rotation = Quaternion.Euler(Mathf.SmoothDamp(transform.eulerAngles.x, 20f, ref velocity, 0.05f), currentPlayerTarget.eulerAngles.y, 0f);

            transform.position = currentPlayerTarget.position + rotation * dir;

            transform.LookAt(currentPlayerTarget);
        }
    }
}
