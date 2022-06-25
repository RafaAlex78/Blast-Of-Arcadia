using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private UIManager _ui;
    [SerializeField] private Inventory _inventory;
    [SerializeField] private CreateInstance _createInstance;
    [SerializeField] private StoreManager _storeManager;
    [SerializeField] private WeaponDataBaseScript _dataBase;
    [SerializeField] private List<Transform> _spawns;
    [SerializeField] private PlayerController _player;
    [SerializeField] private List<Transform> _easyEnemySpawns;
    [SerializeField] private List<Transform> _mediumEnemySpawns;
    [SerializeField] private List<Transform> _hardEnemySpawns;
    [SerializeField] private List<GameObject> _enemies;
    [SerializeField] private List<GameObject> _enemiesSpawned;

    [SerializeField] private GameObject _PressToEnterDungeon;

    private bool _shopOpen=false;
    private bool _isPaused= false;
    private bool _inventoryOpen = false;

    public bool IsPaused { get => _isPaused; set => _isPaused = value; }
    public UIManager Ui { get => _ui; set => _ui = value; }
    public Inventory Inventory { get => _inventory; set => _inventory = value; }
    public bool InventoryOpen { get => _inventoryOpen; set => _inventoryOpen = value; }
    public bool ShopOpen { get => _shopOpen; set => _shopOpen = value; }
    public StoreManager StoreManager { get => _storeManager; }
    public WeaponDataBaseScript DataBase { get => _dataBase;}
    public PlayerController Player { get => _player; set => _player = value; }
    public List<Transform> HardEnemySpawns { get => _hardEnemySpawns; set => _hardEnemySpawns = value; }
    public GameObject PressToEnterDungeon { get => _PressToEnterDungeon; set => _PressToEnterDungeon = value; }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);

        }
        else
        {
            instance = this;
        }
    }
    private void Start()
    {
    }
    private void Update()
    {
        if(CheckIsPaused()== true)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }
    public bool CheckIsPaused()
    {
        if(IsPaused == false)
        {
            return false;
        }
        return true;

        
    }
    public bool CheckInvetoryOpen()
    {
        if(InventoryOpen == false)
        {
            return false;
        }
        return true;

    }
    public void CreateInstance(WeaponScriptableObject weapon)
    {
        _createInstance.CreateWeaponInstance(weapon);
    }
    public void EasyDungeon()
    {
        for (int i = 0; i < _easyEnemySpawns.Count; i++)
        {
            int x = Random.Range(0, 2);
            GameObject enemy = Instantiate(_enemies[x], _easyEnemySpawns[i].position, _easyEnemySpawns[i].rotation);
            _enemiesSpawned.Add(enemy);
        }
        Player.transform.position = _spawns[1].transform.position;
        _ui.EnterDungeon.SetActive(false);
        _isPaused = false;
    } 
    public void MediumDungeon()
    {
       
        for (int i = 0; i < _mediumEnemySpawns.Count; i++)
        {
            int x = Random.Range(0, 3);
           GameObject enemy = Instantiate(_enemies[x], _mediumEnemySpawns[i].position, _mediumEnemySpawns[i].rotation);
            _enemiesSpawned.Add(enemy);
        }
        Player.transform.position = _spawns[2].transform.position;
        _ui.EnterDungeon.SetActive(false);
        _isPaused = false;

    }
    public void HardDungeon()
    {
        for (int i = 0; i < _hardEnemySpawns.Count; i++)
        {
            int x = Random.Range(1, 4);
            GameObject enemy = Instantiate(_enemies[x], _hardEnemySpawns[i].position, _hardEnemySpawns[i].rotation);
            _enemiesSpawned.Add(enemy);
        }
        Player.transform.position = _spawns[3].transform.position;
        _ui.EnterDungeon.SetActive(false);
        _isPaused = false;
    }
    public void ExitDungeon()
    {
        _isPaused = false;
        for (int i = 0; i < _enemiesSpawned.Count; i++)
        {
        if(_enemiesSpawned[i] != null)
            {
                Destroy(_enemiesSpawned[i]);
            }
           
        }
        _player.transform.position = _spawns[0].transform.position;
        _ui.ExitDungeon.SetActive(false);
        _enemiesSpawned.Clear();
    }
    public void DontExitDungeon()
    {
         _isPaused = false;
        _ui.ExitDungeon.SetActive(false);
    }
    public void ResumeGame()
    {
        _isPaused=false;
        _ui.PauseMenu.SetActive(false);
        _player.PauseOpen = false;
    }
    public void CloseEnterDgungeon()
    {
        Ui.EnterDungeon.SetActive(false);
        IsPaused = false;
    }
}

