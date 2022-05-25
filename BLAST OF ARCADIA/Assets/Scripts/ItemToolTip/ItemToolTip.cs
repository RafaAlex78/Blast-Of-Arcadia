using UnityEngine;
using System.Text;
using TMPro;

public class ItemToolTip : MonoBehaviour
{
    [SerializeField] TMP_Text _itemNameText;
    [SerializeField] TMP_Text _itemStatsText;

    private StringBuilder _sb = new StringBuilder();

    public void ShowToolTip(WeaponScriptableObject weapon)
    {
        _itemNameText.text = weapon.Name;
        _sb.Length = 0;
        AddStat("Type", weapon.WeaponType.ToString());
        AddStat("Rarity", weapon.WeaponRarity.ToString());
        AddStat("Element", weapon.WeaponElement.ToString());
        AddStat( "Level", weapon.Level.ToString());
        AddStat("Damage", weapon.Damage.ToString());
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
