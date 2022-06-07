using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blobby : EnemyBase
{
    [Header("State")]
    [SerializeField] EnemyState _currenState = EnemyState.Patrol;
    private Rigidbody2D _rb;
    private Animator _animator;
    [Header("Arrow")]
    [SerializeField] private GameObject _arrow;
    public EnemyState CurrenState { get => _currenState; set => _currenState = value; }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _soulFragmentPrefab.GetComponent<SoulFragments>().SoulFragment = Random.Range(3, 10);
    
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
        if (player != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, Speed * Time.deltaTime);
            _player = player.GetComponent<PlayerController>();
            _currenState = EnemyState.Chase;
        }
    }
    private void Chase()
    {
        _animator.SetBool("CanChase", true);
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
            _animator.SetBool("CanChase", false);

        }
        yield return new WaitForSeconds(_timeToAttack);
        float distance = Vector2.Distance(transform.position, _player.transform.position);

        _animator.SetTrigger("Attack");
        yield return new WaitForSeconds(0.5f);
        if (distance <= _attackRange)
        {

            GameObject projectile = Instantiate(_arrow, transform.position, transform.rotation);
            projectile.GetComponent<Blobby_Projectile>().Damage = _damage;
            Rigidbody2D ArrowRB = projectile.GetComponent<Rigidbody2D>();
            ArrowRB.velocity = projectile.transform.up * 3;

        }
        yield return new WaitForSeconds(0.2f);
        _canAttack = true;
        Speed = currentSpeed;
    }
}
