using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class StoreManager : MonoBehaviour
{
    private GameManager _gm;
    private WeaponInstance _weaponInstance;
    private WeaponInstance _equipedWeapon;
    [SerializeField] private UpdateStatus _updateStatus;
    private bool _isTheEquipped = false;

    public WeaponInstance WeaponInstance { get => _weaponInstance; set => _weaponInstance = value; }
    public WeaponInstance EquipedWeapon { get => _equipedWeapon; set => _equipedWeapon = value; }
    public bool IsTheEquipped { get => _isTheEquipped; set => _isTheEquipped = value; }

    private void Start()
    {
        _gm = GameManager.instance;

    }
    public void GiveCristals(WeaponInstance weaponInstance)
    {
        float baseCristals = 5;
        int level = weaponInstance.NewLevel;
        for (int i = 1; i <= level; i++)
        {
            baseCristals *= 1.4f;
        }
        int cristals = Mathf.RoundToInt(baseCristals);
        switch (weaponInstance.Weapon.WeaponRarity)
        {
            case WeaponScriptableObject.Rarity.Common:
                _gm.Inventory.Cristals[0] += cristals;
                int common = _gm.Inventory.Cristals[Inventory.CristalType.Common];
                _gm.Ui.UpdateCristals(0, common);
                break;
            case WeaponScriptableObject.Rarity.Uncommon:
                _gm.Inventory.Cristals[Inventory.CristalType.Uncommon] += cristals;
                int Uncommon = _gm.Inventory.Cristals[Inventory.CristalType.Uncommon];
                _gm.Ui.UpdateCristals(1, Uncommon);
                break;
            case WeaponScriptableObject.Rarity.Rare:
                _gm.Inventory.Cristals[Inventory.CristalType.Rare] += cristals;
                int rare = _gm.Inventory.Cristals[Inventory.CristalType.Rare];
                _gm.Ui.UpdateCristals(2, rare);
                break;
            case WeaponScriptableObject.Rarity.Legendary:
                _gm.Inventory.Cristals[Inventory.CristalType.Legendary] += cristals;
                int legendary = _gm.Inventory.Cristals[Inventory.CristalType.Legendary];
                _gm.Ui.UpdateCristals(3, legendary);
                break;
            default:
                break;
        }
    }
    public void UpgradeWeapon()
    {
        float critals = 5;
        for (int nivel = 1; nivel <= _weaponInstance.NewLevel+1; nivel++)
        {
            critals *= 1.7f;
        }
        int cristalsNeeded = Mathf.RoundToInt(critals);
        _weaponInstance.NewDamage = Mathf.RoundToInt(_weaponInstance.NewDamage* 1.2f);
        _weaponInstance.NewLevel++;
        _gm.Ui.HideConfirmation();
        if(_isTheEquipped)
        {

        _updateStatus.GetInfo(_weaponInstance.Weapon, _weaponInstance);
        }


    }
    public void ShowButtons()
    {
        //mostrar custo upgrade
        _gm.Ui.ShowConfirmation();
        float baseCristals = 5;
        int level = WeaponInstance.NewLevel;
        for (int i = 1; i <= level; i++)
        {
            baseCristals *= 1.5f;
        }
        float critals = 5;
        for (int nivel = 1; nivel <= _weaponInstance.NewLevel + 1; nivel++)
        {
            critals *= 1.7f;
        }
        int cristalsNeeded = Mathf.RoundToInt(critals);



        int cristalsToGain = Mathf.RoundToInt(baseCristals);
        if (_weaponInstance.Weapon.WeaponRarity==WeaponScriptableObject.Rarity.Common)
        {
            _gm.Ui.TypeCristalGain.GetComponent<Image>().sprite = _gm.Ui.CristalsImage[0];
            _gm.Ui.TypeCristalGain.GetComponentInChildren<TMP_Text>().text="+ "+ cristalsToGain.ToString(); 
            _gm.Ui.TypeCristalCost.GetComponent<Image>().sprite = _gm.Ui.CristalsImage[0];
            _gm.Ui.TypeCristalCost.GetComponentInChildren<TMP_Text>().text="- "+ cristalsNeeded.ToString();
        }
        if (_weaponInstance.Weapon.WeaponRarity == WeaponScriptableObject.Rarity.Uncommon)
        {
            _gm.Ui.TypeCristalGain.GetComponent<Image>().sprite = _gm.Ui.CristalsImage[1];
            _gm.Ui.TypeCristalGain.GetComponentInChildren<TMP_Text>().text = "+ " + cristalsToGain.ToString();
            _gm.Ui.TypeCristalCost.GetComponent<Image>().sprite = _gm.Ui.CristalsImage[1];
            _gm.Ui.TypeCristalCost.GetComponentInChildren<TMP_Text>().text = "- " + cristalsNeeded.ToString();

        }
        if (_weaponInstance.Weapon.WeaponRarity == WeaponScriptableObject.Rarity.Rare)
        {
            _gm.Ui.TypeCristalGain.GetComponent<Image>().sprite = _gm.Ui.CristalsImage[2];
            _gm.Ui.TypeCristalGain.GetComponentInChildren<TMP_Text>().text = "+ " + cristalsToGain.ToString();
            _gm.Ui.TypeCristalCost.GetComponent<Image>().sprite = _gm.Ui.CristalsImage[2];
            _gm.Ui.TypeCristalCost.GetComponentInChildren<TMP_Text>().text = "- " + cristalsNeeded.ToString();

        }
        if (_weaponInstance.Weapon.WeaponRarity == WeaponScriptableObject.Rarity.Legendary)
        {
            _gm.Ui.TypeCristalGain.GetComponent<Image>().sprite = _gm.Ui.CristalsImage[3];
            _gm.Ui.TypeCristalGain.GetComponentInChildren<TMP_Text>().text = "+ " + cristalsToGain.ToString();
            _gm.Ui.TypeCristalCost.GetComponent<Image>().sprite = _gm.Ui.CristalsImage[3];
            _gm.Ui.TypeCristalCost.GetComponentInChildren<TMP_Text>().text = "- " + cristalsNeeded.ToString();

        }
    }
    public void Sell()
    {

        _gm.StoreManager.GiveCristals(_weaponInstance);
        _gm.DataBase.DeleteWeapon(_weaponInstance.Id);
        _gm.Inventory.RemoveItem(_weaponInstance);
        _gm.Ui.HideConfirmation();
    }
    public void Cancel()
    {
        _gm.Ui.HideConfirmation();
    }

}
