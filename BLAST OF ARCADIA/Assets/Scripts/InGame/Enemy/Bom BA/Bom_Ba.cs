using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bom_Ba : EnemyBase
{
    [Header("State")]
    [SerializeField] EnemyState _currenState = EnemyState.Patrol;
    private Rigidbody2D _rb;
    private Animator _animator;
    [Header("ExplosionRadius")]
    [SerializeField] private float _explosionRadious;
    [SerializeField] private GameObject _growing;
    private bool _explode =false;
    public EnemyState CurrenState { get => _currenState; set => _currenState = value; }

    private void Start()
    {
        _gm = GameManager.instance;

        _soulFragmentPrefab.GetComponent<SoulFragments>().SoulFragment = Random.Range(1, 5);
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _growing = this.gameObject.transform.GetChild(1).gameObject;
    }
    private void Update()
    {
        if(_explode)
        {
           if(_growing.transform.localScale.x < 9)
            {
            _growing.transform.localScale += new Vector3(1, 1,0)*Time.deltaTime*5f;   

            }

        }
            
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
        if (Vector2.Angle(transform.position, _player.transform.position) <= _angle && distance <= _attackRange)
        {

            if (_canAttack)
            {
                StartCoroutine(Attack());
            }


        }
    }
    IEnumerator Attack()
    {
        _explode = true;
        gameObject.transform.GetChild(0).gameObject.SetActive(true);
        _canAttack = false;
        if (_loseSpeedOnAttack)
        {
            Speed = 0;
            _animator.SetTrigger("Attack");

        }
        yield return new WaitUntil(() => _growing.transform.localScale.x>=9);
        float distance = Vector2.Distance(transform.position, _player.transform.position);
        Debug.Log(distance);

        if (Vector2.Angle(transform.position, _player.transform.position) <= _angle && distance <= _explosionRadious)
        {
            Debug.Log(_damage - (distance * 25));
            _player.GetComponent<IDamageable>()?.TakeDemage(_damage-(distance*25));
        }
        yield return new WaitForSeconds(0.4f);
        Destroy(this.gameObject);
       
       
    }
}
