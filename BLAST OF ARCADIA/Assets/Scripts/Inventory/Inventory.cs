using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private WeaponIntance _weaponIntance;

    [SerializeField] private  List<WeaponScriptableObject> _weapons; //asdas
    [SerializeField] private Transform _itemsParent;
    [SerializeField] private ItemSlot[] _itemSlots;
    [SerializeField] private int _soulFragments;

    public int SoulFragments { get => _soulFragments; set => _soulFragments = value; }

    public event Action<WeaponIntance> OnWeaponRightClickEvent;
    private void Start()
    {
        for (int i = 0; i < _itemSlots.Length; i++)
        {
            _itemSlots[i].OnRightClickEvent += OnWeaponRightClickEvent;
        }
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

    public bool AddItem (WeaponScriptableObject weapon)
    {
        if(IsFull())
        {
            return false;
        }
        _weapons.Add(weapon);
        RefreshUI();
        return true;
    }
    public bool RemoveItem(WeaponScriptableObject weapon)
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
