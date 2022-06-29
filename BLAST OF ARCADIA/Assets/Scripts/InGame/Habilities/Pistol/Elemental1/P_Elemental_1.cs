using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_Elemental_1 : MonoBehaviour
{
    private float _damage;
    private float _timeToExplode;
    private float _timeToHit;
    private int _applyNTimes;
    private float _perTime;
    private bool _collided =false;
    private float _timer1 = 0;
    private float _timer2 = 0;
    private bool _gotBigger =false;
    private float _enemySpeed;
    private Animator _animator;
    [SerializeField] private element _weaponElement;
    [SerializeField] private GameObject _circle;

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
    public float TimeToExplode { get => _timeToExplode; set => _timeToExplode = value; }
    public float TimeToHit { get => _timeToHit; set => _timeToHit = value; }
    public int ApplyNTimes { get => _applyNTimes; set => _applyNTimes = value; }
    public float PerTime { get => _perTime; set => _perTime = value; }

    void Update()
    {
        if(_collided== false)
        {
            _timer1 += Time.deltaTime;
        }
        else
        {
            _timer2 += Time.deltaTime;


        }
        if (_timer1 >= TimeToHit)
        {
            Destroy(transform.parent.gameObject);
        }
        if (_timer2 >= _timeToExplode)
        {
            Destroy(transform.parent.gameObject);

        }
        //if(_gotBigger)
        //{
        //    transform.Rotate(new Vector3(0, 0, 1) * Time.deltaTime * 500);


        //}
    }
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    private void Bigger()
    {
        //gameObject.transform.localScale = new Vector3(2, 2, 1);
        //_gotBigger=true;

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Enemy"))
        {
            _enemySpeed = collision.GetComponent<EnemyBase>().Speed;
            //Bigger();
            this.GetComponent<CircleCollider2D>().radius = 6.8f;
            _circle.SetActive(true);
            _animator.SetBool("Start", true);
            _collided = true;
            collision.GetComponent<EnemyBase>().Speed = collision.GetComponent<EnemyBase>().Speed / 2;
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
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<EnemyBase>().Speed = collision.GetComponent<EnemyBase>().Speed * 2;

            if (_timer2 <= _timeToExplode)
            {
                Debug.Log("1");
                collision.GetComponent<EnemyBase>().StartDps(ApplyNTimes, Damage*0.13f, PerTime);
            }
            else
            {
                collision.GetComponent<Rigidbody2D>().GetComponent<IDamageable>().TakeDemage(Damage * 1.25f);
                collision.GetComponent<EnemyBase>().StartDps(ApplyNTimes, Damage * 0.13f, PerTime);

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
                Debug.Log("2");

            }
            collision.GetComponent<EnemyBase>().Speed = _enemySpeed;

        }
    }
}
