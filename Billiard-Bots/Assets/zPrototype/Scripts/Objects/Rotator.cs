using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour {
	public float rotationSpeed = 60.0f;

	public float bobAmplitude = 0.125f;
	public float bobPeriod = 1.5f;

	public float wobbleAmplitude = 5.0f;
	public float wobblePeriod = 1.5f;

	private float initialYPos;

    public bool worldSpace = true;

	void Start() {
		initialYPos = worldSpace ? transform.position.y : transform.localPosition.y;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(worldSpace)
        {
            WorldSpace();
        }

        else
        {
            LocalSpace();
        }
    }

    private void WorldSpace()
    {
        float xEulerAngle = wobbleAmplitude * Mathf.Sin(Time.time * wobblePeriod);
        float zEulerAngle = wobbleAmplitude * Mathf.Cos(Time.time * wobblePeriod);

        transform.rotation = Quaternion.Euler(xEulerAngle, transform.eulerAngles.y + rotationSpeed * Time.deltaTime, zEulerAngle);

        float yOffset = bobAmplitude * Mathf.Sin(Time.time * bobPeriod);
        transform.position = new Vector3(transform.position.x, initialYPos + yOffset, transform.position.z);
    }

    private void LocalSpace()
    {
        float xEulerAngle = wobbleAmplitude * Mathf.Sin(Time.time * wobblePeriod);
        float zEulerAngle = wobbleAmplitude * Mathf.Cos(Time.time * wobblePeriod);

        transform.localRotation = Quaternion.Euler(xEulerAngle, transform.localEulerAngles.y + rotationSpeed * Time.deltaTime, zEulerAngle);

        float yOffset = bobAmplitude * Mathf.Sin(Time.time * bobPeriod);
        transform.localPosition = new Vector3(transform.localPosition.x, initialYPos + yOffset, transform.localPosition.z);
    }
}
