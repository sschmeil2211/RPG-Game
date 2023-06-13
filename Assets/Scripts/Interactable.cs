using UnityEngine;

public class Interactable : MonoBehaviour
{ 
    private bool _hasInteracted = false;
    private bool _isFocus = false;
    private Transform _player;

    public float radius = 3f; //Que tan cerca debo estar para interactuar
    public Transform interactionTransform;

    private void Update()
    {
        if (_isFocus && !_hasInteracted)
        {
            float distance = Vector3.Distance(_player.position, interactionTransform.position);
            if (distance <= radius)
            {
                Interact();
                _hasInteracted = true;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if(interactionTransform == null)
            interactionTransform = transform;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interactionTransform.position, radius);
    }

    public virtual void Interact()
    {
        Debug.Log("Interaction with " + transform.name); 
    }

    public void OnFocused(Transform playerTransform)
    {
        _isFocus = true;
        _player = playerTransform;
        _hasInteracted = false;
    }

    public void OnDefocused()
    {
        _isFocus = false;
        _player = null;
        _hasInteracted = false;
    }
} 