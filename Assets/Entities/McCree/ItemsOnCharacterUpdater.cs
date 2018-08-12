using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemsOnCharacterUpdater : MonoBehaviour {
    public Gun GunPrefub;
    public CharacterMovment characterMovment;

    Dictionary<Item, Transform> items = new Dictionary<Item, Transform>();

    private void Awake()
    {
        foreach (var sprite in GetComponentsInChildren<SpriteRenderer>())
        {
            sprite.sortingLayerName = "Player";
        }
    }

    public void UpdateItemsList(IEnumerable<Item> allItems)
    {
        List<Item> usedItems = new List<Item>();

        foreach (var i in allItems)
        {
            if (items.ContainsKey(i))
                usedItems.Add(i);
            else
            {
                if (i.ItemTag != "Carrot")
                {
                    AddNewGun(i);
                    usedItems.Add(i);
                }
            }
        }

        foreach (var item in items.Where(kvp => !usedItems.Contains(kvp.Key)).ToList())
            RemoveGun(item.Key);

        characterMovment.Carrots = allItems
            .Where(kvp => kvp.ItemTag == "Carrot")
            .Count();
    }

    private void RemoveGun(Item i)
    {
        Destroy(items[i].gameObject);
        items.Remove(i);
    }

    private void AddNewGun(Item i)
    {
        var gun = Instantiate<Gun>(GunPrefub, transform.position, Quaternion.identity, transform);
        gun.GunfireAnimationName = i.ItemTag;
        items.Add(i, gun.transform);
    }
}
