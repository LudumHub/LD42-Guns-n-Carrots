using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Train : MonoBehaviour {
    public float DPS = 0f;
    public float RecalculationTimer = 3f;
    public TextMesh DPSLabel;

    float damageRecieved = 0;
    float timer = 0;

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
    }
}
