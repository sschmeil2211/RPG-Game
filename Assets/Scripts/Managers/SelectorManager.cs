using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SelectorManager : MonoBehaviour
{
    private BuildingManager _buildingManager;

    public GameObject buildingUI;
    public GameObject selectedBuilding;
    public TextMeshProUGUI textMeshPro;

    private void Start() => _buildingManager = GameObject.Find("BuildingManager").GetComponent<BuildingManager>(); 

    private void Update()
    { 
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 1000))
                if (hit.collider.gameObject.CompareTag("Building"))
                    Select(hit.collider.gameObject);
        }
        if (Input.GetMouseButtonDown(1) && selectedBuilding != null)
            Deselect();  
    }

    private void Select(GameObject building)
    {
        if (building == selectedBuilding)
            return;
        if (selectedBuilding != null)
            Deselect();
        if (building.TryGetComponent<Outline>(out var outline))
            building.AddComponent<Outline>();
        else
            outline.enabled = true;
        textMeshPro.text = building.name;
        buildingUI.SetActive(true);
        selectedBuilding = building;
    }

    private void Deselect()
    {
        buildingUI.SetActive(false);
        selectedBuilding.GetComponent<Outline>().enabled = false;
        selectedBuilding = null;
    }

    public void Move() => _buildingManager.pendingObject = selectedBuilding; 

    public void Delete()
    {
        GameObject buildingToDestroy = selectedBuilding;
        Deselect();
        Destroy(buildingToDestroy);
    }
}
