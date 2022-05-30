using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_Base_2 : MonoBehaviour
{
    private float _damage;
    private float _timer;
    private int _applyNTimes;
    private float _perTime;
    [SerializeField] private element _weaponElement;
    public enum element
    {
        None,
        Fire,
        Ice,
        Poison,
        Lightning
    }

    public element WeaponElement { get => _weaponElement; set => _weaponElement = value; }

    public float Damage { get => _damage; set => _damage = value; }
    public int ApplyNTimes { get => ApplyNTimes1; set => ApplyNTimes1 = value; }
    public int ApplyNTimes1 { get => _applyNTimes; set => _applyNTimes = value; }
    public float PerTime { get => _perTime; set => _perTime = value; }

    private void Update()
    {
        
        _timer += Time.deltaTime;
        if (_timer >= 3)
        {
            Destroy(transform.parent.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            switch (WeaponElement)
            {
                case element.None:
                    collision.GetComponent<EnemyBase>().HitElement = EnemyBase.element.None;
                    break;
                case element.Fire:
                    collision.GetComponent<EnemyBase>().HitElement = EnemyBase.element.Fire;
                    break;
                case element.Ice:
                    collision.GetComponent<EnemyBase>().HitElement = EnemyBase.element.Ice;
                    break;
                case element.Poison:
                    collision.GetComponent<EnemyBase>().HitElement = EnemyBase.element.Poison;
                    break;
                case element.Lightning:
                    collision.GetComponent<EnemyBase>().HitElement = EnemyBase.element.Lightning;
                    break;
            }

            collision.GetComponent<Rigidbody2D>().GetComponent<IDamageable>().TakeDemage(Damage);
            collision.GetComponent<EnemyBase>().StartDps(ApplyNTimes, Damage / 4, PerTime);
            collision.GetComponent<EnemyBase>().Speed = collision.GetComponent<EnemyBase>().Speed / 2;

        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<EnemyBase>().Speed = collision.GetComponent<EnemyBase>().Speed * 2;
        }
    }
}
