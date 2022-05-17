using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/WeapponScriptableObject/Pistol", order = 2)]


public class Pistol : WeaponScriptableObject
{
    [SerializeField] private GameObject _pistolBullet;
    [SerializeField] private GameObject _hability1Prefab;
    [SerializeField] private GameObject _hability2Prefab;
    [SerializeField] private GameObject _hability3Prefab;
    [SerializeField] private int _shootCount;
    public override void UseWeapon(PlayerController player)
    {
        GameObject newBullet = Instantiate(_pistolBullet, player.transform.position, player.transform.rotation);
        newBullet.GetComponent<Pistol_Bullet>().Range = Range;
        newBullet.GetComponent<Pistol_Bullet>().PlayerPos = player.transform.position;
        Rigidbody2D bulletRB = newBullet.GetComponent<Rigidbody2D>();
        bulletRB.velocity = newBullet.transform.up * 5;
        Pistol_Bullet bull = newBullet.GetComponent<Pistol_Bullet>();
        if (_shootCount< 5)
        {
            player.AttackCD = 0.3f;
            _shootCount++;
           
            bull.Damage = Damage;
            
            switch (WeaponElement)
            {
                case Element.None:
                    bull.WeaponElement = Pistol_Bullet.element.None;
                    break;
                case Element.Fire:
                    bull.WeaponElement = Pistol_Bullet.element.Fire;

                    break;
                case Element.Ice:
                    bull.WeaponElement = Pistol_Bullet.element.Ice;

                    break;
                case Element.Poison:
                    bull.WeaponElement = Pistol_Bullet.element.Poison;

                    break;
                case Element.Lightning:
                    bull.WeaponElement = Pistol_Bullet.element.Lightning;

                    break;
                default:
                    break;
            }
        }
        else
        {
            switch (WeaponElement)
            {
                case Element.None:
                    bull.WeaponElement = Pistol_Bullet.element.None;
                    break;
                case Element.Fire:
                    bull.WeaponElement = Pistol_Bullet.element.Fire;

                    break;
                case Element.Ice:
                    bull.WeaponElement = Pistol_Bullet.element.Ice;

                    break;
                case Element.Poison:
                    bull.WeaponElement = Pistol_Bullet.element.Poison;

                    break;
                case Element.Lightning:
                    bull.WeaponElement = Pistol_Bullet.element.Lightning;

                    break;
                default:
                    break;
            }
            player.AttackCD = 1f;
            newBullet.GetComponent<Pistol_Bullet>().Damage = Damage*1.5f;
            _shootCount = 0;
        }
      
    }
    public override void UseBaseHability1(PlayerController player)
    {
        GameObject newHab = Instantiate(_hability1Prefab, player.transform.position, player.transform.rotation);
        Rigidbody2D bull = newHab.GetComponent<Rigidbody2D>();
        bull.velocity = newHab.transform.up * 5;
        P_Base_1 hab1 = newHab.GetComponent<P_Base_1>();
        hab1.PlayerPos = player.transform.position;
        hab1.Damage = Damage;
        switch (WeaponElement)
        {
            case Element.None:
                hab1.WeaponElement = P_Base_1.element.None;
                break;
            case Element.Fire:
                hab1.WeaponElement = P_Base_1.element.Fire;

                break;
            case Element.Ice:
                hab1.WeaponElement = P_Base_1.element.Ice;

                break;
            case Element.Poison:
                hab1.WeaponElement = P_Base_1.element.Poison;

                break;
            case Element.Lightning:
                hab1.WeaponElement = P_Base_1.element.Lightning;

                break;
        }
        HabilityCastTime = 0.5f;
        HabilityCD = 1.5f;
    }

    public override void UseBaseHability2(PlayerController player)
    {
        GameObject newHab = Instantiate(_hability2Prefab, player.transform.position, player.transform.rotation);

        P_Base_2 hab2 = newHab.GetComponentInChildren<P_Base_2>();

        hab2.Damage = Damage;

        switch (WeaponElement)
        {
            case Element.None:
                hab2.WeaponElement = P_Base_2.element.None;
                break;
            case Element.Fire:
                hab2.WeaponElement = P_Base_2.element.Fire;

                break;
            case Element.Ice:
                hab2.WeaponElement = P_Base_2.element.Ice;

                break;
            case Element.Poison:
                hab2.WeaponElement = P_Base_2.element.Poison;

                break;
            case Element.Lightning:
                hab2.WeaponElement = P_Base_2.element.Lightning;
                break;
        }
        Debug.Log("5");
        hab2.ApplyNTimes = 4;
        hab2.PerTime = 1.5f;


        HabilityCastTime = 1.5f;
        HabilityCD = 6.5f;
    }

    public override void UseElementalHability1(PlayerController player)
    {
        GameObject newHab = Instantiate(_hability3Prefab, player.transform.position, player.transform.rotation);

        P_Elemental_1 hab3 = newHab.GetComponentInChildren<P_Elemental_1>();

        hab3.Damage = Damage;

        switch (WeaponElement)
        {
            case Element.None:
                hab3.WeaponElement = P_Elemental_1.element.None;
                break;
            case Element.Fire:
                hab3.WeaponElement = P_Elemental_1.element.Fire;

                break;
            case Element.Ice:
                hab3.WeaponElement = P_Elemental_1.element.Ice;

                break;
            case Element.Poison:
                hab3.WeaponElement = P_Elemental_1.element.Poison;

                break;
            case Element.Lightning:
                hab3.WeaponElement = P_Elemental_1.element.Lightning;
                break;
        }
        hab3.TimeToExplode = 3;
        hab3.TimeToHit = 7;
        hab3.ApplyNTimes = 6;
        hab3.PerTime = 1.2f;
        HabilityCastTime = 1f;
        HabilityCD =10f;

    }

    public override void UseElementalHability2(PlayerController player)
    {
        throw new System.NotImplementedException();
    }

  

   
}
