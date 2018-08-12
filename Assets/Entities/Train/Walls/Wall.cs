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
        var damage = bullet.Damage * DamageScale;

        if (DamageReceived != null)
            DamageReceived(damage, bullet.transform.position);

        if (hitParticle != null && damage > 1f)
        {
            Instantiate(hitParticle, bullet.transform.position, Quaternion.identity);
        }

        if (TextPrefab != null)
        {
            var label = Instantiate(TextPrefab, bullet.transform.position, Quaternion.identity);
            label.textMesh.text = "- " + damage;

            if (DamageScale < 1) label.textMesh.color = Color.gray;
            if (DamageScale > 1) label.textMesh.color = Color.red;
        }
    }
}
