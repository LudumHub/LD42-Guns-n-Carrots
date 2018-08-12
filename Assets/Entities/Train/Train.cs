using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Train : MonoBehaviour
{
    [SerializeField] private ItemSpawner itemSpawner;
    [SerializeField] private Text dps;

    private readonly Queue<Wagon> wagons = new Queue<Wagon>();
    private Wagon currentWagon;

    public float Speed = 1;

    private void Awake()
    {
        foreach (var wagon in GetComponentsInChildren<Wagon>())
        {
            wagons.Enqueue(wagon);
        }

        RegisterNextWagon();
    }

    private void Start()
    {
        dps.text = string.Empty;
    }

    private void RegisterNextWagon()
    {
        if (currentWagon != null)
        {
            currentWagon.DpsGoalReached -= CurrentWagon_DpsGoalReached;
            currentWagon.ItemSpawnThresholdReached -= CurrentWagon_ItemSpawnThresholdReached;
        }
        currentWagon = wagons.Dequeue();
        currentWagon.DpsGoalReached += CurrentWagon_DpsGoalReached;
        currentWagon.ItemSpawnThresholdReached += CurrentWagon_ItemSpawnThresholdReached;
    }

    private void CurrentWagon_ItemSpawnThresholdReached(Vector3 position)
    {
        var spawnPosition = position;
        spawnPosition.z = transform.position.z;
        itemSpawner.SpawnItem(spawnPosition);
    }

    private void CurrentWagon_DpsGoalReached()
    {
        // TODO
    }

    private void Update()
    {
        dps.text = FormatDps(currentWagon.Dps) + "/" + FormatDps(currentWagon.DpsGoal);
    }

    private string FormatDps(float value)
    {
        return value.ToString("F1");
    }
}