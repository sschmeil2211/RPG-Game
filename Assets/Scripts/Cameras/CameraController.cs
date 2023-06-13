using UnityEngine;

public class CameraController : MonoBehaviour
{
    private float _currentZoom = 10f;
    private float _currentDrift = 0f;

    public float driftSpeed = 100f; //Velocidad de rotacion
    public float maxZoom = 15f;
    public float minZoom = 5f;
    public float pitch = 2f; //Inclinacion de la camara
    public float zoomSpeed = 4f; //Velocidad al hacer zoom
    public Transform target; //Target a seguir, jugador
    public Vector3 offset; //Desplazamiento del jugador

    private void Update()
    {
        //Ajusta el zoom en funcion de la ruedita
        _currentZoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        _currentZoom = Mathf.Clamp(_currentZoom, minZoom, maxZoom);
        //Ajusta la rotacion de la camara
        _currentDrift -= Input.GetAxis("Horizontal") * driftSpeed * Time.deltaTime; 
    }

    private void LateUpdate()
    {
        //Establece la posicion de la camara en funcion del zoom y movimiento
        transform.position = target.position - offset * _currentZoom;
        //Mira al jugador
        transform.LookAt(target.position + Vector3.up * pitch);
        //Rota sobre el jugador
        transform.RotateAround(target.position, Vector3.up, _currentDrift);
    }
}
