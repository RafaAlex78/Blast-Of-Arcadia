using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]


public class WeaponInstance 
{
    public WeaponInstance(WeaponScriptableObject weapon, int id)
    {
        _id = id;
        _weapon = weapon;
    }

    [SerializeField] private int _id;
    [SerializeField] private WeaponScriptableObject _weapon;

    public int Id { get => _id; set => _id = value; }
    public WeaponScriptableObject Weapon { get => _weapon; set => _weapon = value; }
}
