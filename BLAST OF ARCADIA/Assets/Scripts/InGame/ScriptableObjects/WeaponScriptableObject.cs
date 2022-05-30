using UnityEngine;

public enum Type
{
    Sword,
    Pistol,
}

public abstract class WeaponScriptableObject : ScriptableObject
{
    [SerializeField] private Sprite _icon;
    [SerializeField] private string _name;


    [SerializeField] private int _damage;
    [SerializeField] private float _range;
    [SerializeField] private float _angle;
    [SerializeField] private float _habilityCD;
    [SerializeField] private float _habilityCastTime;

    [SerializeField] private int _level;
    [SerializeField] public Type _weaponType;
   
    [SerializeField] Rarity _weaponRarity;
    public enum Rarity
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
  
    
    public int Damage { get => _damage; set => _damage = value; }
    public float Range { get => _range; set => _range = value; }
    public float Angle { get => _angle; set => _angle = value; }
    public Type WeaponType { get => _weaponType; set => _weaponType = value; }
    public Element WeaponElement { get => _weaponElement; set => _weaponElement = value; }
    public float HabilityCD { get => _habilityCD; set => _habilityCD = value; }
    public float HabilityCastTime { get => _habilityCastTime; set => _habilityCastTime = value; }
    public Sprite Icon { get => _icon; set => _icon = value; }
    public string Name { get => _name; set => _name = value; }
    public Rarity WeaponRarity { get => _weaponRarity; set => _weaponRarity = value; }
    public int Level { get => _level; set => _level = value; }
    
    public abstract void UseWeapon(PlayerController player,WeaponInstance weaponInstance);
    public abstract void UseBaseHability1(PlayerController player,WeaponInstance weaponInstance);
    public abstract void UseBaseHability2(PlayerController player, WeaponInstance weaponInstance);
    public abstract void UseElementalHability1(PlayerController player,WeaponInstance weaponInstance);
    public abstract void UseElementalHability2(PlayerController player, WeaponInstance weaponInstance);


}