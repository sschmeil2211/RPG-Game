using System.Collections;
using UnityEngine;

public class SpellCasterEnemyController : EnemyController
{
    public GameObject projectilePrefab;
    public Transform projectileSpawnPoint;
    public float baseProjectileSpeed = 10f;
    public float maxProjectileSpeed = 20f;
    public float distanceThreshold = 10f;
    public float attackCooldown = 2f;
    public int projectileDamage = 33;

    private bool _canAttack = true;

    protected override void Update()
    {
        base.Update();

        if (characterCombat.EnemyKind == EnemyType.CASTER && _canAttack)
        {
            if (IsPlayerInRange())
            {
                FaceTarget();
                LaunchProjectile();
                StartCoroutine(AttackCooldown());
            }
        }
    }

    private void LaunchProjectile()
    {
        GameObject projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, Quaternion.identity);

        Rigidbody projectileRigidbody = projectile.GetComponent<Rigidbody>();

        float distanceToPlayer = Vector3.Distance(target.position, transform.position);
        float speedMultiplier = Mathf.Lerp(1f, maxProjectileSpeed / baseProjectileSpeed, distanceToPlayer / distanceThreshold);
        float finalProjectileSpeed = baseProjectileSpeed * speedMultiplier;

        projectileRigidbody.velocity = transform.forward * finalProjectileSpeed;
        Destroy(projectile, 5f);
    }

    private bool IsPlayerInRange()
    {
        float distance = Vector3.Distance(target.position, transform.position);
        return distance <= lookRadius;
    }

    private IEnumerator AttackCooldown()
    {
        _canAttack = false;
        yield return new WaitForSeconds(attackCooldown);
        _canAttack = true;
    }
}