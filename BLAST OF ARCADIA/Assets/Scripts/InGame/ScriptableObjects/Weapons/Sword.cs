using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/WeapponScriptableObject/Sword", order = 1)]
public class Sword : WeaponScriptableObject
{
    [SerializeField] GameObject _hability1Prefab;
    [SerializeField] GameObject _hability2Prefab;
    [SerializeField] GameObject _hability3Prefab;
    [SerializeField] GameObject _hability4Prefab;

    private void Awake()
    {
        WeaponType = Type.Sword;
    }
    public override void UseWeapon(PlayerController player,WeaponInstance weaponInstance)
    {       

        Collider2D[] objectsHit = Physics2D.OverlapCircleAll(player.transform.position, Range, LayerMask.GetMask("Enemy"));
        foreach (Collider2D obj in objectsHit)
        {
            Vector2 dir = obj.transform.position - player.transform.position;
            Debug.Log(Vector2.Angle(dir , player.transform.up));

            if (Vector2.Angle(dir, player.transform.up) <= Angle)
            {
                Debug.Log("hit");
                obj.GetComponent<IDamageable>()?.TakeDemage(weaponInstance.NewDamage);

                if(obj.CompareTag("Enemy"))
                {
                    switch (WeaponElement)
                    {
                        case Element.None:
                            obj.GetComponent<EnemyBase>().HitElement = EnemyBase.element.None;                         
                                break;
                        case Element.Fire:
                            obj.GetComponent<EnemyBase>().HitElement = EnemyBase.element.Fire;

                            break;
                        case Element.Ice:
                            obj.GetComponent<EnemyBase>().HitElement = EnemyBase.element.Ice;

                            break;
                        case Element.Poison:
                            obj.GetComponent<EnemyBase>().HitElement = EnemyBase.element.Poison;

                            break;
                        case Element.Lightning:
                            obj.GetComponent<EnemyBase>().HitElement = EnemyBase.element.Lightning;
                            break;
                        default:
                            break;
                    }
                }
            }
        }
        player.AttackCD = 0.3f;
   
    }
    public override void UseBaseHability1(PlayerController player, WeaponInstance weaponInstance)
    {
        GameObject newHab = Instantiate(_hability1Prefab, player.transform.position, player.transform.rotation);
        Rigidbody2D bull = newHab.GetComponent<Rigidbody2D>();
        bull.velocity = newHab.transform.up * 5;
        SwordBaseHab1 hab1 = newHab.GetComponent<SwordBaseHab1>();
        hab1.Range = Range*4;
        hab1.PlayerPos = player.transform.position;
        hab1.Damage = weaponInstance.NewDamage;
        switch (WeaponElement)
        {
            case Element.None:
                hab1.WeaponElement = SwordBaseHab1.element.None;
                break;
            case Element.Fire:
                hab1.WeaponElement = SwordBaseHab1.element.Fire;

                break;
            case Element.Ice:
                hab1.WeaponElement = SwordBaseHab1.element.Ice;

                break;
            case Element.Poison:
                hab1.WeaponElement = SwordBaseHab1.element.Poison;

                break;
            case Element.Lightning:
                hab1.WeaponElement = SwordBaseHab1.element.Lightning;

                break;
            default:
                break;
        }
        HabilityCastTime = 0.2f;
        HabilityCD = 4f;
    } 
    
    public override void UseBaseHability2(PlayerController player, WeaponInstance weaponInstance)
    {
        GameObject newHab = Instantiate(_hability2Prefab, player.transform.position, player.transform.rotation);
        SwordBaseHab2[] swordScrpt;
        swordScrpt = newHab.GetComponentsInChildren<SwordBaseHab2>();
       
        foreach (SwordBaseHab2 hab2 in swordScrpt)
        {
            hab2.Speed = 5;
            hab2.Range = Range*6;
            hab2.PlayerPos= player.transform.position;
            hab2.Damage = weaponInstance.NewDamage + 2;
            switch (WeaponElement)
            {
                case Element.None:
                    hab2.WeaponElement = SwordBaseHab2.element.None;
                    break;
                case Element.Fire:
                    hab2.WeaponElement = SwordBaseHab2.element.Fire;

                    break;
                case Element.Ice:
                    hab2.WeaponElement = SwordBaseHab2.element.Ice;

                    break;
                case Element.Poison:
                    hab2.WeaponElement = SwordBaseHab2.element.Poison;

                    break;
                case Element.Lightning:
                    hab2.WeaponElement = SwordBaseHab2.element.Lightning;

                    break;
                default:
                    break;
            }
        }
        HabilityCastTime = 0.3f;
        HabilityCD = 6.5f;
    }

    public override void UseElementalHability1(PlayerController player, WeaponInstance weaponInstance)
    {
        if(WeaponElement == Element.None)
        {
            return;
        }
        else
        {
            GameObject newHab = Instantiate(_hability3Prefab, player.transform.position, player.transform.rotation);
            Rigidbody2D bull = newHab.GetComponentInChildren<Rigidbody2D>();
            S_Element_Hab1 hab3 = bull.GetComponent<S_Element_Hab1>();
            hab3.Range = Range*10;
            hab3.PlayerPos = player.transform.position;
            hab3.Damage = weaponInstance.NewDamage +5;
            hab3.Speed = 4;
            hab3.ApplyNTimes = 8;
            hab3.PerTime = 0.5f;
            switch (WeaponElement)
            {
                case Element.None:
                    hab3.WeaponElement = S_Element_Hab1.element.None;
                    break;
                case Element.Fire:
                    hab3.WeaponElement = S_Element_Hab1.element.Fire;

                    break;
                case Element.Ice:
                    hab3.WeaponElement = S_Element_Hab1.element.Ice;

                    break;
                case Element.Poison:
                    hab3.WeaponElement = S_Element_Hab1.element.Poison;

                    break;
                case Element.Lightning:
                    hab3.WeaponElement = S_Element_Hab1.element.Lightning;

                    break;
                default:
                    break;
            }
            HabilityCastTime = 0.4f;
            HabilityCD = 12.5f;
        }
    }

    public override void UseElementalHability2(PlayerController player, WeaponInstance weaponInstance)
    {
        if (WeaponElement == Element.None)
        {
            return;
        }
        else
        {
            GameObject newHab = Instantiate(_hability4Prefab, player.transform.position, player.transform.rotation);
            Rigidbody2D bull = newHab.GetComponentInChildren<Rigidbody2D>();

            S_Element_Hab2 hab4 = bull.GetComponent<S_Element_Hab2>();
            hab4.Range = Range * 10;
            hab4.PlayerPos = player.transform.position;
            hab4.Damage = weaponInstance.NewDamage +8;
            hab4.Speed = 7;
            switch (WeaponElement)
            {
                case Element.None:
                    hab4.WeaponElement = S_Element_Hab2.element.None;
                    break;
                case Element.Fire:
                    hab4.WeaponElement = S_Element_Hab2.element.Fire;

                    break;
                case Element.Ice:
                    hab4.WeaponElement = S_Element_Hab2.element.Ice;

                    break;
                case Element.Poison:
                    hab4.WeaponElement = S_Element_Hab2.element.Poison;

                    break;
                case Element.Lightning:
                    hab4.WeaponElement = S_Element_Hab2.element.Lightning;

                    break;
                default:
                    break;
            }
            HabilityCastTime = 0.7f;
            HabilityCD = 24f;
        }
    }
}
