using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sell_Upgrade : MonoBehaviour
{
    [SerializeField] private GameManager _gm;

    private void Start()
    {
        _gm = GameManager.instance;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _gm.IsPaused = true;
            _gm.InventoryOpen = true;
            _gm.ShopOpen = true;
            _gm.Ui.Inventory.SetActive(true);
        }
        
    }

}

