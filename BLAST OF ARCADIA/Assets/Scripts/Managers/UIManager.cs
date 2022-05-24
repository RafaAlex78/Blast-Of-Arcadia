using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
[SerializeField] private GameObject _inventory;

    public GameObject Inventory { get => _inventory; set => _inventory = value; }

    private void Start()
    {
        Inventory.SetActive(false);
    }
}
