using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private Inventory _inventory;
    [SerializeField] private EquipmentPanel _equipmentPanel;
    [SerializeField] private WeaponDataBaseScript _dataBase;
    private GameManager _gm;
    private void Awake()
    {
        _inventory.OnWeaponRightClickEvent += EquipFromInventory;
        _inventory.OnWeaponRightClickEvent2 += teste;
    }
    private void Start()
    {
        _gm = GameManager.instance;
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
        if (_inventory.RemoveItem(weapon))
        { 
            WeaponInstance previousWeapon;
            

            if (_equipmentPanel.AddItem(weapon, out previousWeapon))
            {
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
    private void teste(WeaponInstance weapon)
    {
        _gm.StoreManager.ShowButtons();
        _gm.StoreManager.WeaponInstance=weapon;
       
    }
  
}
