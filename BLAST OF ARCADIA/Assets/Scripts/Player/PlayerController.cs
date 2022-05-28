using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IDamageable
{



    [Header("Player Status")]
    [SerializeField] private float _maxHP = 100;
    [SerializeField] private float _actualHP;

    [Header("Movement")]
    private Vector2 _moveInput;
    [SerializeField] private float _movementSpeed;
    private float _sideSpeed;

    [Header("Dash")]
    [SerializeField] float _dashCD;

    [Header("My References")]
    private Rigidbody2D _rigidbody;
    [SerializeField] private WeaponSlot _weapomSlot;
    [SerializeField] GameManager _gm;

    [Header("conditions")]
    private bool _canMove = true;
    private bool _canDash = true;
    private bool _canAttack;
    private bool _attacking = false;
    private float _attackTimer=0;
    private float _attackCD;
    private bool _invetoryOpen=false;
    private bool _pauseOpen=false;
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
        _gm = GameManager.instance;
        if (_actualHP ==0)
        {
            _actualHP = _maxHP;
        }
        _sideSpeed = _movementSpeed / 1.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if(_weapomSlot.Weapon != null)
        {
        _equippedWeapon = _weapomSlot.Weapon.Weapon;

        }
        _moveInput.x = Input.GetAxisRaw("Horizontal");
        _moveInput.y = Input.GetAxisRaw("Vertical");
        
        PlayerInputToPause();
        if(_gm.CheckIsPaused() == false)
        {
            if (_equippedWeapon != null)
            {
                PlayerInput();
            }

            if (_attacking == false && _canMove == true)
            {
                FaceDirection();


            }
            if (_canMove == false)
            {
                _rigidbody.velocity = Vector3.zero;
            }
            if (_canAttack == false)
            {
                _attackTimer += Time.deltaTime;
                if (_attackTimer > AttackCD)
                {
                    _canAttack = true;
                    _attackTimer = 0;
                }

            }
        }

        
        
    }
    private void OnDrawGizmos()
    {
        if (_equippedWeapon != null)
        {
        Gizmos.DrawWireSphere(transform.position, _equippedWeapon.Range);
        Vector2 line = transform.up * _equippedWeapon.Range;
        Vector2 rotateLeftLine = Quaternion.AngleAxis(_equippedWeapon.Angle / 2, transform.forward) * line;
        Vector2 rotateRightLine = Quaternion.AngleAxis(-_equippedWeapon.Angle / 2, transform.forward) * line;

        Debug.DrawRay(transform.position, rotateRightLine, Color.blue);
        Debug.DrawRay(transform.position, rotateLeftLine, Color.blue);

        }
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
    private void FaceDirection()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Vector2 direction = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);
        transform.up = direction;
    }
   
    private void PlayerInput()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if(_canAttack)
            {
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
                StartCoroutine(UseAbilityOne());
            }          
        } 
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (_canHability2 &&  _equippedWeapon.WeaponRarity != WeaponScriptableObject.Rarity.Common)
            {
                StartCoroutine(UseAbilityTwo());

            }
        } 
        if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (_canHability3 && _equippedWeapon.WeaponRarity == WeaponScriptableObject.Rarity.Rare || _equippedWeapon.WeaponRarity == WeaponScriptableObject.Rarity.Legendary)
            {
                StartCoroutine(UseAbilityThree());


            }
        } 
        if(Input.GetKeyDown(KeyCode.Alpha4))
        {
            if (_canHability4 && _equippedWeapon.WeaponRarity == WeaponScriptableObject.Rarity.Legendary)
            {
                StartCoroutine(UseAbilityFour());


            }
        }
        

    }
    private void PlayerInputToPause()
    {
       
        if (Input.GetKeyDown(KeyCode.I))
        {
            if(_pauseOpen == false && _gm.ShopOpen==false)
            {

               if (_gm.CheckIsPaused()==false)
                {
                    _gm.IsPaused = true;
                    _gm.Ui.Inventory.SetActive(true);
                    _gm.InventoryOpen = true;
                    return;
                }
                else
                {
                _gm.IsPaused = false;
                _gm.Ui.Inventory.SetActive(false);
                _gm.InventoryOpen = false;
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_gm.CheckIsPaused() == false && _gm.CheckInvetoryOpen() == false && _gm.ShopOpen == false)
            {
                _gm.IsPaused = true;
                _pauseOpen = true;
                return;
            }
            if (_gm.CheckIsPaused() && _gm.CheckInvetoryOpen() == false && _gm.ShopOpen == false)
            {
                Debug.Log(_gm.CheckIsPaused());
                Debug.Log(_gm.CheckInvetoryOpen());
                _gm.IsPaused = false;
                _pauseOpen = false;
                return;
            }
            if (_gm.CheckIsPaused() && _gm.CheckInvetoryOpen() && _gm.ShopOpen==false)
            {
                _gm.IsPaused = false;
                _gm.InventoryOpen = false;
                _gm.Ui.Inventory.SetActive(false);
                return;
            }
            if(_gm.ShopOpen)
            {
                _gm.IsPaused = false;
                _gm.InventoryOpen = false;
                _gm.ShopOpen = false;
                _gm.Ui.Inventory.SetActive(false);
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
        yield return new WaitForSeconds(_equippedWeapon.HabilityCastTime);
        
        _canMove = true;
        yield return new WaitForSeconds(_equippedWeapon.HabilityCD);
        _canHability1 = true;
    } 
    IEnumerator UseAbilityTwo()
    {
        _canHability2 = false;
        _canMove = false;
        _equippedWeapon.UseBaseHability2(this);
        yield return new WaitForSeconds(_equippedWeapon.HabilityCastTime);
        _canMove = true;
        yield return new WaitForSeconds(_equippedWeapon.HabilityCD);
        _canHability2 = true;
    }
    IEnumerator UseAbilityThree()
    {
        _canHability3 = false;
        _canMove = false;
        _equippedWeapon.UseElementalHability1(this);
        yield return new WaitForSeconds(_equippedWeapon.HabilityCastTime);
        _canMove = true;
        yield return new WaitForSeconds(_equippedWeapon.HabilityCD);
        _canHability3 = true;
    }IEnumerator UseAbilityFour()
    {
        _canHability4 = false;
        _canMove = false;
        _equippedWeapon.UseElementalHability2(this);
        yield return new WaitForSeconds(_equippedWeapon.HabilityCastTime);
        _canMove = true;
        yield return new WaitForSeconds(_equippedWeapon.HabilityCD);
        _canHability4 = true;
    }
    IEnumerator Dash()
    {
        _canDash = false;
        Debug.Log("1");
        _rigidbody.AddForce(transform.up * 6500, ForceMode2D.Force);
        yield return new WaitForSeconds(4);
        _canDash = true;


    }


    public void TakeDemage(float amount)
    {
        _actualHP-=amount;
        if(_actualHP <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        _gm.Ui.ReloadScene();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.GetComponent<ICollectable>()!=null)
        {
            ICollectable collectable = collision.transform.GetComponent<ICollectable>();
            collectable.Collect(this);
        }
    }

}
