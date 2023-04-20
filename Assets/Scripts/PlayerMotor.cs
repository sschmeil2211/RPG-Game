using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerMotor : MonoBehaviour
{
    private NavMeshAgent _navMeshAgent;

    private void Start() => _navMeshAgent = GetComponent<NavMeshAgent>();

    public void MoveTo(Vector3 pointToMove) => _navMeshAgent.SetDestination(pointToMove);
}
