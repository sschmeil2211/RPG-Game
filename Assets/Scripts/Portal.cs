using UnityEngine;

public class Portal : MonoBehaviour
{
    private bool _isOverlapping = false;

    public Transform player;
    public Transform spawnPoint;

    private void Update()
    {
        if (_isOverlapping)
            player.transform.position = spawnPoint.transform.position; 
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
            _isOverlapping = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            _isOverlapping = false;
    }
}
