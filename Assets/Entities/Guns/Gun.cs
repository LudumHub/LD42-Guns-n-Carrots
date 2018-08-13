using UnityEngine;

public class Gun : MonoBehaviour {
    public Bullet BulletPrefab;
    public Animator GunAnimator;
    public string GunfireAnimationName = "Rifle";
    public float Cooldown = 3f;

    public ParticleSystem GunshotEffect;

    float MaxBulletSize = 0.9f;
    public float RifleBulletDamage = 2;
    public float ShotgunBulletDamage = 1;
    public float RevolverBulletDamage = 0.5f;

    float timer;

    private void Start()
    {
        GunAnimator.Play(GunfireAnimationName);
    }

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
        GunshotEffect.Play();

        var maxBulletDamage = Mathf.Max(RifleBulletDamage, ShotgunBulletDamage, RevolverBulletDamage, 15);
        var bulletDamage = 1f;

        var b = Instantiate<Bullet>(BulletPrefab, transform.position, transform.rotation);


        if (GunfireAnimationName == "Rifle" && name != "BaseGun")
        {
            bulletDamage = RifleBulletDamage;
            b.isRifle = true;
        }
        else if (GunfireAnimationName == "HighNoon")
            bulletDamage = RevolverBulletDamage;
        else if (GunfireAnimationName == "Shotgun")
            bulletDamage = ShotgunBulletDamage;

        var scale = (bulletDamage / maxBulletDamage) * MaxBulletSize + 0.3f;

        if (!(BulletPrefab.name == "EnemyBullet"))
            Shaker.instance.Shake(0.1f * scale);

        b.transform.localScale = Vector3.one * scale;
        b.Damage = bulletDamage;
        b.GetComponent<TrailRenderer>().widthMultiplier *= scale;
    }
}
