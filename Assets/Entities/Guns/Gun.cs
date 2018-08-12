using UnityEngine;

public class Gun : MonoBehaviour {
    public Bullet BulletPrefab;
    public Animator GunAnimator;
    public string GunfireAnimationName = "Rifle";
    public float Cooldown = 3f;

    float MaxBulletSize = 1.4f;
    public float RifleBulletDamage = 2;
    public float ShotgunBulletDamage = 1;
    public float RevolverBulletDamage = 0.5f;

    float timer;

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer < Cooldown) return;
        GunAnimator.Play(GunfireAnimationName);
        ResetCooldown();
    }

    public void ResetCooldown()
    {
        timer = 0;
    }

    public void Shoot()
    {
        var maxBulletDamage = Mathf.Max(RifleBulletDamage, ShotgunBulletDamage, RevolverBulletDamage);
        var bulletDamage = 1f;
        if (GunfireAnimationName == "Rifle")
            bulletDamage = RifleBulletDamage;
        else if (GunfireAnimationName == "HighNoon")
            bulletDamage = RevolverBulletDamage;
        else if (GunfireAnimationName == "Shotgun")
            bulletDamage = ShotgunBulletDamage;

        var b = Instantiate<Bullet>(BulletPrefab, transform.position, transform.rotation);
        var scale = (bulletDamage / maxBulletDamage) * MaxBulletSize + 0.3f;
        b.transform.localScale = Vector3.one * scale;
        b.Damage = bulletDamage;
        b.GetComponent<TrailRenderer>().widthMultiplier *= scale;
    }
}
