using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/WeapponScriptableObject/Pistol", order = 2)]


public class Pistol : WeaponScriptableObject
{
    [SerializeField] private GameObject _pistolBullet;
    [SerializeField] private GameObject _hability1Prefab;
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
            default:
                break;
        }
    }

    public override void UseBaseHability2(PlayerController player)
    {
        throw new System.NotImplementedException();
    }

    public override void UseElementalHability1(PlayerController player)
    {
        throw new System.NotImplementedException();
    }

    public override void UseElementalHability2(PlayerController player)
    {
        throw new System.NotImplementedException();
    }

  

   
}
