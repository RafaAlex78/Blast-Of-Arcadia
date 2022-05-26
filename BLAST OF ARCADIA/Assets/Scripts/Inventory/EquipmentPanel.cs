using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentPanel : MonoBehaviour
{
    [SerializeField] Transform _weaponSlotParent;
    [SerializeField] WeaponSlot _weaponSlot;
    [SerializeField] UpdateStatus _updateStatus;

    private void OnValidate()
    {
        _weaponSlot = _weaponSlotParent.GetComponentInChildren<WeaponSlot>();
    }

    public bool AddItem(WeaponInstance weapon, out WeaponInstance previousWeapon)
    {
       
        if(_weaponSlot.Weapon != null)
        {
            
            previousWeapon = (WeaponInstance)_weaponSlot.Weapon;
            _weaponSlot.Weapon = weapon;
            _updateStatus.GetInfo(weapon.Weapon);
            return true;

        }
        _weaponSlot.Weapon = weapon;
        previousWeapon = null;
        _updateStatus.GetInfo(weapon.Weapon);

        return false;
    }   
    public bool RemoveItem(WeaponInstance weapon)
    {
        _weaponSlot.Weapon=weapon;
         if(_weaponSlot != null)
        {
            _weaponSlot.Weapon = null;
            return true;

        }
         return false;
    }
}
