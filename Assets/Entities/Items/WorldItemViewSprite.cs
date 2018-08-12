using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class WorldItemViewSprite : MonoBehaviour
{
    public void Assign(Item item)
    {
        var image = item.InventoryViewTemplate.GetComponent<Image>();
        var spriteSize = GetSize(item);
        var rotation = image.transform.rotation.eulerAngles.z;
        if (Math.Abs(rotation - 90) < Mathf.Epsilon || Math.Abs(rotation - 270) < Mathf.Epsilon)
            spriteSize = Rotate(spriteSize);
        var sprite = gameObject.AddComponent<SpriteRenderer>();
        sprite.drawMode = SpriteDrawMode.Sliced;
        sprite.sprite = image.sprite;
        sprite.size = spriteSize;
        sprite.sortingLayerName = "RoadItems";
        sprite.flipX = image.transform.localScale.x < 0;
        sprite.flipY = image.transform.localScale.y < 0;
        transform.rotation = image.transform.rotation;
    }

    private static Vector2 GetSize(Item item)
    {
        var positions = item.OccupiedSlotPositions;
        var left = positions.Min(p => p.x);
        var right = positions.Max(p => p.x);
        var top = positions.Min(p => p.y);
        var bottom = positions.Max(p => p.y);
        const float sizeStep = 0.35f;
        return new Vector2(right - left + 1, bottom - top + 1) * sizeStep;
    }

    private static Vector2 Rotate(Vector2 vector)
    {
        return new Vector2(vector.y, vector.x);
    }
}