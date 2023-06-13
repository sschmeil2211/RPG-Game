using UnityEngine; 

public class Bullet : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerStats playerStat = other.GetComponent<PlayerStats>();
            playerStat.TakeDamage(33);
            Destroy(gameObject);
        }
    }
}
