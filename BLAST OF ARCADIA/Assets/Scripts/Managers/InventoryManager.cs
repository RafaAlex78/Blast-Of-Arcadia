using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private Inventory _inventory;
    [SerializeField] private EquipmentPanel _equipmentPanel;
    private void Awake()
    {
        _inventory.OnWeaponRightClickEvent += EquipFromInventory;
    }
    private void EquipFromInventory(WeaponInstance weapon)
    {
        if(weapon is WeaponInstance)
        {
            Equip((WeaponInstance)weapon);
        }
    }

    public void Equip(WeaponInstance weapon)
    {
        Debug.Log("123");
        if (_inventory.RemoveItem(weapon))
        { 
            WeaponInstance previousWeapon;
            

            if (_equipmentPanel.AddItem(weapon, out previousWeapon))
            {
                Debug.Log("123");
                if (previousWeapon != null)
                {
                    _inventory.AddItem(previousWeapon);
                }
                else
                {
                    _inventory.AddItem(weapon);

                }
            }
        }
    }
  
}
