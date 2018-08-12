using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Train : MonoBehaviour
{
    [SerializeField] private ItemSpawner itemSpawner;
    [SerializeField] private Text dps;
    [SerializeField] private float wagonLength = 9;

    private readonly Queue<Wagon> wagons = new Queue<Wagon>();
    private Wagon currentWagon;
    private bool shouldUpdateDps = true;

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
        StartCoroutine(MoveToNextWagon());
    }

    private IEnumerator MoveToNextWagon()
    {
        UnregisterCurrentWagon();
        UpdateDpsLabel();
        shouldUpdateDps = false;
        yield return currentWagon.Destroy();
        RegisterNextWagon();
        shouldUpdateDps = true;

        var currentY = transform.position.y;
        foreach (var y in Easing.OutSine(currentY, currentY - wagonLength, 2))
        {
            var position = transform.position;
            position.y = y;
            transform.position = position;
            yield return new WaitForEndOfFrame();
        }
    }

    private void UnregisterCurrentWagon()
    {
        if (currentWagon != null)
        {
            currentWagon.DpsGoalReached -= CurrentWagon_DpsGoalReached;
            currentWagon.ItemSpawnThresholdReached -= CurrentWagon_ItemSpawnThresholdReached;
        }
    }

    private void Update()
    {
        if (shouldUpdateDps)
        {
            UpdateDpsLabel();
        }
    }

    private void UpdateDpsLabel()
    {
        dps.text = FormatDps(currentWagon.Dps) + "/" + FormatDps(currentWagon.DpsGoal);
    }

    private string FormatDps(float value)
    {
        return value.ToString("F1");
    }
}