using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    public ProjectileData data;
    public Rigidbody rb;
    public float lifeTime = 8f;
    public bool IsReflected { get; private set; } = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = data != null && data.usesGravity;
    }

    public void Launch(Vector3 origin, Vector3 dir)
    {
        transform.position = origin;
        if (rb == null) rb = GetComponent<Rigidbody>();
        rb.velocity = dir.normalized * (data != null ? data.speed : 10f);
        Invoke(nameof(Despawn), lifeTime);
    }

    public void OnBatted(Vector3 batDir)
    {
        IsReflected = true;
        if (rb == null) rb = GetComponent<Rigidbody>();
        rb.velocity = batDir.normalized * (data != null ? data.speed * data.reflectMultiplier : 20f);
        // Optionally change layer/tag so it doesn't hurt player now.
        gameObject.tag = "PlayerProjectile";
        // small hit effect or SFX could be triggered here.
    }

    public void OnHitPlayer()
    {
        // default behavior when hitting player (non-reflected)
        // play SFX, spawn vfx, then destroy
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (IsReflected)
        {
            if (other.TryGetComponent<BossHealth>(out var boss))
            {
                float dmg = data != null ? data.damage * data.reflectDamageMultiplier : 20f;
                boss.TakeHit(dmg);
                Destroy(gameObject);
            }
        }
        else
        {
            if (other.TryGetComponent<PlayerHealth>(out var player))
            {
                player.TakeHit(data != null ? data.damage : 10f);
                Destroy(gameObject);
            }
        }
    }

    void Despawn() => Destroy(gameObject);
}
