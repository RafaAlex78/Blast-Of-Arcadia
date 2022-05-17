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
    private float _dotTimer;
    private bool _startDotTimer  =false;

    public element HitElement { get => _hitElement; set => _hitElement = value; }
    public float Speed { get => _speed; set => _speed = value; }

    private void Update()
    {
        if(_startDotTimer)
        {
            _dotTimer += Time.deltaTime;
        }
    }
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
     IEnumerator Dps(int applyDamageNTimes, float damage, float perTime)
    {
        int appliedTimes = 0;
        while (appliedTimes < applyDamageNTimes)
        {
            TakeDemage(damage);
            yield return new WaitForSeconds(perTime);
            appliedTimes++;
            Debug.Log(appliedTimes);
            Debug.Log(applyDamageNTimes);
        }

    }
    public void StartDps(int applyDamageNTimes, float damage, float perTime)
    {
        StartCoroutine(Dps(applyDamageNTimes, damage, perTime));
    }



}
