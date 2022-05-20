using UnityEngine;


public abstract class WeaponScriptableObject : ScriptableObject
{
  

    [SerializeField] private float _damage;
    [SerializeField] private float _range;
    [SerializeField] private float _angle;
    [SerializeField] private float _habilityCD;
    [SerializeField] private float _habilityCastTime;

    [SerializeField] private int _level;
    [SerializeField] private Type _weaponType;
    [SerializeField]
    public enum Type
    {
        Sword,
        Pistol,
    }
    [SerializeField] Rarity _weaponRarity;
    [SerializeField] private enum Rarity
    {
        Common,
        Uncommon,
        Rare,
        Legendary
    }
   
    [SerializeField] Element _weaponElement;
    public enum Element
    {
       None,
       Fire,
       Ice,
       Poison,
       Lightning
    }
  
    
    public float Damage { get => _damage; set => _damage = value; }
    public float Range { get => _range; set => _range = value; }
    public float Angle { get => _angle; set => _angle = value; }
    public Type WeaponType { get => _weaponType; set => _weaponType = value; }
    public Element WeaponElement { get => _weaponElement; set => _weaponElement = value; }
    public float HabilityCD { get => _habilityCD; set => _habilityCD = value; }
    public float HabilityCastTime { get => _habilityCastTime; set => _habilityCastTime = value; }

    
    public abstract void UseWeapon(PlayerController player);
    public abstract void UseBaseHability1(PlayerController player);
    public abstract void UseBaseHability2(PlayerController player);
    public abstract void UseElementalHability1(PlayerController player);
    public abstract void UseElementalHability2(PlayerController player);


}