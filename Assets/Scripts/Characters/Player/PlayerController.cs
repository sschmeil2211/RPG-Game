using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{
    private Camera _camera;
    private PlayerMotor _playerMotor;

    public LayerMask movementMask; //Usado para ver dodne podemos caminar y donde no
    public Interactable focus; //Objeto con el que estamos interactuando

    private void Start()
    {
        _camera = Camera.main;
        _playerMotor = GetComponent<PlayerMotor>();
    }

    private void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject() && BuildingManager.buildingManagerInstance.IsActive)
            return;
        MovePlayer();
    }

    private void MovePlayer()
    {
        //Podemos movernos solo si estamos fuera del menu de construccion
        if (!BuildingManager.buildingManagerInstance.IsActive)
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
    }

    //Seteamos el objeto al que queremos focusear
    private void SetFocus(Interactable newFocus)
    {
        //Si el objetivo de focus cambio
        if(newFocus != focus)
        {
            //defocuseamos el viejo
            if(focus != null)
                focus.OnDefocused();
            focus = newFocus; //Seteamos el nuevo focus
            _playerMotor.FollowTarget(newFocus); //Y lo perseguimos
        }
        newFocus.OnFocused(transform);
    }

    //Removemos el focus
    private void RemoveFocus()
    {
        if(focus != null)
            focus.OnDefocused();
        focus = null;
        _playerMotor.StopFollowingTarget();
    }
}
