using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDataBaseScript :MonoBehaviour
{
   [SerializeField] List<WeaponIntance> _weaponDataBase = new List<WeaponIntance>();

    public List<WeaponIntance> WeaponDataBase { get => _weaponDataBase; set => _weaponDataBase = value; }

    public bool ContainsID(int id)
    {
        for (int i = 0; i < WeaponDataBase.Count; i++)
        {
            if (WeaponDataBase[i].Id == id)
            {
                return true;
            }

        }
        return false;
    }
}
