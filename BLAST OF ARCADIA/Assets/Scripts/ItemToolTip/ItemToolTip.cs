using UnityEngine;
using System.Text;
using TMPro;

public class ItemToolTip : MonoBehaviour
{
    [SerializeField] TMP_Text _ItemNameText;
    [SerializeField] TMP_Text _itemStatsText;

    private StringBuilder _sb = new StringBuilder();

    public void ShowToolTip(WeaponScriptableObject weapon)
    {
        _itemStatsText.text = weapon.Name;
        _sb.Length = 0;
        AddStat(weapon.WeaponType.ToString(), "Type");
        AddStat(weapon.WeaponRarity.ToString(), "Rarity");
        AddStat(weapon.WeaponElement.ToString(), "Element");
        AddStat(weapon.Level.ToString(), "Level");
        AddStat(weapon.Damage.ToString(), "Damage");
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
            _sb.Append(" ");
            _sb.Append(statName);
        }
    
       
}
