using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallsTrap : MonoBehaviour
{
    [SerializeField] float _distanceToCover;
    [SerializeField] float _speed;
    private Vector3 _startingPosition;

    public PlayerStats playerStat;

    private void Start() => _startingPosition = transform.position;

    private void Update()
    {
        Vector3 startingPosition = _startingPosition;
        startingPosition.z += _distanceToCover * Mathf.Sin(Time.time * _speed);
        transform.position = startingPosition;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) 
            playerStat.TakeDamage(100);  
    }
}
