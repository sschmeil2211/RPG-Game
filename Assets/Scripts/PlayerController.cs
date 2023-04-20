using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{
    private Camera _camera;
    private PlayerMotor _playerMotor;

    public LayerMask movementMask;
    public Interactable focus;

    private void Start()
    {
        _camera = Camera.main;
        _playerMotor = GetComponent<PlayerMotor>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);//We create a ray
            if (Physics.Raycast(ray, out RaycastHit hit, 100, movementMask))//Si el raycast colisiona
            {
                Debug.Log("Colision con " + hit.collider.name + " en " + hit.point);
                _playerMotor.MoveTo(hit.point);//Muevo al jugador al lugar
                RemoveFocus();//Dejo de focusear los objetos
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 100))
            {
                if (hit.collider.TryGetComponent<Interactable>(out var interactable))//If we did set it as our focus
                    SetFocus(interactable);
            }
        }
    }

    private void SetFocus(Interactable newFocus)
    {
        if(newFocus != focus)
        {
            if(focus != null)
                focus.OnDefocused();
            focus = newFocus;
            _playerMotor.FollowTarget(newFocus);
        }
        newFocus.OnFocused(transform);
    }

    private void RemoveFocus()
    {
        if(focus != null)
            focus.OnDefocused();
        focus = null;
        _playerMotor.StopFollowingTarget();
    }
}
