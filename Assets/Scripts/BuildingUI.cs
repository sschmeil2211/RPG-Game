using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingUI : MonoBehaviour
{
    public GameObject buildingUI;

    private void Update()
    {
        if (Input.GetButtonDown("Building"))
        {
            buildingUI.SetActive(!buildingUI.activeSelf); 
            BuildingManager.buildingManagerInstance.IsActive = !BuildingManager.buildingManagerInstance.IsActive;
            Debug.Log(BuildingManager.buildingManagerInstance.IsActive);
        }
    }
}
