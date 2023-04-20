using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float radius = 3f;
    public Transform interactionTransform;

    private bool _hasInteracted = false;
    private bool _isFocus = false;
    private Transform _player;

    private void Update()
    {
        if (_isFocus && !_hasInteracted)
        {
            float distance = Vector3.Distance(_player.position, interactionTransform.position);
            if (distance <= radius)
            {
                Interact();
                _hasInteracted |= true;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interactionTransform.position, radius);
    }

    public virtual void Interact()
    {
        Debug.Log("Interact"); 
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
