using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]//Lo usamos para mover al player
public class PlayerMotor : MonoBehaviour
{
    private NavMeshAgent _navMeshAgent; //Referencia al agente
    private Transform _target; // Target a seguir

    private void Start() => _navMeshAgent = GetComponent<NavMeshAgent>();

    private void Update()
    {
        if (_target != null)
        {
            _navMeshAgent.SetDestination(_target.position);
            FaceTarget();
        }
    }

    private void FaceTarget() 
    {
        Vector3 direction = (_target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    //Muevo al objetivo
    public void MoveTo(Vector3 pointToMove) => _navMeshAgent.SetDestination(pointToMove);

    //Persigue al objetivo
    public void FollowTarget(Interactable newTarget)
    {
        _navMeshAgent.stoppingDistance = newTarget.radius * 0.8f;
        _navMeshAgent.updateRotation = false;
        _target = newTarget.interactionTransform;
    }

    //Deja de seguir al objetivo
    public void StopFollowingTarget()
    {
        _navMeshAgent.stoppingDistance = 0f;
        _navMeshAgent.updateRotation = true;
        _target = null;
    }
}
