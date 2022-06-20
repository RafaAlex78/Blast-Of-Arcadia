using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveInitialWeapon : MonoBehaviour
{

    private GameManager _gm;
    [SerializeField] WeaponScriptableObject _startWeapon;
    [SerializeField] TutorialManager _tM;
    
    private void Start()
    {
        _gm = GameManager.instance;
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _gm.CreateInstance(_startWeapon);
            Destroy(gameObject);
            _tM.Info2.SetActive(true);
            _gm.IsPaused = true;
        }
    }
}
