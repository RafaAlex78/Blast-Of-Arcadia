using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : EnemyBase
{
    [Header("State")]
    [SerializeField] EnemyState _currenState = EnemyState.Patrol;
    [Header("Arrow")]
    [SerializeField] private GameObject _arrow;
    public EnemyState CurrenState { get => _currenState; set => _currenState = value; }

    private void Update()
    {
        switch (CurrenState)
        {
            case EnemyState.Patrol:
                Search();
                break;
            case EnemyState.Chase:
                Chase();
                break;

        }

    }
    public enum EnemyState
    {
        Patrol,
        Chase

    }

    private void Search()
    {
        Collider2D player = Physics2D.OverlapCircle(transform.position, _targetRange, LayerMask.GetMask("Player"));
        if (player != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, Speed * Time.deltaTime);
            _player = player.GetComponent<PlayerController>();
            _currenState = EnemyState.Chase;
        }
    }
    private void Chase()
    {

        transform.position = Vector3.MoveTowards(transform.position, _player.transform.position, Speed * Time.deltaTime);
        Vector2 dir = _player.transform.position - transform.position;
        transform.up = dir;
        float distance = Vector2.Distance(transform.position, _player.transform.position);
        if (distance <= _attackRange)
        {

            if (_canAttack)
            {
                StartCoroutine(Attack());
            }


        }
    }
    IEnumerator Attack()
    {
        float currentSpeed = Speed;
        _canAttack = false;
        if (_loseSpeedOnAttack)
        {
            Speed = 0;

        }
        yield return new WaitForSeconds(_timeToAttack);
        float distance = Vector2.Distance(transform.position, _player.transform.position);

        Debug.Log("1");

        if (distance <= _attackRange)
        {
            Debug.Log("2");

            GameObject newArrow = Instantiate(_arrow, transform.position, transform.rotation);
            newArrow.GetComponent<ArrowEnemy>().Damage = _damage;
            Rigidbody2D ArrowRB = newArrow.GetComponent<Rigidbody2D>();
            ArrowRB.velocity = newArrow.transform.up * 5;

        }
        yield return new WaitForSeconds(0.2f);
        _canAttack = true;
        Speed = currentSpeed;
    }
}
