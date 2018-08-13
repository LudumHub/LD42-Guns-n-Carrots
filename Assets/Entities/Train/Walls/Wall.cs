using System;
using UnityEngine;

public class Wall : MonoBehaviour
{
    [SerializeField] private GameObject hitParticle;

    public float DamageScale = 1;
    public FloatingNumber TextPrefab;
    public event Action<float, Vector3> DamageReceived;

    public void Hit(Bullet bullet)
    {
        if (hitParticle != null)
        {
            Instantiate(hitParticle, bullet.transform.position, Quaternion.identity);
        }

        var damage = bullet.Damage * DamageScale;
        if (DamageScale < 5) return;

        if (DamageReceived != null)
            DamageReceived(damage, bullet.transform.position);

        if (TextPrefab != null && DamageScale > 1)
        {
            var label = Instantiate(TextPrefab, bullet.transform.position, Quaternion.identity);
            label.textMesh.text = "- " + damage;
            label.textMesh.color = Color.red;
        }

        Destroy(gameObject);

    }
}
