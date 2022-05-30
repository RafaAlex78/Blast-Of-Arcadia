using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    [SerializeField] private  List<WeaponInstance> _weapons; 
    [SerializeField] private Transform _itemsParent;
    [SerializeField] private ItemSlot[] _itemSlots;
    [SerializeField] private ItemSlot _equippedWeappon;
    [SerializeField] private int _soulFragments;
    public enum CristalType
    {
        Common,
        Uncommon,
        Rare,
        Legendary
    }
    private Dictionary<CristalType,int> _cristals = new Dictionary<CristalType, int>(); 
    

    public int SoulFragments { get => _soulFragments; set => _soulFragments = value; }
    public Dictionary<CristalType, int> Cristals { get => _cristals; set => _cristals = value; }
    public ItemSlot EquippedWeappon { get => _equippedWeappon; set => _equippedWeappon = value; }

    public event Action<WeaponInstance> OnWeaponRightClickEvent;
    public event Action<WeaponInstance> OnWeaponRightClickEvent2;
    public event Action<WeaponInstance> OnWeaponRightClickEvent3;
    private void Start()
    {
        
            Cristals.Add(CristalType.Common,0);
            Cristals.Add(CristalType.Uncommon,0);
            Cristals.Add(CristalType.Rare,0);
            Cristals.Add(CristalType.Legendary,0);
        

       

        for (int i = 0; i < _itemSlots.Length; i++)
        {
            _itemSlots[i].OnRightClickEvent += OnWeaponRightClickEvent;
            _itemSlots[i].OnRightClickEvent2 += OnWeaponRightClickEvent2;
        }
        EquippedWeappon.OnRightClickEvent3 += OnWeaponRightClickEvent3;
    }
    private void OnValidate()
    {
        if(_itemsParent != null)
        {
            _itemSlots= _itemsParent.GetComponentsInChildren<ItemSlot>();
        }
        RefreshUI();
    }
    private void RefreshUI()
    {
        int i = 0;
        for (; i < _weapons.Count && i< _itemSlots.Length; i++)
        {
            _itemSlots[i].Weapon = _weapons[i];
        }
        for(; i<_itemSlots.Length; i++)
        {
            _itemSlots[i].Weapon = null;
        }
    }

    public bool AddItem (WeaponInstance weapon)
    {
        if(IsFull())
        {
            return false;
        }
        _weapons.Add(weapon);
        EquippedWeappon.Tooltip.HideToolTip();
            
        RefreshUI();
        return true;
    }
    public bool RemoveItem(WeaponInstance weapon)
    {
        if(_weapons.Remove(weapon))
        {
            RefreshUI();
            return true;
        }
        return false;
    }
    public bool IsFull()
    {
        return _weapons.Count>= _itemSlots.Length;
    }
}
