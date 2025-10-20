using UnityEngine;

// Attach to the Bat GameObject which has a Trigger Collider.
public class BatHitbox : MonoBehaviour
{
    private Collider _col;

    void Awake()
    {
        _col = GetComponent<Collider>();
        if (_col != null) _col.isTrigger = true;
        _col.enabled = false;
    }

    public void SetActive(bool on)
    {
        if (_col != null) _col.enabled = on;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Projectile>(out var proj))
        {
            // Use the player's forward as reflection direction by default if available
            Vector3 batDir = transform.root != null ? transform.root.forward : transform.forward;
            proj.OnBatted(batDir);
        }
    }
}
