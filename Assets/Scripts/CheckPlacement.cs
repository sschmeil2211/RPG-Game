using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPlacement : MonoBehaviour
{
    private BuildingManager _buildingManager; 

    private void Start() => _buildingManager = GameObject.Find("BuildingManager").GetComponent<BuildingManager>(); 

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Building"))
            _buildingManager.canPlace = false;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Building"))
            _buildingManager.canPlace = true;
    }
}
