using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IDamageable
{
    [Header("Player Status")]
    [SerializeField] private int _maxHP;
    [SerializeField] private int _actualHP;

    [Header("Movement")]
    private Vector2 _moveInput;
    [SerializeField] private float _movementSpeed;
    private float _sideSpeed;

    [Header("Dash")]
    [SerializeField] float _dashCD;

    [Header("My References")]
    private Rigidbody2D _rigidbody;

    [Header("conditions")]
    private bool _canMove = true;
    private bool _canDash = true;
    private bool _canAttack;
    private bool _attacking = false;
    private float _attackTimer=0;
    private float _attackCD;
    [SerializeField] private WeaponScriptableObject _equippedWeapon;

    [Header("Habilities")]
    private bool _canHability1 = true;
    private bool _canHability2 = true;
    private bool _canHability3 = true;
    private bool _canHability4 = true;

    public float AttackCD { get => _attackCD; set => _attackCD = value; }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();

    }
    void Start()        
    {
        if(_actualHP ==0)
        {
            _actualHP = _maxHP;
        }
        _sideSpeed = _movementSpeed / 1.5f;
    }

    // Update is called once per frame
    void Update()
    {
        _moveInput.x = Input.GetAxisRaw("Horizontal");
        _moveInput.y = Input.GetAxisRaw("Vertical");


            PlayerInput();
        if(_attacking == false && _canMove ==true)
        {
            Rotation();
            
            
        }
        if(_canMove == false)
        {
            _rigidbody.velocity = Vector3.zero;
        }
        if(_canAttack ==false)
        {
            _attackTimer += Time.deltaTime;
            if (_attackTimer > AttackCD)
            {
                _canAttack = true;
                _attackTimer = 0;
            }

        }
        
    }
    private void OnDrawGizmos()
    {

        Gizmos.DrawWireSphere(transform.position, _equippedWeapon.Range);
        Vector2 line = transform.up * _equippedWeapon.Range;
        Vector2 rotateLeftLine = Quaternion.AngleAxis(_equippedWeapon.Angle / 2, transform.forward) * line;
        Vector2 rotateRightLine = Quaternion.AngleAxis(-_equippedWeapon.Angle / 2, transform.forward) * line;

        Debug.DrawRay(transform.position,rotateRightLine,Color.blue);
        Debug.DrawRay(transform.position,rotateLeftLine,Color.blue);
    }
    private void FixedUpdate()
    {
        if(_canMove)
        {
            Movement();

        }

    }
    private void Movement()
    {
        if(_moveInput.x != 0 && _moveInput.y != 0)
        {

            _rigidbody.velocity = _moveInput * _sideSpeed;
        }
        else
        {
            _rigidbody.velocity = _moveInput * _movementSpeed;
        }
      

    }      
    private void Rotation()
    {
        if(_moveInput.x == 1)
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, -90);
        }
        if(_moveInput.x == -1)
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 90);

        }
        if(_moveInput.y == 1)
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0);

        }if(_moveInput.y == -1)
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 180);
        }
        if(_moveInput.x == 1 && _moveInput.y==1)
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, -45);
        }
        if (_moveInput.x == 1 && _moveInput.y == -1)
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, -135);
        }
        if (_moveInput.x == -1 && _moveInput.y == 1)
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 45);
        }
        if (_moveInput.x == -1 && _moveInput.y == -1)
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 135);
        }
    }
    private void PlayerInput()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if(_canAttack)
            {
                Debug.Log("1");
                _canAttack = false;
                _equippedWeapon.UseWeapon(this);
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if(_canDash)
            {
               StartCoroutine(Dash());
            }
        }
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            if(_canHability1)
            {
                Debug.Log(_equippedWeapon.WeaponType);
                switch (_equippedWeapon.WeaponType)
                {
                    case WeaponScriptableObject.Type.Sword:
                       
                        StartCoroutine(SwordHab1());

                        break;
                    case WeaponScriptableObject.Type.Pistol:
                        StartCoroutine(PistolHab1());
                        break;
                    default:
                        break;
                }                           
            }          
            //if (_equippedWeapon. == WeaponScriptableObject.)
            //{

            //}

            //switch ((string)_equippedWeapon.name)
            //{
            //    case "Sword":
            //        break;
            //}
        } 
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (_canHability2)
            {
                switch (_equippedWeapon.WeaponType)
                {
                    case WeaponScriptableObject.Type.Sword:

                        StartCoroutine(SwordHab2());

                        break;
                    case WeaponScriptableObject.Type.Pistol:
                        break;
                    default:
                        break;
                }


            }
        } 
        if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (_canHability3)
            {
                switch (_equippedWeapon.WeaponType)
                {
                    case WeaponScriptableObject.Type.Sword:

                        StartCoroutine(SwordEle1());

                        break;
                    case WeaponScriptableObject.Type.Pistol:
                        break;
                    default:
                        break;
                }


            }
        } 
        if(Input.GetKeyDown(KeyCode.Alpha4))
        {
            if (_canHability4)
            {
                switch (_equippedWeapon.WeaponType)
                {
                    case WeaponScriptableObject.Type.Sword:

                        StartCoroutine(SwordEle2());

                        break;
                    case WeaponScriptableObject.Type.Pistol:
                        break;
                    default:
                        break;
                }


            }
        }

    }
    IEnumerator UseAbilityOne()
    {
        _canHability1 = false;
        _canMove = false;
        _equippedWeapon.UseBaseHability1(this);

        if(_equippedWeapon is Sword)
        {
            yield return new WaitForSeconds(0.2f);
            _equippedWeapon.UseBaseHability1(this);
        }
   
        yield return new WaitForSeconds(0.3f);
        _canMove = true;
        yield return new WaitForSeconds(1.5f);
        _canHability1 = true;
    }
    IEnumerator SwordHab1()
    {
        _canHability1 = false;
        _canMove = false;
        _equippedWeapon.UseBaseHability1(this);
        yield return new WaitForSeconds(0.2f);
        _equippedWeapon.UseBaseHability1(this);
        yield return new WaitForSeconds(0.3f);
        _canMove =true;
        yield return new WaitForSeconds(1.5f);
        _canHability1 = true;
    } IEnumerator PistolHab1()
    {
        _canHability1 = false;
        _canMove = false;
        _equippedWeapon.UseBaseHability1(this);
        yield return new WaitForSeconds(0.1f);
        _canMove = true;
        yield return new WaitForSeconds(2.5f);
        _canHability1 = true;
    }
    IEnumerator SwordHab2()
    {
        _canHability2 = false;
        _canMove = false;
        _equippedWeapon.UseBaseHability2(this);
        yield return new WaitForSeconds(1.5f);
        _canMove = true;
        yield return new WaitForSeconds(2.5f);
        _canHability2 = true;
    }
    IEnumerator SwordEle1()
    {
        _canHability3 = false;
        _canMove = false;
        _equippedWeapon.UseElementalHability1(this);
        yield return new WaitForSeconds(2f);
        _canMove = true;
        yield return new WaitForSeconds(5.5f);
        _canHability3 = true;
    }
    IEnumerator SwordEle2()
    {
        _canHability4 = false;
        _canMove = false;
        _equippedWeapon.UseElementalHability2(this);
        yield return new WaitForSeconds(2f);
        _canMove = true;
        yield return new WaitForSeconds(10.5f);
        _canHability4 = true;
    }
    IEnumerator PistolHab2()
    {
        _canHability2 = false;
        _canMove = false;
        _equippedWeapon.UseBaseHability2(this);
        yield return new WaitForSeconds(0.5f);
        _canMove = true;
        yield return new WaitForSeconds(6.5f);
        _canHability1 = true;
    }
    IEnumerator Dash()
    {
        _canDash = false;
        if (Input.GetKey(KeyCode.A))
        {
            transform.position = new Vector2(transform.position.x + -2, transform.position.y);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.position = new Vector2(transform.position.x + +2, transform.position.y);

        }
        if (Input.GetKey(KeyCode.W))
        {
            transform.position = new Vector2(transform.position.x, transform.position.y + 2);

        }
        else if (Input.GetKey(KeyCode.S))
        {
            transform.position = new Vector2(transform.position.x, transform.position.y + -2);
        }
        yield return new WaitForSeconds(_dashCD);
        _canDash = true;

    }
   

    public void TakeDemage(int amount)
    {
        throw new System.NotImplementedException();
    }

    public void Die()
    {
        throw new System.NotImplementedException();
    }

    public void TakeDemage(float amount)
    {
        throw new System.NotImplementedException();
    }
}
