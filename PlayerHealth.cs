using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class ArmorItem
{
    public string slot;
    public int hitNegation = 0; // number of hits that are absorbed
    [Range(0f, 1f)]
    public float damageReduction = 0f; // percent reduction
}

public class PlayerHealth : MonoBehaviour
{
    public float maxHp = 100f;
    public List<ArmorItem> equipped = new List<ArmorItem>();
    private float _hp;

    void Awake()
    {
        _hp = maxHp;
    }

    public void TakeHit(float dmg)
    {
        // armor negation first
        for (int i = 0; i < equipped.Count; i++)
        {
            if (equipped[i].hitNegation > 0)
            {
                equipped[i].hitNegation--;
                // TODO: play shield VFX
                return;
            }
        }

        // reduction
        float reduction = 0f;
        foreach (var a in equipped) reduction += a.damageReduction;
        reduction = Mathf.Clamp01(reduction);

        _hp -= dmg * (1f - reduction);
        Debug.Log($"Player took {dmg} dmg (after reduction: {dmg*(1f-reduction)}). HP:{_hp}/{maxHp}");

        if (_hp <= 0f) Die();
    }

    void Die()
    {
        Debug.Log("Player died. Trigger death flow."); // Replace with your respawn/retry logic.
    }
}
