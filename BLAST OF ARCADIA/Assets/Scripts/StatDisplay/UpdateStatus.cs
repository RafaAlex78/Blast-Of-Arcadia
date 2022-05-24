using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateStatus : MonoBehaviour
{
    [SerializeField] private StatDisplay[] _statDisplays;
    [SerializeField] private WeaponSlot _weaponSlot;
    [SerializeField] private WeaponScriptableObject _equippedWeapon;
    [SerializeField] private string[] _status;

    private void Awake()
    {
        _statDisplays= GetComponentsInChildren<StatDisplay>();

    }
    private void Start()
    {
        _status = new string[5];
        _equippedWeapon = _weaponSlot.Weapon;
    }
    private void Update()
    {
        _equippedWeapon = _weaponSlot.Weapon;
        if (_equippedWeapon != null)
        {
            GetInfo();

        }
    }
    private void GetInfo()
    {
        _status[0] = (_equippedWeapon.WeaponType.ToString());
        _status[1] = (_equippedWeapon.WeaponRarity.ToString());
        _status[2]=(_equippedWeapon.WeaponElement.ToString());
        _status[3] = (_equippedWeapon.Level.ToString());
        _status[4]=(_equippedWeapon.Damage.ToString());   
        for (int i = 0; i < _statDisplays.Length; i++)
        {
            _statDisplays[i].ValueText.text = _status[i];
        }
        
    
    }
}
