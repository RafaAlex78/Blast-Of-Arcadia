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
      
        if(_weaponInstance.Weapon.WeaponRarity == WeaponScriptableObject.Rarity.Common)
        {
            if(_gm.Inventory.Cristals[Inventory.CristalType.Common]>= cristalsNeeded)
            {
                _gm.Inventory.Cristals[Inventory.CristalType.Common]-= cristalsNeeded;
                _weaponInstance.NewDamage = Mathf.RoundToInt(_weaponInstance.NewDamage* 1.2f);
                _weaponInstance.NewLevel++;               
                _gm.Ui.UpdateCristals(0, _gm.Inventory.Cristals[Inventory.CristalType.Common]);
                _gm.Ui.HideConfirmation();
            }
        }
        if(_weaponInstance.Weapon.WeaponRarity == WeaponScriptableObject.Rarity.Uncommon)
        {
            if(_gm.Inventory.Cristals[Inventory.CristalType.Uncommon]>= cristalsNeeded)
            {
                _gm.Inventory.Cristals[Inventory.CristalType.Uncommon]-= cristalsNeeded;
                _weaponInstance.NewDamage = Mathf.RoundToInt(_weaponInstance.NewDamage* 1.2f);
                _weaponInstance.NewLevel++;               
                _gm.Ui.UpdateCristals(1, _gm.Inventory.Cristals[Inventory.CristalType.Uncommon]);
                _gm.Ui.HideConfirmation();
            }
        }
        if (_weaponInstance.Weapon.WeaponRarity == WeaponScriptableObject.Rarity.Rare)
        {
            if (_gm.Inventory.Cristals[Inventory.CristalType.Rare] >= cristalsNeeded)
            {
                _gm.Inventory.Cristals[Inventory.CristalType.Rare] -= cristalsNeeded;
                _weaponInstance.NewDamage = Mathf.RoundToInt(_weaponInstance.NewDamage * 1.2f);
                _weaponInstance.NewLevel++;
                _gm.Ui.UpdateCristals(2, _gm.Inventory.Cristals[Inventory.CristalType.Rare]);
                _gm.Ui.HideConfirmation();
            }
        }
        if (_weaponInstance.Weapon.WeaponRarity == WeaponScriptableObject.Rarity.Legendary)
        {
            if (_gm.Inventory.Cristals[Inventory.CristalType.Legendary] >= cristalsNeeded)
            {
                _gm.Inventory.Cristals[Inventory.CristalType.Legendary] -= cristalsNeeded;
                _weaponInstance.NewDamage = Mathf.RoundToInt(_weaponInstance.NewDamage * 1.2f);
                _weaponInstance.NewLevel++;
                _gm.Ui.UpdateCristals(3, _gm.Inventory.Cristals[Inventory.CristalType.Legendary]);
                _gm.Ui.HideConfirmation();
            }
        }
        if(IsTheEquipped)
        {

            _updateStatus.GetInfo(_weaponInstance.Weapon, _weaponInstance);
        }


    }
    public void ShowButtons()
    {
        //mostrar custo upgrade
        _gm.Ui.ShowConfirmation(IsTheEquipped);
        float baseCristals = 5;
        int level = WeaponInstance.NewLevel;
        for (int i = 1; i <= level; i++)
        {
            baseCristals *= 1.4f;
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
            if(IsTheEquipped)
            {
                _gm.Ui.TypeCristalGain.gameObject.SetActive(false);
            }
            else
            {
                RectTransform rect = _gm.Ui.TypeCristalGain.GetComponent<RectTransform>();
                rect.sizeDelta = new Vector2(50, 50);

                _gm.Ui.TypeCristalGain.gameObject.SetActive(true);
                _gm.Ui.TypeCristalGain.GetComponent<Image>().sprite = _gm.Ui.CristalsImage[0];
                _gm.Ui.TypeCristalGain.GetComponentInChildren<TMP_Text>().text="+ "+ cristalsToGain.ToString(); 

            }
            RectTransform rect2 = _gm.Ui.TypeCristalCost.GetComponent<RectTransform>();
            rect2.sizeDelta = new Vector2(50, 50);

            _gm.Ui.TypeCristalCost.GetComponent<Image>().sprite = _gm.Ui.CristalsImage[0];
            _gm.Ui.TypeCristalCost.GetComponentInChildren<TMP_Text>().text="- "+ cristalsNeeded.ToString();
        }
        if (_weaponInstance.Weapon.WeaponRarity == WeaponScriptableObject.Rarity.Uncommon)
        {
            if (IsTheEquipped)
            {
                _gm.Ui.TypeCristalGain.gameObject.SetActive(false);
            }
            else
            {
                RectTransform rect = _gm.Ui.TypeCristalGain.GetComponent<RectTransform>();
                rect.sizeDelta = new Vector2(50, 50);

                _gm.Ui.TypeCristalGain.gameObject.SetActive(true);
                _gm.Ui.TypeCristalGain.GetComponent<Image>().sprite = _gm.Ui.CristalsImage[1];
                _gm.Ui.TypeCristalGain.GetComponentInChildren<TMP_Text>().text = "+ " + cristalsToGain.ToString();

            }
            RectTransform rect2 = _gm.Ui.TypeCristalCost.GetComponent<RectTransform>();
            rect2.sizeDelta = new Vector2(50, 50);

            _gm.Ui.TypeCristalCost.GetComponent<Image>().sprite = _gm.Ui.CristalsImage[1];
            _gm.Ui.TypeCristalCost.GetComponentInChildren<TMP_Text>().text = "- " + cristalsNeeded.ToString();

        }
        if (_weaponInstance.Weapon.WeaponRarity == WeaponScriptableObject.Rarity.Rare)
        {
            if (IsTheEquipped)
            {
                _gm.Ui.TypeCristalGain.gameObject.SetActive(false);
            }
            else
            {
                _gm.Ui.TypeCristalGain.gameObject.SetActive(true);
                RectTransform rect = _gm.Ui.TypeCristalGain.GetComponent<RectTransform>();
                rect.sizeDelta = new Vector2(50, 50);
                _gm.Ui.TypeCristalGain.GetComponent<Image>().sprite = _gm.Ui.CristalsImage[2];
                _gm.Ui.TypeCristalGain.GetComponentInChildren<TMP_Text>().text = "+ " + cristalsToGain.ToString();

            }
            RectTransform rect2 = _gm.Ui.TypeCristalCost.GetComponent<RectTransform>();
            rect2.sizeDelta = new Vector2(50, 50);
            _gm.Ui.TypeCristalCost.GetComponent<Image>().sprite = _gm.Ui.CristalsImage[2];
            _gm.Ui.TypeCristalCost.GetComponentInChildren<TMP_Text>().text = "- " + cristalsNeeded.ToString();

        }
        if (_weaponInstance.Weapon.WeaponRarity == WeaponScriptableObject.Rarity.Legendary)
        {
            if (IsTheEquipped)
            {
                _gm.Ui.TypeCristalGain.gameObject.SetActive(false);
            }
            else
            {
                _gm.Ui.TypeCristalGain.gameObject.SetActive(true);
                RectTransform rect = _gm.Ui.TypeCristalGain.GetComponent<RectTransform>();
                rect.sizeDelta = new Vector2(40, 97);
                _gm.Ui.TypeCristalGain.GetComponent<Image>().sprite = _gm.Ui.CristalsImage[3];
            _gm.Ui.TypeCristalGain.GetComponentInChildren<TMP_Text>().text = "+ " + cristalsToGain.ToString();
            }
            RectTransform rect2 = _gm.Ui.TypeCristalCost.GetComponent<RectTransform>();
            rect2.sizeDelta = new Vector2(40, 97);

            _gm.Ui.TypeCristalCost.GetComponent<Image>().sprite = _gm.Ui.CristalsImage[3];
            _gm.Ui.TypeCristalCost.GetComponentInChildren<TMP_Text>().text = "- " + cristalsNeeded.ToString();

        }
        Debug.Log(_gm.Ui.TypeCristalCost.GetComponent<RectTransform>().sizeDelta);
    }
    public void Sell()
    {
        Debug.Log(_weaponInstance);
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
