using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDataBaseScript :MonoBehaviour
{
   [SerializeField] List<WeaponInstance> _weaponDataBase = new List<WeaponInstance>();

    public List<WeaponInstance> WeaponDataBase { get => _weaponDataBase; set => _weaponDataBase = value; }

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
    public void DeleteWeapon(int id)
    {
        Debug.Log("4");
        for (int i = 0;i < WeaponDataBase.Count;i++)
        {
            if (WeaponDataBase[i].Id == id)
            {
                _weaponDataBase.RemoveAt(i);
            }
        }
    }
}
