using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordBaseHab1 : MonoBehaviour
{
    private float _damage;
    private float _range;
    private Vector2 _playerPos;
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
    public float Range { get => _range; set => _range = value; }
    public Vector2 PlayerPos { get => _playerPos; set => _playerPos = value; }

    private void Update()
    {
    
        if (Vector2.Distance(PlayerPos, transform.position) >= Range )
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Enemy"))
        {

            collision.GetComponent<Rigidbody2D>().GetComponent<IDamageable>().TakeDemage(Damage);
            Destroy(gameObject);
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
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
