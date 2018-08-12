using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainAnimation : MonoBehaviour {
    public float smoothDampTime = 0.2f;

    Vector3 smoothDampVelocity;
    Vector3 destination;

    Vector3 deviation = Vector3.zero;
    float timer = 0;
    public float deviationChangeTimer = 4f;
    public float deviationSize = 2f;
    Vector3 StartPosition;
    private void Awake()
    {
        StartPosition = transform.position;
        timer = deviationChangeTimer;
    }

    private void Update()
    {
        destination = StartPosition +
            deviation;

        GenerateDeviation();
    }

    private void GenerateDeviation()
    {
        timer += Time.deltaTime;
        if (timer < deviationChangeTimer) return;
        timer = 0;

        deviation = new Vector3(0, Random.value - 1, 0) * deviationSize;
    }

    void LateUpdate()
    {
        transform.position =
            Vector3.SmoothDamp(transform.position, destination, ref smoothDampVelocity, smoothDampTime);
    }
}
