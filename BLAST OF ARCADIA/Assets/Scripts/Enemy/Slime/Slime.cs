using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : EnemyBase
   
{
    [Header("State")]
    [SerializeField] EnemyState _currenState = EnemyState.Patrol;
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
        if(player != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, Speed * Time.deltaTime);
            _player = player.GetComponent<PlayerController>();
            _currenState = EnemyState.Chase;
        }
    }
    private void Chase()
    {
        Debug.Log(Vector2.Distance(transform.position, _player.transform.position));
        Debug.Log(Vector2.Angle(transform.up, _player.transform.position));
        transform.position = Vector3.MoveTowards(transform.position, _player.transform.position, Speed * Time.deltaTime);
        Vector2 dir = _player.transform.position - transform.position;
        transform.up = dir;
        float distance = Vector2.Distance(transform.position,_player.transform.position);
        if (Vector2.Angle(transform.position, _player.transform.position) <= _angle && distance <=_attackRange)
        {
         
            if(_canAttack)
            {
                StartCoroutine(Attack());
            }
            // obj.GetComponent<IDamageable>()?.TakeDemage(Damage);


        }
    }
    IEnumerator Attack()
    {
        _canAttack = false;
        if(_loseSpeedOnAttack)
        {
            Speed = Speed / 6;

        }
        yield return new WaitForSeconds(_timeToAttack);
        float distance = Vector2.Distance(transform.position, _player.transform.position);

        Debug.Log("1");
        
        if (Vector2.Angle(transform.position, _player.transform.position) <= _angle && distance <= _attackRange)
        {
            Debug.Log("2");

            _player.GetComponent<IDamageable>()?.TakeDemage(_damage);
        }
        yield return new WaitForSeconds(0.8f);
        _canAttack = true;
        Speed = Speed * 6;
    }
}
