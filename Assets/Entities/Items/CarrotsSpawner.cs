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
        Spawn(GetRandomCarrot());
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer < CheckPlayersInventoryCooldown) return;
        timer = 0;
        Spawn();
    }

    private GameObject GetRandomCarrot()
    {
        return Random.value > 0.5 ? Carrot1 : Carrot2;
    }

    private void Spawn()
    {
        var item = GetRandomCarrot();
        if (!itemsOnCharacterUpdater.items.Keys.Any(i => LastRecievedGun.CompareTag(i.ItemTag)))
            item = LastRecievedGun;

        Spawn(item);
    }

    private void Spawn(GameObject item)
    {
        SpawnItem(new Item(item), transform.position);
    }
}
