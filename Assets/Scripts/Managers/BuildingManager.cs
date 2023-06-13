using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingManager : MonoBehaviour
{
    #region Singleton
    public static BuildingManager buildingManagerInstance;

    private void Awake()
    {
        if (buildingManagerInstance != null)
        {
            Debug.LogWarning("Mas de un inventario");
            return;
        }
        buildingManagerInstance = this;
    }
    #endregion

    private bool _gridEnabled;
    private bool _isActive = false;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private List<Material> _materials;
    private RaycastHit _hit;
    [SerializeField] private Toggle _gridToggle;
    private Vector3 _position;

    public bool canPlace = true;
    public float gridSize;
    public float rotateAmount;
    public GameObject pendingObject;
    public List<GameObject> gameObjects = new();

    public bool IsActive
    {
        get => _isActive;
        set => _isActive = value;
    }

    private void Update()
    {  
        if (pendingObject == null) 
            return;
        Debug.Log("Entro");
        if (_gridEnabled)
            pendingObject.transform.position = new(RoundToNearestGrid(_position.x), RoundToNearestGrid(_position.y), RoundToNearestGrid(_position.z));
        else
            pendingObject.transform.position = _position; 
        if (Input.GetMouseButtonDown(0) && canPlace)
            PlaceObject();
        if (Input.GetKeyDown(KeyCode.R))
            RotateObject(); 
        UpdateMaterials(); 
    }

    private void FixedUpdate()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out _hit, 1000, _layerMask)) 
            _position = _hit.point;  
    }

    private void UpdateMaterials() => pendingObject.GetComponent<MeshRenderer>().material = canPlace ? _materials[0] : _materials[1]; 

    private float RoundToNearestGrid(float pos) 
    {
        float xDiff = pos % gridSize;
        pos -= xDiff;
        if (xDiff > (gridSize / 2))
            pos += gridSize;
        return pos;
    }

    public void RotateObject() => pendingObject.transform.Rotate(Vector3.up, rotateAmount); 

    public void ToggleGrid() => _gridEnabled = _gridToggle.isOn;

    public void PlaceObject()
    {
        pendingObject.GetComponent<MeshRenderer>().material = _materials[2];
        pendingObject = null;
    }

    public void SelectObject(int index) => pendingObject = Instantiate(gameObjects[index], _position, transform.rotation); 
}
