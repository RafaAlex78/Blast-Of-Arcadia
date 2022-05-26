using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateStatus : MonoBehaviour
{
    [SerializeField] private StatDisplay[] _statDisplays;
    [SerializeField] private string[] _status;

    private void Awake()
    {
        _statDisplays= GetComponentsInChildren<StatDisplay>();

    }
    private void Start()
    {
        _status = new string[5];
        _status[0] = "None";
        _status[1] = "None";
        _status[2] = "None";
        _status[3] = "None";
        _status[4] = "None";
        for (int i = 0; i < _statDisplays.Length; i++)
        {
            _statDisplays[i].ValueText.text = _status[i];
        }
    }
   
    public void GetInfo(WeaponScriptableObject weapon)
    {
        _status[0] = (weapon.WeaponType.ToString());
        _status[1] = (weapon.WeaponRarity.ToString());
        _status[2]=(weapon.WeaponElement.ToString());
        _status[3] = (weapon.Level.ToString());
        _status[4]=(weapon.Damage.ToString());   
        for (int i = 0; i < _statDisplays.Length; i++)
        {
            _statDisplays[i].ValueText.text = _status[i];
        }
        
    
    }
}
