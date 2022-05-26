using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateInstance : MonoBehaviour
{
    WeaponScriptableObject weaponToCerate;
   [SerializeField] private WeaponDataBaseScript _weaponDataBase;

  


    
    
  public void CreateWeaponInstance(WeaponScriptableObject weapon)
    {
        weaponToCerate = weapon;
        int newId = GenerateID();
        while (_weaponDataBase.ContainsID(newId) == true)
        {
            newId = GenerateID();
        }


        WeaponIntance nome = new WeaponIntance(weaponToCerate, newId);
        _weaponDataBase.WeaponDataBase.Add(nome);
    }



    int GenerateID()
    {
        int i = Random.Range(1, 100);
        return i;
    }
}
