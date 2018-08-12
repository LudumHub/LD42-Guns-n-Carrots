using UnityEngine;

public class Train : MonoBehaviour
{
    [SerializeField] private ItemSpawner itemSpawner;

    public float Speed = 1;
    public float DPS;
    public float RecalculationTimer = 3f;
    public TextMesh DPSLabel;

    float damageRecieved;
    float timer;

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer < RecalculationTimer) return;

        DPS = damageRecieved / timer;

        timer = 0;
        damageRecieved = 0;

        DPSLabel.text = "DPS: " + DPS + "/5";
    }

    public void Hit(int damage)
    {
        damageRecieved += damage;
        if(Random.value > 0.8f)
            itemSpawner.SpawnItem();
    }
}
