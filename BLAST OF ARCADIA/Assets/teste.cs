using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teste : MonoBehaviour
{

    private GameManager _gm;
    [SerializeField] WeaponScriptableObject _startWeapon;
    private void Start()
    {
        _gm = GameManager.instance;
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _gm.CreateInstance(_startWeapon);
          //  Destroy(gameObject);
        }
    }
}
