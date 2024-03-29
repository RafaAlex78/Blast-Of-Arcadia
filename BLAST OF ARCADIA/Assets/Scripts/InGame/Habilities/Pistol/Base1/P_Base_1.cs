using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_Base_1 : MonoBehaviour

{
    private float _damage;
    private float _distance;
    private Vector2 _pistolPos;

    public enum element
    {
        None,
        Fire,
        Ice,
        Poison,
        Lightning
    }

    [SerializeField] private element _weaponElement;

    public float Damage { get => _damage; set => _damage = value; }
    public Vector2 PistolPos { get => _pistolPos; set => _pistolPos = value; }
    public element WeaponElement { get => _weaponElement; set => _weaponElement = value; }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _distance = Vector2.Distance(PistolPos, transform.position);
        
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
            Debug.Log(_distance);
            collision.GetComponent<Rigidbody2D>().GetComponent<IDamageable>().TakeDemage(Damage+_distance);
            Destroy(gameObject);

        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag != "Enemy")
        {
            Destroy(gameObject);
        }
    }

}
