using UnityEngine;

public class BossHealth : MonoBehaviour
{
    public float maxHp = 200f;
    private float _hp;

    void Awake() { _hp = maxHp; }

    public void TakeHit(float dmg)
    {
        _hp -= dmg;
        Debug.Log($"Boss took {dmg} dmg. HP:{_hp}/{maxHp}");
        // TODO: play flinch VFX, camera impulse
        if (_hp <= 0f) Die();
    }

    void Die()
    {
        Debug.Log("Boss died."); // play death anim, show results UI
        Destroy(gameObject, 1.5f);
    }
}
