using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtoCameraController : MonoBehaviour
{
    private const float minY = 0f;
    private const float maxY = 85f;
    private const float distance = -8f;

    private float currentX;
    private float currentY;

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

        prevFreecamActive = freecamActive;

        freecamActive = currentController.freecamActive;

        if (freecamActive)
        {
            if(prevFreecamActive != freecamActive)
            {
                currentX = transform.eulerAngles.y;
            }

            currentX += Input.GetAxisRaw("P1_R_Horizontal") * xRotationSpeed;

            currentY += Input.GetAxisRaw("P1_R_Vertical") * yRotationSpeed;

            currentY = Mathf.Clamp(currentY, minY, maxY);
        }
    }

    void LateUpdate()
    {
        if(freecamActive)
        {
            Vector3 dir = new Vector3(0f, 0f, distance);

            Quaternion rotation = Quaternion.Euler(currentY, currentX, 0f);

            transform.position = currentPlayerTarget.position + rotation * dir;

            transform.LookAt(currentPlayerTarget);
        }

        else
        {
            Vector3 dir = new Vector3(0f, 0f, distance);

            Quaternion rotation = Quaternion.Euler(20f, currentPlayerTarget.eulerAngles.y, 0f);

            transform.position = currentPlayerTarget.position + rotation * dir;

            transform.LookAt(currentPlayerTarget);
        }
    }
}
