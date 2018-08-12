using UnityEngine;

public class WorldItemView : MonoBehaviour
{
    public GameObject InventoryTemplate;

    public Item Item { get; set; }

    private void Start()
    {
        Item = Item ?? new Item(InventoryTemplate);
        var hitboxGo = new GameObject("Sprite");
        hitboxGo.transform.SetParent(transform, false);
        hitboxGo.layer = LayerMask.NameToLayer("RoadItems");
        var viewSprite = hitboxGo.AddComponent<WorldItemViewSprite>();
        viewSprite.Assign(Item);
        var box = gameObject.AddComponent<BoxCollider2D>();
        box.size = viewSprite.GetComponent<SpriteRenderer>().size + Vector2.one * 0.1f;
    }

    private void OnMouseDown()
    {
        var inventoryView = FindObjectOfType<ItemFactory>().CreateInventoryItem(Item);
        inventoryView.StartDragging();
        var inventory = FindObjectOfType<Inventory>();
        inventory.DraggedItem = Item;
        Destroy(gameObject);
    }
}