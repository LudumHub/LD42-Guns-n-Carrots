using System.Linq;
using UnityEngine;

[ExecuteInEditMode]
public class EditorItemGrid : MonoBehaviour
{
    public RectTransform ItemToConfigure;

    public void ConfigureTarget()
    {
        if (ItemToConfigure == null)
        {
            Debug.LogError("Please, attach item to Grid to configure.");
            return;
        }

        var slots = GetComponentsInChildren<EditorInventorySlot>();
        var itemSlots = slots.Where(s => s.IsItem).ToList();
        if (itemSlots.Count == 0)
        {
            Debug.LogError("Item must occupy at least one slot");
            return;
        }

        var rootSlots = slots.Where(s => s.IsRoot).ToList();
        if (rootSlots.Count != 1)
        {
            Debug.LogError("Item must have exactly one root slot");
            return;
        }

        var rootSlot = rootSlots.Single();
        var rootPosition = rootSlot.transform.position;
        var pivot = CalculatePivot(ItemToConfigure, rootPosition);
        ItemToConfigure.pivot = pivot;
        ItemToConfigure.anchoredPosition = Vector2.zero;
        var dimensions = ItemToConfigure.GetComponent<ItemDimensions>();
        if (dimensions != null)
            DestroyImmediate(dimensions);
        dimensions = ItemToConfigure.gameObject.AddComponent<ItemDimensions>();
        var rootGridPosition = GetGridPosition(rootSlot);
        dimensions.OccupiedSlotPositions = slots
            .Where(s => s.IsItem || s.IsRoot)
            .Select(s => GetGridPosition(s) - rootGridPosition)
            .ToList();
    }

    private Vector2Int GetGridPosition(EditorInventorySlot slot)
    {
        var index = GetComponentsInChildren<EditorInventorySlot>()
            .ToList()
            .IndexOf(slot);
        return CalculateGridPosition(index);
    }

    private static Vector2Int CalculateGridPosition(int slotIndex)
    {
        const int columns = 3;
        return new Vector2Int(slotIndex % columns, slotIndex / columns);
    }

    private static Vector2 CalculatePivot(RectTransform rectTransform, Vector3 newPivotOrigin)
    {
        var corners = new Vector3[4];
        rectTransform.GetWorldCorners(corners);
        var rect = new Rect(corners[0], corners[2] - corners[0]);
        var localPivotOrigin = (Vector2) newPivotOrigin - rect.position;
        return new Vector2(localPivotOrigin.x / rect.width, localPivotOrigin.y / rect.height);
    }

    public void ResetGrid()
    {
        var slots = GetComponentsInChildren<EditorInventorySlot>();
        foreach (var slot in slots)
        {
            slot.IsItem = false;
            slot.IsRoot = false;
        }
    }
}