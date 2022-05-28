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
        _newDamage = weapon.Damage;
        _newLevel = weapon.Level;
        
    }

    [SerializeField] private int _id;
    [SerializeField] private WeaponScriptableObject _weapon;
    [SerializeField] private int _newDamage;
    [SerializeField] private int _newLevel ;

    public int Id { get => _id; set => _id = value; }
    public WeaponScriptableObject Weapon { get => _weapon; set => _weapon = value; }
    public int NewDamage { get => _newDamage; set => _newDamage = value; }
    public int NewLevel { get => _newLevel; set => _newLevel = value; }
}
