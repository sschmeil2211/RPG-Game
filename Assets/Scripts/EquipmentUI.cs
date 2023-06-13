using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EquipmentUI : MonoBehaviour
{
    public GameObject equipmentUI;
    public PlayerStats playerStats;
    public TextMeshProUGUI armor;
    public TextMeshProUGUI damage;

    private void Update()
    {
        if (Input.GetButtonDown("Equipment"))
            equipmentUI.SetActive(!equipmentUI.activeSelf);
        armor.text = "Armadura: " + playerStats.armor.GetBaseValue().ToString();
        damage.text = "Daño: " + playerStats.damage.GetBaseValue().ToString();
    }
}
