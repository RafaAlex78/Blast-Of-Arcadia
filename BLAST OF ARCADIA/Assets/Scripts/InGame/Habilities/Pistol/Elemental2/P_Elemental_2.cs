using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_Elemental_2 : MonoBehaviour
{
   [SerializeField] private GameObject _target;
    private float _damage;
    private float _speed;
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


    public GameObject Target { get => _target; set => _target = value; }
    public float Damage { get => _damage; set => _damage = value; }
    public float Speed { get => _speed; set => _speed = value; }
    public int ApplyNTimes { get => _applyNTimes; set => _applyNTimes = value; }
    public float PerTime { get => _perTime; set => _perTime = value; }

    void Update()
    {
        if (!_target)
        {
            Destroy(gameObject);
        }
        else
        {
            transform.Rotate(new Vector3(0, 0, 1000 * Time.deltaTime));
            transform.position = Vector3.MoveTowards(transform.position, _target.transform.position, Speed * Time.deltaTime);
        }
        
       
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
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
            collision.GetComponent<Rigidbody2D>().GetComponent<IDamageable>().TakeDemage(Damage*2);
            collision.GetComponent<EnemyBase>().StartDps(ApplyNTimes, Damage*0.45f, PerTime);
            Destroy(gameObject);
        }

        
        
    }
}
