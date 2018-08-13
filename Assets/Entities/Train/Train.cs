using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Train : MonoBehaviour
{
    [SerializeField] private ItemSpawner itemSpawner;
    [SerializeField] private Text dps;
    [SerializeField] private float wagonLength = 9;
    [SerializeField] private AudioSource TUTURU;

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

        foreach (var bandos in GetComponentsInChildren<Wall>().Where(w => w.IsBandos))
        {
            bandos.SetGunEnabled(false);
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
        if (wagons.Count > 0)
        {
            currentWagon = wagons.Dequeue();
            currentWagon.DpsGoalReached += CurrentWagon_DpsGoalReached;
            currentWagon.ItemCreated += CurrentWagon_ItemCreated;
            foreach (var bandos in currentWagon.GetComponentsInChildren<Wall>().Where(w => w.IsBandos))
            {
                bandos.SetGunEnabled(true);
            }
        }
        else
            StartCoroutine(Fin());
    }

    private IEnumerator Fin()
    {
        var s = Camera.main.GetComponent<Shaker>();
        s.StartPosition += new Vector3(-10, -20, 0);
        s.smoothDampTime = 5f;

        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene(2);
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
        TUTURU.Play();
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