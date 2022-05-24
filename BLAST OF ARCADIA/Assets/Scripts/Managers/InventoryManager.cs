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
    private void EquipFromInventory(WeaponScriptableObject weapon)
    {
        if(weapon is WeaponScriptableObject)
        {
            Equip((WeaponScriptableObject)weapon);
        }
    }

    public void Equip(WeaponScriptableObject weapon)
    {
        Debug.Log("123");
        if (_inventory.RemoveItem(weapon))
        { 
            WeaponScriptableObject previousWeapon;
            

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
    //public void Unequip(WeaponScriptableObject weapon)
    //{
    //    if(!_inventory.IsFull()&& _equipmentPanel.RemoveItem(weapon))
    //    {
    //        _inventory.AddItem(weapon);
    //    }
    //}
}
