using UnityEngine;

public class Train : MonoBehaviour
{
    [SerializeField] private ItemSpawner itemSpawner;

    public static Train instance;

    public float Speed = 1;
    public float DPS;
    public float RecalculationTimer = 3f;
    public TextMesh DPSLabel;

    float damageRecieved;
    float timer;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer < RecalculationTimer) return;

        DPS = damageRecieved / timer;

        timer = 0;
        damageRecieved = 0;

        DPSLabel.text = "DPS: " + DPS + "/5";
    }

    public void RecievedDamage(float damage, Vector3 position)
    {
        damageRecieved += damage;
        if (Random.value > 0.8f)
        {
            itemSpawner.transform.position = 
                new Vector3(position.x, position.y, transform.position.z);
            itemSpawner.SpawnItem();
        }
    }
}
