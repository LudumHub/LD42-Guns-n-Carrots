using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovment : MonoBehaviour {
    public int Carrots {
        get { return carrots; }
        set { carrots = Mathf.Min(value, maxCarrotsAmount); }
    }
    int carrots = 0;

    public Transform LowestPosition;
    public Transform HighestPosition;

    public float smoothDampTime = 0.2f;
    public float predictDistance = 1f;

    Vector3 smoothDampVelocity;
    Vector3 destination;
    Vector3 carrotDistanceBoost;
    public int maxCarrotsAmount = 3;
    Vector3 deviation = Vector3.zero;
    float timer = 0;
    public float deviationChangeTimer = 2f;
    public float deviationSize = 2f;

    public void Awake()
    {
        carrotDistanceBoost = (HighestPosition.position - LowestPosition.position) / maxCarrotsAmount;
        destination = LowestPosition.position;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(destination, 0.1f);
    }

    private void Update()
    {
        destination = LowestPosition.position +
            carrotDistanceBoost * carrots +
            deviation;

        GenerateDeviation();
    }

    private void GenerateDeviation()
    {
        timer += Time.deltaTime;
        if (timer < deviationChangeTimer) return;
        timer = 0;

        deviation = new Vector3(Random.value, Random.value, 0) * deviationSize;
    }

    void LateUpdate()
    {
        transform.position = 
            Vector3.SmoothDamp(transform.position, destination, ref smoothDampVelocity, smoothDampTime);
    }
}
