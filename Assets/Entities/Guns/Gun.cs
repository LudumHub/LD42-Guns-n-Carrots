using UnityEngine;

public class Gun : MonoBehaviour {
    public Bullet BulletPrefab;
    public Animator GunAnimator;
    public string GunfireAnimationName = "Rifle";
    public float Cooldown = 3f;

    float timer;

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer < Cooldown) return;

        timer = 0;

        GunAnimator.Play(GunfireAnimationName);
    }

    public void Shoot()
    {
        Instantiate(BulletPrefab.transform, transform.position, transform.rotation);

    }
}
