using UnityEngine;
using System.Collections;

public class BossController : MonoBehaviour
{
    public Transform launchPoint;
    public Transform target; // assign the player transform
    public BossPhase[] phases;
    public GameObject projectilePrefab; // generic prefab with Projectile component & Rigidbody
    public Animator animator;

    private int _phaseIndex = 0;
    private int _shotsFired = 0;

    void Start()
    {
        if (phases == null || phases.Length == 0)
        {
            Debug.LogWarning("No phases assigned on BossController."); 
            return;
        }
        StartCoroutine(PhaseLoop());
    }

    IEnumerator PhaseLoop()
    {
        while (_phaseIndex < phases.Length)
        {
            var phase = phases[_phaseIndex];
            yield return StartCoroutine(PhaseRoutine(phase));
            _phaseIndex++;
            _shotsFired = 0;
        }
        OnBossDefeated();
    }

    IEnumerator PhaseRoutine(BossPhase phase)
    {
        while (_shotsFired < phase.shotsToNextPhase)
        {
            // Telegraph
            if (animator != null) animator.SetTrigger("Windup");
            yield return new WaitForSeconds(phase.windupTime);

            // Fire
            var pd = phase.patterns[Random.Range(0, phase.patterns.Length)];
            Fire(pd);

            _shotsFired++;
            yield return new WaitForSeconds(phase.interval);
        }
    }

    void Fire(ProjectileData data)
    {
        if (projectilePrefab == null)
        {
            Debug.LogError("Projectile prefab not assigned on BossController.");
            return;
        }

        var go = Instantiate(projectilePrefab, launchPoint.position, Quaternion.identity);
        var proj = go.GetComponent<Projectile>();
        proj.data = data;

        Vector3 dir = (target.position + Vector3.up * 1.2f - launchPoint.position).normalized;
        proj.Launch(launchPoint.position, dir);
    }

    void OnBossDefeated()
    {
        Debug.Log("Boss defeated! Play VFX/Rewards."); 
    }
}
