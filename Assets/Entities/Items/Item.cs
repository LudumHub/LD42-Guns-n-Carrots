﻿using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Item
{
    public List<Vector2Int> OccupiedSlotPositions;
    public GameObject InventoryViewTemplate;

    public Item(GameObject inventoryViewTemplate)
    {
        InventoryViewTemplate = inventoryViewTemplate;
        OccupiedSlotPositions = inventoryViewTemplate.GetComponent<ItemDimensions>().OccupiedSlotPositions;
    }
}