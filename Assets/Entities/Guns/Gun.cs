using UnityEngine;

public class Gun : MonoBehaviour {
    public Bullet BulletPrefab;
    public Animator GunAnimator;
    public AudioSource AudioSource;
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
        if (GunfireAnimationName == "HighNoon") {
            GunfireAnimationName = "Rifle";
            RifleBulletDamage = 0.5f;
            ShotgunBulletDamage = 2;
            gameObject.name = "BaseGun";
        }

        GunAnimator.Play(GunfireAnimationName);
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer < Cooldown) return;

        if ((BulletPrefab.name != "EnemyBullet") && !CharacterMovment.isMoving)
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


        if (GunfireAnimationName == "Rifle" && gameObject.name != "BaseGun")
        {
            bulletDamage = RifleBulletDamage;
            b.isRifle = true;
        }
        else if (GunfireAnimationName == "HighNoon")
        {
            bulletDamage = RevolverBulletDamage;
            GunfireAnimationName = "Rifle";
            bulletDamage = 0.1f;
        }
        else if (GunfireAnimationName == "Shotgun")
            bulletDamage = ShotgunBulletDamage;

        var scale = (bulletDamage / maxBulletDamage) * MaxBulletSize + 0.3f;

        if (!(BulletPrefab.name == "EnemyBullet"))
            Shaker.instance.Shake(0.1f * scale);

        b.transform.localScale = Vector3.one * scale;
        b.Damage = bulletDamage;
        b.GetComponent<TrailRenderer>().widthMultiplier *= scale;
        PlayGunSound(name == "BaseGun" ? "BaseGun" : GunfireAnimationName);
    }

    private void PlayGunSound(string gun)
    {
        var sound = FindObjectOfType<GunSoundsLibrary>().GetSound(gun);
        AudioSource.clip = sound;
        AudioSource.Play();
    }
}