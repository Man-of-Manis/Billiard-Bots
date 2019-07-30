using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TornadoMovement : MonoBehaviour
{
    private Vector3 Velocity = Vector3.zero;

    private Vector3 targetPos;

    public float smoothing;

    public float radiusClamp;

    private float timer = 0f;

    public float timeToReset = 5f;

    // Start is called before the first frame update
    void Start()
    {
        TargetPosition();
    }

    // Update is called once per frame
    void Update()
    {
        TargetTimer();

        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref Velocity, smoothing);
    }

    void TargetTimer()
    {
        timer += Time.deltaTime;

        if(timer >= timeToReset)
        {
            TargetPosition();
            timer = 0f;
        }
    }

    void TargetPosition()
    {
        targetPos = new Vector3(Random.Range(-radiusClamp, radiusClamp), 0f, Random.Range(-radiusClamp, radiusClamp));
    }
}
