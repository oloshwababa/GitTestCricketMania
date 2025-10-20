using UnityEngine;

[CreateAssetMenu(menuName = "ArcadeCricket/ProjectileData") ]
public class ProjectileData : ScriptableObject
{
    public string displayName = "Projectile";
    public float speed = 15f;
    public float damage = 10f;
    public float reflectMultiplier = 1.2f;
    public float reflectDamageMultiplier = 1.5f;
    public bool usesGravity = false;
    public GameObject prefab;
    public AudioClip whooshSfx;
    public AudioClip hitSfx;
}
