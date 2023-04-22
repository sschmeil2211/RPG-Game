using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private NavMeshAgent _navMeshAgent;
    private Transform _target;

    public float lookRadius = 10f;

    private void Start()
    {
        _target = PlayerManager.playerManagerInstance.player.transform;
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        float distance = Vector3.Distance(_target.position, transform.position);
        if (distance <= lookRadius)
        {
            _navMeshAgent.SetDestination(_target.position);
            if(distance <= _navMeshAgent.stoppingDistance)
            {
                FaceTarget();
            }
        }
    }

    private void FaceTarget()
    {
        Vector3 direction = (_target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
