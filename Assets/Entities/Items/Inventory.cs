using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField] private GridLayoutGroup grid;
    [SerializeField] private InventorySlot slotTemplate;
    [SerializeField] private GameObject startingGun;
    [SerializeField] private Vector2Int startingGunPosition;

    private InventorySlot[,] slots;
    private InventorySlot hoveredSlot;

    private const int Columns = 5;
    private const int Rows = 5;

    public Item DraggedItem { get; set; }
    public ItemsOnCharacterUpdater GunsInHandsUpdater;

    private void Awake()
    {
        foreach (Transform child in grid.transform)
        {
            Destroy(child.gameObject);
        }

        slots = new InventorySlot[Columns, Rows];
        slotTemplate.gameObject.SetActive(true);
        for (var y = 0; y < Rows; y++)
        {
            for (var x = 0; x < Columns; x++)
            {
                var slot = Instantiate(slotTemplate, grid.transform);
                slot.Position = new Vector2Int(x, y);
                slot.PointerEnter += () => Slot_PointerEnter(slot);
                slot.PointerDown += Slot_PointerDown;
                slots[x, y] = slot;
            }
        }
        slotTemplate.gameObject.SetActive(false);
    }

    private IEnumerator Start()
    {
        // We need this wait because grid needs to layout before we can assign items to it
        yield return new WaitForEndOfFrame();
        var startingItem = new Item(startingGun);
        var startingItemView = FindObjectOfType<ItemFactory>().CreateInventoryItem(startingItem);
        AcceptItem(startingItemView, startingGunPosition);
    }

    private void Slot_PointerEnter(InventorySlot slot)
    {
        hoveredSlot = slot;
        foreach (var inventorySlot in slots)
        {
            inventorySlot.PointerExit -= Slot_PointerExit;
        }
        slot.PointerExit += Slot_PointerExit;
        if (DraggedItem != null)
        {
            HighlightDraggedItemSlots();
        }
        else if (slot.Item != null)
        {
            HighlightInventoryItem();
        }
    }

    private void HighlightInventoryItem()
    {
        var itemSlots = FindSlots(hoveredSlot.Item);
        foreach (var slot in itemSlots)
        {
            slot.HighlightHover();
        }
    }

    private void HighlightDraggedItemSlots()
    {
        ResetHighlight();
        var hoveredSlots = HoveredSlots.ToList();
        var isInvalidSelection = hoveredSlots.Any(s => s == null);
        hoveredSlots = hoveredSlots
            .Where(s => s != null)
            .ToList();
        foreach (var slot in hoveredSlots)
        {
            if (isInvalidSelection)
            {
                slot.HighlightInvalid();
            }
            else if (slot.Item != null)
            {
                slot.HighlightInvalid();
            }
            else
            {
                slot.HighlightHover();
            }
        }
    }

    private void ResetHighlight()
    {
        foreach (var slot in slots)
        {
            slot.ResetHighlight();
        }
    }

    public IEnumerable<Item> AllItems
    {
        get
        {
            return AllSlots
                .Where(s => s.Item != null)
                .Select(s => s.Item)
                .Distinct();
        }
    }

    private void Slot_PointerExit()
    {
        hoveredSlot = null;
        ResetHighlight();
    }

    private void Slot_PointerDown()
    {
        if (DraggedItem != null) return;
        if (hoveredSlot == null || hoveredSlot.Item == null) return;
        if (!Input.GetMouseButton(0))
        {
            hoveredSlot.ItemView.DropAway();
            ForgetAbout(hoveredSlot.Item);
            GunsInHandsUpdater.UpdateItemsList(AllItems);
            return;
        }
        DraggedItem = hoveredSlot.Item;
        hoveredSlot.ItemView.StartDragging();
        ForgetAbout(hoveredSlot.Item);
        HighlightDraggedItemSlots();
    }

    private void ForgetAbout(Item item)
    {
        foreach (var slot in FindSlots(item))
        {
            slot.Item = null;
            slot.ItemView = null;
        }
    }

    public bool AcceptDraggedItem(InventoryItemView itemView)
    {
        if (!IsDraggedItemAcceptable)
        {
            DraggedItem = null;
            GunsInHandsUpdater.UpdateItemsList(AllItems);
            return false;
        }

        foreach (var slot in
              HoveredSlots.Where(s => s.Item != null)
                .GroupBy(s => s.Item)
                .Select(s => s.First()))
        {
               slot.ItemView.DropAway();
               ForgetAbout(slot.Item);

        }

        AcceptItem(itemView, hoveredSlot.Position);
        DraggedItem = null;
        ResetHighlight();
        return true;
    }

    private void AcceptItem(InventoryItemView item, Vector2Int position)
    {
        item.transform.position = GetSlot(position).transform.position;
        foreach (var slot in GetHoveredSlots(item.Item, position))
        {
            slot.Item = item.Item;
            slot.ItemView = item;
        }
        GunsInHandsUpdater.UpdateItemsList(AllItems);
    }

    internal void ClearCarrots()
    {
        foreach (var slot in
              AllSlots.Where(s => s.Item != null && s.Item.ItemTag == "Carrot")
                .GroupBy(s => s.Item)
                .Select(s => s.First()))
        {
            ForgetAbout(slot.Item);
            Destroy(slot.ItemView.transform);
        }
        GunsInHandsUpdater.UpdateItemsList(AllItems);
    }

    private bool IsDraggedItemAcceptable
    {
        get { return hoveredSlot != null && HoveredSlots.All(s => s != null); }
    }

    private IEnumerable<InventorySlot> FindSlots(Item item)
    {
        return AllSlots.Where(s => s.Item == item);
    }

    private IEnumerable<InventorySlot> AllSlots
    {
        get { return slots.Cast<InventorySlot>(); }
    }

    private IEnumerable<InventorySlot> HoveredSlots
    {
        get { return GetHoveredSlots(DraggedItem, hoveredSlot.Position); }
    }

    private IEnumerable<InventorySlot> GetHoveredSlots(Item item, Vector2Int origin)
    {
        return item.OccupiedSlotPositions
            .Select(p => GetSlot(p, origin))
            .ToList();
    }

    private InventorySlot GetSlot(Vector2Int position, Vector2Int origin)
    {
        return GetSlot(origin + position);
    }

    private InventorySlot GetSlot(Vector2Int position)
    {
        return AllSlots.FirstOrDefault(s => s.Position.Equals(position));
    }
}