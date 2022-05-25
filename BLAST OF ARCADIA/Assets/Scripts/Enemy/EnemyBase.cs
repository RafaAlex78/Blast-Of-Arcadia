using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour, IDamageable
{
    [SerializeField] protected float _hP;
    [SerializeField] private float speed;
    [SerializeField] element _hitElement;
    [SerializeField] element _weakness;
    [SerializeField] protected float _damage;
    [SerializeField] protected float _attackRange;
    [SerializeField] protected float _targetRange;
    [SerializeField] protected float _angle;
    [SerializeField] protected bool _canAttack = true;
    [SerializeField] protected bool _loseSpeedOnAttack;
    [SerializeField] protected float _timeToAttack;
    [SerializeField] protected GameManager _gm;
    [SerializeField] protected GameObject _soulFragmentPrefab;

    [SerializeField] protected PlayerController _player;
    private float _dotTimer;
    private bool _startDotTimer  =false;

    public element HitElement { get => _hitElement; set => _hitElement = value; }
    public float Speed { get => speed; set => speed = value; }

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
    private void OnDrawGizmos()
    {

        Gizmos.DrawWireSphere(transform.position, _targetRange);
        Gizmos.DrawWireSphere(transform.position, _attackRange);
        Vector2 line = transform.up * _attackRange;
        Vector2 rotateLeftLine = Quaternion.AngleAxis(_angle / 2, transform.forward) * line;
        Vector2 rotateRightLine = Quaternion.AngleAxis(-_angle / 2, transform.forward) * line;

        Debug.DrawRay(transform.position, rotateRightLine, Color.blue);
        Debug.DrawRay(transform.position, rotateLeftLine, Color.blue);
    }
    private void Awake()
    {
        _gm = GameManager.instance;
    }
    public void Die()
    {
        Debug.Log("AIDEAD");
        Drop();
        Destroy(gameObject);
    }
    private void Drop()
    {
        _gm.NumberOfEnemies--;
        Instantiate(_soulFragmentPrefab, transform.position, transform.rotation);
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
            _hP = 100;
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
