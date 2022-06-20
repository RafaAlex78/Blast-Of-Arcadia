using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Element_Hab2 : MonoBehaviour
{
    private float _damage;
    private float _range;
    private float _speed;
    private Vector2 _playerPos;
    private float _timer1 = 0;
    private float _timer2 = 0;
    [SerializeField] private bool _expended = false;
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
    public float Speed { get => _speed; set => _speed = value; }

    void Update()
    {
       
        _timer1 += Time.deltaTime;
        if (_timer1 >= 1.6f && Vector2.Distance(PlayerPos, transform.position) <= Range && _expended == false)
        {
            GetComponent<Rigidbody2D>().velocity = transform.up * Speed;
        }
        if (Vector2.Distance(PlayerPos, transform.position) >= Range)
        {

            Bigger();
                   
        }
        
        if (_expended)
        {
            transform.Rotate(new Vector3(0, 0, 1) * Time.deltaTime * -500);

            _timer2 += Time.deltaTime;

        }
        if (_timer2 >= 6 )
        {
            Destroy(transform.parent.gameObject);
        }
    }
    private void Bigger()
    {
        _expended = true;
        gameObject.transform.localScale = new Vector3(1.5f, 1.5f, -1);
        GetComponent<Rigidbody2D>().velocity = transform.up * 0;


    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Enemy"))
        {
            Bigger();
           
            collision.GetComponent<Rigidbody2D>().GetComponent<IDamageable>().TakeDemage(Damage);
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

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.transform.position = Vector2.MoveTowards(collision.transform.position, transform.position, 0.01f);
        }
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if (_timer2 >= 5.9)
            {
                collision.GetComponent<Rigidbody2D>().GetComponent<IDamageable>().TakeDemage(Damage * 4);
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
    }
}
