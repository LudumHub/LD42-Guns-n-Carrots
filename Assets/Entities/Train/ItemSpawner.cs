using UnityEngine;

public class ItemSpawner: MonoBehaviour
{
    [SerializeField] private ItemFactory itemFactory;

    public void SpawnItem(Vector3 position)
    {
        SpawnItem(itemFactory.CreateRandomItem(), position);
    }

    public void SpawnItem(Item item, Vector3 position)
    {
        var worldItem = itemFactory.CreateWorldItem(item);
        worldItem.transform.SetParent(transform.parent.parent);
        var randomOffset = (Vector3) Random.insideUnitCircle;
        worldItem.transform.position = position + randomOffset;
        worldItem.gameObject.AddComponent<ParabolicFlyMotion>();
    }
}