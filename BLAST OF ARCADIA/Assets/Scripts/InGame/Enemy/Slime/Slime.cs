using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : EnemyBase
   
{
    [Header("State")]
    [SerializeField] EnemyState _currenState = EnemyState.Patrol;
    private Rigidbody2D _rb;
    private Animator _animator;
    public EnemyState CurrenState { get => _currenState; set => _currenState = value; }

    private void Start()
    {
        _gm = GameManager.instance;

        _soulFragmentPrefab.GetComponent<SoulFragments>().SoulFragment = Random.Range(1, 5);
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }
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
       _animator.SetBool("CanChase",true);
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
        _animator.SetTrigger("Attack");

        yield return new WaitForSeconds(_timeToAttack);
        float distance = Vector2.Distance(transform.position, _player.transform.position);
        
        if (Vector2.Angle(transform.position, _player.transform.position) <= _angle && distance <= _attackRange)
        {
            Debug.Log("damage");
            _player.GetComponent<IDamageable>()?.TakeDemage(_damage);
        }
        yield return new WaitForSeconds(0.8f);
        _canAttack = true;
        Speed = Speed * 6;
    }
}
