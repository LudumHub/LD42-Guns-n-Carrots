using UnityEngine;
using UnityEngine.UI;

public class WorldItemView : MonoBehaviour
{
    [SerializeField] private Image itemImage;

    public GameObject InventoryTemplate;

    public Item Item { get; set; }

    private void Start()
    {
        Item = Item ?? new Item(InventoryTemplate);
        itemImage.sprite = Item.InventoryViewTemplate.GetComponent<Image>().sprite;
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