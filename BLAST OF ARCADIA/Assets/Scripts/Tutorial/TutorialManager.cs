using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    private GameManager _gm;
    [SerializeField] private GameObject _info1;
    [SerializeField] private GameObject _info2;
    [SerializeField] private GameObject _info3;
    [SerializeField] private GameObject _info4;
    [SerializeField] private GameObject _info5;
    [SerializeField] private GameObject _wall1;
    [SerializeField] private GameObject _wall2;
    [SerializeField] private GameObject _wall3;
    [SerializeField] private List<GameObject> _enemys;
    [SerializeField] private PlayerController _playerController;

    public GameObject Info2 { get => _info2; set => _info2 = value; }
    public GameObject Info3 { get => _info3; set => _info3 = value; }
    public GameObject Info4 { get => _info4; set => _info4 = value; }
    public GameObject Info5 { get => _info5; set => _info5 = value; }

    void Start()
    {
        _gm = GameManager.instance;
        _gm.IsPaused =true;
    }

    // Update is called once per frame
    void Update()
    {
        if(_wall1 != null)
        {
            if(_playerController.EquippedWeapon != null)
            {
                Destroy(_wall1);
            }
        }
        if(_wall2 != null)
        {
            if(_enemys[0] ==null && _enemys[1] == null)
            {
                Destroy(_wall2);
            }
        }   
        if(_wall3 != null)
        {
           if(_gm.Inventory.Cristals[Inventory.CristalType.Legendary] != 0)
            {
                Destroy(_wall3);
            }

            
        }
    }
    public void Hide1()
    {
        _info1.SetActive(false);
        _gm.IsPaused = false;
    } 
    public void Hide2()
    {
        _info2.SetActive(false);
        _gm.IsPaused = false;
    } 
    public void Hide3()
    {
        Info3.SetActive(false);
        _gm.IsPaused = false;
    }
    public void Hide4()
    {
        Info4.SetActive(false);
        _gm.IsPaused = false;
    }
    public void Hide5()
    {
        Info5.SetActive(false);
        _gm.IsPaused = false;
    }
}
