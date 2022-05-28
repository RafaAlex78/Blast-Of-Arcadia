using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class StoreManager : MonoBehaviour
{
    private GameManager _gm;
    private WeaponInstance _weaponInstance;

    public WeaponInstance WeaponInstance { get => _weaponInstance; set => _weaponInstance = value; }

    private void Start()
    {
        _gm = GameManager.instance;

    }
    public void GiveCristals(WeaponInstance weaponInstance)
    {
        float baseCristals = 5;
        int level = weaponInstance.Weapon.Level;
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
    public void ShowButtons()
    {
        _gm.Ui.ShowConfirmation();
    }
    public void Sell()
    {
        Debug.Log(_weaponInstance.Id);

        _gm.StoreManager.GiveCristals(_weaponInstance);
        Debug.Log(_weaponInstance.Id);
        _gm.DataBase.DeleteWeapon(_weaponInstance.Id);
        _gm.Inventory.RemoveItem(_weaponInstance);
        _gm.Ui.HideConfirmation();
    }
    public void Cancel()
    {
        _gm.Ui.HideConfirmation();
    }

}