using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sell_Upgrade : MonoBehaviour
{
    [SerializeField] private GameManager _gm;
    [SerializeField] private GameObject _openShopWord;
    [SerializeField] private GameObject _ShowButton;

    private void Start()
    {
        _gm = GameManager.instance;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        _openShopWord.SetActive(true);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
       
        if (Input.GetKey(KeyCode.U))
        {
            if(_gm.ShopOpen == false)
            {
                _openShopWord.SetActive(false);
                _ShowButton.SetActive(true);
                _gm.IsPaused = true;
            _gm.InventoryOpen = true;
            _gm.ShopOpen = true;
            _gm.Ui.Inventory.SetActive(true);
            }
           
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        _openShopWord.SetActive(false);
    }
    public void CloseShop()
    {
        _openShopWord.SetActive(true);

        _gm.IsPaused = false;
            _gm.InventoryOpen = false;
            _gm.ShopOpen = false;
            _gm.Ui.Inventory.SetActive(false);
        _ShowButton.SetActive(false);
    }
}

