using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {
    public Bullet BulletPrefab;
    public Animator GunAnimator;
    public string GunfireAnimationName = "Rifle";
    public float Cooldown = 3f;

    float timer = 0f;

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer < Cooldown) return;

        timer = 0;

        GunAnimator.Play(GunfireAnimationName);
    }

    public void Shoot()
    {
        Instantiate<Transform>(BulletPrefab.transform, transform.position, transform.rotation);

    }
}
