using UnityEngine;

public class ItemFactory: MonoBehaviour
{
    [SerializeField] private InventoryItemView inventoryTemplate;
    [SerializeField] private WorldItemView worldTemplate;
    [SerializeField] private Canvas gameInterface;
    [SerializeField] private GameObject[] itemTemplates;

    public InventoryItemView CreateInventoryItem(Item item)
    {
        var view = Instantiate(inventoryTemplate, gameInterface.transform);
        view.Item = item;
        Instantiate(item.InventoryViewTemplate, view.transform);
        return view;
    }

    public WorldItemView CreateWorldItem(Item item)
    {
        var view = Instantiate(worldTemplate);
        view.Item = item;
        return view;
    }

    public Item CreateRandomItem()
    {
        var template = itemTemplates[new System.Random().Next(itemTemplates.Length)];
        var item = new Item(template);
        return item;
    }
}