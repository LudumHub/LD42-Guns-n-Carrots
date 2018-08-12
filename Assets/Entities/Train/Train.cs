using UnityEngine;
using UnityEngine.UI;

public class Train : MonoBehaviour
{
    [SerializeField] private ItemSpawner itemSpawner;
    [SerializeField] private Text dps;

    public float Speed = 1;
    public float DPS;
    public float RecalculationTimer = 3f;

    float damageRecieved;
    float timer;

    private void Start()
    {
        dps.text = DPS + "/5";
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer < RecalculationTimer) return;

        DPS = damageRecieved / timer;

        timer = 0;
        damageRecieved = 0;

        dps.text = DPS + "/5";
    }

    public void RecievedDamage(float damage, Vector3 position)
    {
        damageRecieved += damage;
        if (Random.value > 0.8f)
        {
            var spawnPosition = position;
            spawnPosition.z = transform.position.z;
            itemSpawner.SpawnItem(spawnPosition);
        }
    }
}
