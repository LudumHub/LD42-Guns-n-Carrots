﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    }

    private void Start()
    {
        if (wagons.Any(w => w.SkipToThis))
        {
            var wagonsSkipped = 0;
            while (!wagons.Peek().SkipToThis)
            {
                var wagon = wagons.Dequeue();
                Destroy(wagon.gameObject);
                wagonsSkipped++;
            }
            transform.position = transform.position - new Vector3(0, wagonsSkipped * wagonLength, 0);
        }
        RegisterNextWagon();
        dps.text = string.Empty;
        ProtectionWall.SetActive(false);
    }

    private void RegisterNextWagon()
    {
        UnregisterCurrentWagon();
        currentWagon = wagons.Dequeue();
        currentWagon.DpsGoalReached += CurrentWagon_DpsGoalReached;
        currentWagon.ItemCreated += CurrentWagon_ItemCreated;
    }

    private void CurrentWagon_ItemCreated(Item item, Vector3 position)
    {
        var spawnPosition = position;
        spawnPosition.z = transform.position.z;
        itemSpawner.SpawnItem(item, spawnPosition);
    }

    public void CurrentWagon_DpsGoalReached()
    {
        StartCoroutine(MoveToNextWagon());
    }

    public GameObject ProtectionWall;

    private IEnumerator MoveToNextWagon()
    {
        ProtectionWall.SetActive(true);

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

        ProtectionWall.SetActive(false);
    }

    private void UnregisterCurrentWagon()
    {
        if (currentWagon != null)
        {
            currentWagon.DpsGoalReached -= CurrentWagon_DpsGoalReached;
            currentWagon.ItemCreated -= CurrentWagon_ItemCreated;
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