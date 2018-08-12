using UnityEngine;

public class ItemSpawner: MonoBehaviour
{
    [SerializeField] private ItemFactory itemFactory;

    public void SpawnItem()
    {
        var item = itemFactory.CreateRandomItem();
        var worldItem = itemFactory.CreateWorldItem(item);
        worldItem.transform.SetParent(transform.parent);
        var randomOffset = (Vector3) Random.insideUnitCircle;
        worldItem.transform.position = transform.position + randomOffset;
        worldItem.gameObject.AddComponent<ParabolicFlyMotion>();
    }
}