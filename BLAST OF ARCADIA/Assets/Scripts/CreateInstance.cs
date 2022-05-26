using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateInstance : MonoBehaviour
{
    WeaponScriptableObject weaponToCerate;
    [SerializeField] private WeaponDataBaseScript _weaponDataBase;

    private GameManager _gm;

    private void Start()
    {
        _gm = GameManager.instance;
    }


    public void CreateWeaponInstance(WeaponScriptableObject weapon)
    {
        weaponToCerate = weapon;
        int newId = GenerateID();
        while (_weaponDataBase.ContainsID(newId) == true)
        {
            newId = GenerateID();
        }


        WeaponInstance nome = new WeaponInstance(weaponToCerate, newId);
        _weaponDataBase.WeaponDataBase.Add(nome);
        _gm.Inventory.AddItem(nome);
    }



    int GenerateID()
    {
        int i = Random.Range(1, 100);
        return i;
    }
}
