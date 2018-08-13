using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CarrotsSpawner : ItemSpawner {
    public float CheckPlayersInventoryCooldown = 5f;

    public GameObject Carrot1;
    public GameObject Carrot2;

    public ItemsOnCharacterUpdater itemsOnCharacterUpdater;
    public static GameObject LastRecievedGun;
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
        var item = Random.value > 0.5 ? Carrot1 : Carrot2;
        if (itemsOnCharacterUpdater.items.Keys.Where(i => i.ItemTag != "Carrot").Count() == 0)
            item = LastRecievedGun;

        SpawnItem(new Item(item), transform.position);
    }
}
