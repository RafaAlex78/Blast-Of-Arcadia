using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour, IDamageable
{
    [SerializeField] private float _hP=10;
    [SerializeField] private float _speed=5;
    [SerializeField] element _hitElement;
    [SerializeField] element _weakness;
    [SerializeField] private float _damage;

    public element HitElement { get => _hitElement; set => _hitElement = value; }

    public enum element
    {
        None,
        Fire,
        Ice,
        Poison,
        Lightning
    }
    public void Die()
    {
        Destroy(gameObject);
    }

    public void TakeDemage(float amount)
    {
        if(_weakness == HitElement)
        {
            _hP -= amount * 2f;
        }
        else
        {
            _hP -= amount;

        }

        if (_hP <=0)
        {
            Die();
        }
    }


}
