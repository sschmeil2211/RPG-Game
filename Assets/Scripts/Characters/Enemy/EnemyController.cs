using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed = 5f;

    protected CharacterCombat characterCombat;
    protected NavMeshAgent navMeshAgent;
    protected Transform target; //Referencia al jugador

    public float lookRadius = 10f; //Rango de deteccion 

    private void Start()
    {
        target = PlayerManager.playerManagerInstance.player.transform;
        navMeshAgent = GetComponent<NavMeshAgent>();
        characterCombat = GetComponent<CharacterCombat>();
    }

    protected virtual void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);
        if (distance <= lookRadius)
        {
            if (characterCombat.EnemyKind == EnemyType.CASTER)
                FaceTarget();
            if (characterCombat.EnemyKind == EnemyType.MELEE)
            {
                navMeshAgent.SetDestination(target.position);
                if (distance <= navMeshAgent.stoppingDistance)
                {
                    if (target.TryGetComponent<CharacterStats>(out var targetStats))
                        characterCombat.Attack(targetStats);
                    FaceTarget();
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

    protected void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * _rotationSpeed);
    }
}