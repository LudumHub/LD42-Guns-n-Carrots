using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstGunSpawner : ItemSpawner {
    public float CheckPlayersInventoryCooldown = 5f;
    public ItemsOnCharacterUpdater itemsOnCharacterUpdater;
    float timer;

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer < CheckPlayersInventoryCooldown) return;
        timer = 0;

        if (itemsOnCharacterUpdater.items.Count == 0)
            SpawnItem(transform.position);
    }
}
