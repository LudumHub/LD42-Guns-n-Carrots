using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastrunCheck : MonoBehaviour {
    public Train train;
    public Inventory items;
    float cooldown = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (cooldown > 0) return;
        train.CurrentWagon_DpsGoalReached();
        cooldown = 10f;
    }

    private void Update()
    {
        if (cooldown < 0) return;
        cooldown -= Time.deltaTime;
    }

}
