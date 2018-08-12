using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class Wagon: MonoBehaviour
{
    [SerializeField] private float dpsGoal = 5;

    private readonly DpsCounter dpsCounter = new DpsCounter();
    private List<Wall> walls = new List<Wall>();

    public event Action DpsGoalReached;
    public event Action<Vector3> ItemSpawnThresholdReached;

    private void Awake()
    {
        walls = GetComponentsInChildren<Wall>().ToList();
        foreach (var wall in walls)
        {
            wall.DamageReceived += Wall_DamageReceived;
        }
    }

    private void Wall_DamageReceived(float damage, Vector3 position)
    {
        dpsCounter.Register(damage);
        if (Dps >= dpsGoal)
        {
            if (DpsGoalReached != null)
                DpsGoalReached();
        }

        if (Random.value > 0.8f)
        {
            if (ItemSpawnThresholdReached != null)
                ItemSpawnThresholdReached(position);
        }
    }

    public float Dps
    {
        get { return dpsCounter.Dps; }
    }

    public float DpsGoal
    {
        get { return dpsGoal; }
    }
}