using UnityEngine;

[RequireComponent(typeof(Collider))]
public class BatterController : MonoBehaviour
{
    public Animator animator;
    public Collider batHitbox; // Trigger collider on the bat (disabled by default)
    public PlayerHealth health;
    public float moveSpeed = 5f;

    private void Reset()
    {
        // Attempt to auto-populate common components if available
        animator = GetComponentInChildren<Animator>();
        var col = GetComponentInChildren<Collider>();
        if (col != null) batHitbox = col;
    }

    void Update()
    {
        HandleMove();
        if (Input.GetButtonDown("Fire1"))
        {
            animator.SetTrigger("Swing");
        }
    }

    void HandleMove()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 dir = new Vector3(h, 0, v).normalized;
        transform.Translate(dir * moveSpeed * Time.deltaTime, Space.World);
    }

    // Called by Animation Event on swing animation:
    public void EnableBatHitbox() => batHitbox.enabled = true;
    public void DisableBatHitbox() => batHitbox.enabled = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Projectile>(out var proj))
        {
            if (!proj.IsReflected)
            {
                health?.TakeHit(proj.data.damage);
                proj.OnHitPlayer();
            }
        }
    }
}
