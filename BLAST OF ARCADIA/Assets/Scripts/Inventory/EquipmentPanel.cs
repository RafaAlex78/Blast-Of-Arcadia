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

    public bool AddItem(WeaponScriptableObject weapon, out WeaponScriptableObject previousWeapon)
    {
       
        if(_weaponSlot.Weapon != null)
        {
            
            previousWeapon = (WeaponScriptableObject)_weaponSlot.Weapon;
            _weaponSlot.Weapon = weapon;
            _updateStatus.GetInfo(weapon);
            return true;

        }
        _weaponSlot.Weapon = weapon;
        previousWeapon = null;
        _updateStatus.GetInfo(weapon);

        return false;
    }   public bool RemoveItem(WeaponScriptableObject weapon)
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
