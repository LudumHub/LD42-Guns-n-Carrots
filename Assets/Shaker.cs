using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shaker : MonoBehaviour {
    public float smoothDampTime = 0.2f;

    Vector3 smoothDampVelocity;
    Vector3 destination;

    public Vector3 deviation = Vector3.zero;
    float timer = 0;
    public float deviationChangeTimer = 4f;
    public float deviationSize = 2f;
    public Vector3 StartPosition;
    float ShakeTimer = 0f;
    private bool preserveTimer;

    public static Shaker instance;

    public void Shake(float seconds)
    {
        if (!preserveTimer)
            ShakeTimer = seconds;
    }

    public void ShakeContinuously(float seconds)
    {
        Shake(seconds);
        preserveTimer = true;
    }

    private void Awake()
    {
        StartPosition = transform.position;
        instance = this;
    }

    private void Update()
    {
        if (ShakeTimer < 0f)
        {
            preserveTimer = false;
            destination = StartPosition;
        }
        else
        {
            ShakeTimer -= Time.deltaTime;
            destination = StartPosition +
                deviation;

            GenerateDeviation();
        }
    }

    private void GenerateDeviation()
    {
        timer += Time.deltaTime;
        if (timer < deviationChangeTimer) return;
        timer = 0;

        deviation = new Vector3(Random.value - .5f, Random.value - .5f, 0) * deviationSize;
    }

    void LateUpdate()
    {
        transform.position =
            Vector3.SmoothDamp(transform.position, destination, ref smoothDampVelocity, smoothDampTime);
    }
}