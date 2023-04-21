using UnityEngine;

public class CameraController : MonoBehaviour
{
    private float _currentZoom = 10f;
    private float _currentDrift = 0f;

    public float driftSpeed = 100f;
    public float maxZoom = 15f;
    public float minZoom = 5f;
    public float pitch = 2f;
    public float zoomSpeed = 4f;
    public Transform target;
    public Vector3 offset;

    private void Update()
    {
        _currentZoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        _currentZoom = Mathf.Clamp(_currentZoom, minZoom, maxZoom);
        _currentDrift -= Input.GetAxis("Horizontal") * driftSpeed * Time.deltaTime;
    }

    private void LateUpdate()
    {
        transform.position = target.position - offset * _currentZoom;
        transform.LookAt(target.position + Vector3.up * pitch);
        transform.RotateAround(target.position, Vector3.up, _currentDrift);
    }
}
