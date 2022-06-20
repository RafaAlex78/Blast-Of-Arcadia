using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IDamageable
{



    [Header("Player Status")]
    [SerializeField] private float _maxHP = 100;
    [SerializeField] private float _actualHP;

    [Header("Potions")]
    [SerializeField] List<GameObject> _potions;
    private int _potionsAvailable;

    [Header("Movement")]
    private Vector2 _moveInput;
    [SerializeField] private float _movementSpeed;
    private float _sideSpeed;

    [Header("Dash")]
    [SerializeField] float _dashCD;

    [Header("My References")]
    private Rigidbody2D _rigidbody;
    [SerializeField] private WeaponSlot _weapomSlot;
    private GameManager _gm;
    private Animator _animator;
    [SerializeField] private Transform _pistolPos;
    [SerializeField] private TutorialManager _tutorial;


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
    public Transform PistolPos { get => _pistolPos; }
    public WeaponScriptableObject EquippedWeapon { get => _equippedWeapon; set => _equippedWeapon = value; }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();

    }
    void Start()        
    {
        _gm = GameManager.instance;
        if (_actualHP ==0)
        {
            _actualHP = _maxHP;
        }
        _potionsAvailable = 3;
        _gm.Ui.UpdateHpBar(_actualHP, _maxHP);
        _sideSpeed = _movementSpeed / 1.5f;
    }

    // Update is called once per frame
    void Update()
    {
        _moveInput.Normalize();
        if(_weapomSlot.Weapon != null)
        {
        EquippedWeapon = _weapomSlot.Weapon.Weapon;
            if(EquippedWeapon.WeaponType ==Type.Pistol)
            {
                _animator.SetBool("PistolEquipped", true);
                _animator.SetBool("SwordEquipped", false);
            }
            if(EquippedWeapon.WeaponType == Type.Sword)
            {

                _animator.SetBool("PistolEquipped", false);
                _animator.SetBool("SwordEquipped", true);
            }
        }
        _moveInput.x = Input.GetAxisRaw("Horizontal");
        _animator.SetFloat("Movement", _moveInput.magnitude);
        _moveInput.y = Input.GetAxisRaw("Vertical");
        
        PlayerInputToPause();
        if(EquippedWeapon != null)
        { 
        switch (EquippedWeapon.WeaponRarity)
        {
            case WeaponScriptableObject.Rarity.Common:
                _gm.Ui.HablitiesImage[0].gameObject.SetActive(true);
                _gm.Ui.HablitiesImage[1].gameObject.SetActive(false);
                _gm.Ui.HablitiesImage[2].gameObject.SetActive(false);
                _gm.Ui.HablitiesImage[3].gameObject.SetActive(false);
                break;
            case WeaponScriptableObject.Rarity.Uncommon:
                _gm.Ui.HablitiesImage[0].gameObject.SetActive(true);
                _gm.Ui.HablitiesImage[1].gameObject.SetActive(true);
                _gm.Ui.HablitiesImage[2].gameObject.SetActive(false);
                _gm.Ui.HablitiesImage[3].gameObject.SetActive(false);
                break;
            case WeaponScriptableObject.Rarity.Rare:
                _gm.Ui.HablitiesImage[0].gameObject.SetActive(true);
                _gm.Ui.HablitiesImage[1].gameObject.SetActive(true);
                _gm.Ui.HablitiesImage[2].gameObject.SetActive(true);
                _gm.Ui.HablitiesImage[3].gameObject.SetActive(false);
                break;
            case WeaponScriptableObject.Rarity.Legendary:
                _gm.Ui.HablitiesImage[0].gameObject.SetActive(true);
                _gm.Ui.HablitiesImage[1].gameObject.SetActive(true);
                _gm.Ui.HablitiesImage[2].gameObject.SetActive(true);
                _gm.Ui.HablitiesImage[3].gameObject.SetActive(true);
                break;
            default:
                break;
        }

        
        }
        if(_gm.CheckIsPaused() == false)
        {
            if (EquippedWeapon != null)
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
        if (EquippedWeapon != null)
        {
        Gizmos.DrawWireSphere(transform.position, EquippedWeapon.Range);
        Vector2 line = transform.up * EquippedWeapon.Range;
        Vector2 rotateLeftLine = Quaternion.AngleAxis(EquippedWeapon.Angle / 2, transform.forward) * line;
        Vector2 rotateRightLine = Quaternion.AngleAxis(-EquippedWeapon.Angle / 2, transform.forward) * line;

        Debug.DrawRay(transform.position, rotateRightLine, Color.blue);
        Debug.DrawRay(transform.position, rotateLeftLine, Color.blue);

        }
    }
    private void FixedUpdate()
    {
        if(_canMove)
        {
            Movement();
            if(_moveInput== Vector2.zero)
            {
                _rigidbody.angularVelocity = 0;
            }
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
            if(_canAttack && EquippedWeapon is Pistol)
            {
                _canAttack = false;
                EquippedWeapon.UseWeapon(this, _weapomSlot.Weapon);
                _animator.SetTrigger("Shoot");
            }
            if(_canAttack && EquippedWeapon is Sword)
            {
                _canAttack = false;
                StartCoroutine(SwordAttack());
                
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
            if (_canHability2 &&  EquippedWeapon.WeaponRarity != WeaponScriptableObject.Rarity.Common)
            {
                StartCoroutine(UseAbilityTwo());

            }
        } 
        if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (_canHability3 && EquippedWeapon.WeaponRarity == WeaponScriptableObject.Rarity.Rare || _canHability3 && EquippedWeapon.WeaponRarity == WeaponScriptableObject.Rarity.Legendary)
            {
                StartCoroutine(UseAbilityThree());


            }
        } 
        if(Input.GetKeyDown(KeyCode.Alpha4))
        {
            if (_canHability4 && EquippedWeapon.WeaponRarity == WeaponScriptableObject.Rarity.Legendary)
            {
                StartCoroutine(UseAbilityFour());


            }
        }
        if(Input.GetKeyDown(KeyCode.Q))
        {
            if(_potionsAvailable >0 && _actualHP!=_maxHP)
            {
            _actualHP += _maxHP / 4;
            _potionsAvailable--;
            _potions[_potionsAvailable].SetActive(false);
                _gm.Ui.UpdateHpBar(_actualHP, _maxHP);

            }

        }
        

    }
    IEnumerator SwordAttack()
    {
        _animator.SetTrigger("Shoot");
        yield return new WaitForSeconds(0.3f);
        EquippedWeapon.UseWeapon(this, _weapomSlot.Weapon);

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
        EquippedWeapon.UseBaseHability1(this, _weapomSlot.Weapon);
        if(EquippedWeapon is Sword)
        {
            yield return new WaitForSeconds(0.2f);
            EquippedWeapon.UseBaseHability1(this, _weapomSlot.Weapon);
        }  
        yield return new WaitForSeconds(EquippedWeapon.HabilityCastTime);
        
        _canMove = true;
        _gm.Ui.UpdateAblitiesCD(1, EquippedWeapon.HabilityCD);
        yield return new WaitForSeconds(EquippedWeapon.HabilityCD);
        _canHability1 = true;
    } 
    IEnumerator UseAbilityTwo()
    {
        _canHability2 = false;
        _canMove = false;
        EquippedWeapon.UseBaseHability2(this, _weapomSlot.Weapon);
        yield return new WaitForSeconds(EquippedWeapon.HabilityCastTime);
        _canMove = true;
        _gm.Ui.UpdateAblitiesCD(2, EquippedWeapon.HabilityCD);
        yield return new WaitForSeconds(EquippedWeapon.HabilityCD);
        _canHability2 = true;
    }
    IEnumerator UseAbilityThree()
    {
        _canHability3 = false;
        _canMove = false;
        EquippedWeapon.UseElementalHability1(this, _weapomSlot.Weapon);
        yield return new WaitForSeconds(EquippedWeapon.HabilityCastTime);
        _canMove = true;
        _gm.Ui.UpdateAblitiesCD(3, EquippedWeapon.HabilityCD);
        yield return new WaitForSeconds(EquippedWeapon.HabilityCD);
        _canHability3 = true;
    }IEnumerator UseAbilityFour()
    {
        _canHability4 = false;
        _canMove = false;
        EquippedWeapon.UseElementalHability2(this, _weapomSlot.Weapon);
        
        yield return new WaitForSeconds(EquippedWeapon.HabilityCastTime);
        _canMove = true;
        _gm.Ui.UpdateAblitiesCD(4, EquippedWeapon.HabilityCD);
        yield return new WaitForSeconds(EquippedWeapon.HabilityCD);
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
        _gm.Ui.UpdateHpBar(_actualHP, _maxHP);
    
        StartCoroutine(ChangeColor());
        if(_actualHP <= 0)
        {
            Die();
        }
    }
    IEnumerator ChangeColor()
    {
        SpriteRenderer sprite = _rigidbody.GetComponent<SpriteRenderer>();
        sprite.color = Color.red;
        yield return new WaitForSeconds(0.5f);
        sprite.color = Color.white;

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
        if(collision.gameObject.name== "ShowInfo3")
        {
            _tutorial.Info3.SetActive(true);
            _gm.IsPaused = true;
            Destroy(collision.gameObject);
        } 
        if(collision.gameObject.name== "ShowInfo4")
        {
            _tutorial.Info4.SetActive(true);
            _gm.IsPaused = true;
            Destroy(collision.gameObject);
        }
        if(collision.gameObject.name== "ShowInfo5")
        {
            _tutorial.Info5.SetActive(true);
            _gm.IsPaused = true;
            Destroy(collision.gameObject);
        }  
        if(collision.gameObject.name== "Load Scene")
        {
            _gm.Ui.EndTutorial();
        }
    }

}
