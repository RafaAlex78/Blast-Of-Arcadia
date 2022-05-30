using UnityEngine;
using System.Text;
using TMPro;

public class ItemToolTip : MonoBehaviour
{
    [SerializeField] TMP_Text _itemNameText;
    [SerializeField] TMP_Text _itemStatsText;

    private StringBuilder _sb = new StringBuilder();

    public void ShowToolTip(WeaponInstance weaponInstance)
    {
        _itemNameText.text = weaponInstance.Weapon.Name;
        _sb.Length = 0;
        AddStat("Type", weaponInstance.Weapon.WeaponType.ToString());
        AddStat("Rarity", weaponInstance.Weapon.WeaponRarity.ToString());
        AddStat("Element", weaponInstance.Weapon.WeaponElement.ToString());
        AddStat( "Level", weaponInstance.NewLevel.ToString());
        AddStat("Damage", weaponInstance.NewDamage.ToString());
        _itemStatsText.text = _sb.ToString(); 

        gameObject.SetActive(true);

    }
    public void HideToolTip()
    {
        gameObject.SetActive(false);
    }
    private void AddStat(string value, string statName)
    {
        
             if(_sb.Length > 0)
            {
                _sb.AppendLine();
            }
           

            _sb.Append(value);
            _sb.Append(" -> ");
            _sb.Append(statName);
        }
    
       
}
