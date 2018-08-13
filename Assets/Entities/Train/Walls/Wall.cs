﻿using System;
using UnityEngine;

public class Wall : MonoBehaviour
{
    [SerializeField] private GameObject hitParticle;

    public float DamageScale = 1;
    public FloatingNumber TextPrefab;
    public event Action<float, Vector3, GameObject, bool> DamageReceived;
    public GameObject drop;

    public void Hit(Bullet bullet)
    {
        PlayParticles(bullet.transform.position);

        var damage = bullet.Damage * DamageScale;
        if (DamageScale < 5 && !bullet.isRifle) return;

        if (DamageReceived != null)
            DamageReceived(damage, bullet.transform.position, drop, CompareTag("Enemy"));

        if (TextPrefab != null && DamageScale > 1)
        {
            var label = Instantiate(TextPrefab, bullet.transform.position, Quaternion.identity);
            label.textMesh.text = "- " + damage;
            label.textMesh.color = Color.red;
        }

        Destroy(gameObject);

    }

    public void PlayParticles(Vector3 position)
    {
        if (hitParticle != null)
        {
            Instantiate(hitParticle, position, Quaternion.identity);
        }
    }
}
