using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : EnemyBase
   
{
    [Header("Pathing")]
    [SerializeField] private List<Transform> _waypoints;
    private int _currentTarget=0;
    private bool _targetReached=false;
    private bool _reverse=false;
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
        transform.position = Vector3.MoveTowards(transform.position, _player.transform.position, Speed * Time.deltaTime);
        Vector2 dir = _player.transform.position - transform.position;
        transform.up = dir;
        float distance = Vector2.Distance(transform.position,_player.transform.position);
        if (Vector2.Angle(dir, _player.transform.up) <= _angle && distance <=_attackRange)
        {
            if(_canAttack)
            {

            }
            // obj.GetComponent<IDamageable>()?.TakeDemage(Damage);


        }
    }
    IEnumerator Attack()
    {
        yield return new WaitForSeconds(0.5f);
    }
}
