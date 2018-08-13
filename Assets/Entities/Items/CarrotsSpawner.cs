using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarrotsSpawner : ItemSpawner {
    public float CheckPlayersInventoryCooldown = 5f;

    public GameObject Carrot1;
    public GameObject Carrot2;
    float timer;

    private void Start()
    {
        Spawn();
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer < CheckPlayersInventoryCooldown) return;
        timer = 0;
        Spawn();
    }

    private void Spawn()
    {
        SpawnItem(new Item(Random.value > 0.5 ? Carrot1 : Carrot2),
                    transform.position);
    }
}
