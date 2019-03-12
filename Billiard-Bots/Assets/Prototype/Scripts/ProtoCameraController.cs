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

    public Transform currentPlayerTarget;

    public float xRotationSpeed;
    public float yRotationSpeed;

    void Start()
    {
        currentY = 20f;
    }
        
    void Update()
    {
        currentX += Input.GetAxisRaw("P1_R_Horizontal") * xRotationSpeed;

        currentY += Input.GetAxisRaw("P1_R_Vertical") * yRotationSpeed;

        currentY = Mathf.Clamp(currentY, minY, maxY);

    }

    void LateUpdate()
    {
        Vector3 dir = new Vector3(0f, 0f, distance);

        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0f);

        transform.position = currentPlayerTarget.position + rotation * dir;

        transform.LookAt(currentPlayerTarget);
    }
}
