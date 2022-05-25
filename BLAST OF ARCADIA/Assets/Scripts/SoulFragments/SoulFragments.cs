using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulFragments : MonoBehaviour, ICollectable
{
    [SerializeField]private int _soulFragment;
    private GameManager _gm;

    public int SoulFragment { get => _soulFragment; set => _soulFragment = value; }
    private void Start()
    {
        _gm = GameManager.instance;
    }
    public void Collect(PlayerController player)
    {
        _gm.Inventory.SoulFragments += SoulFragment;
        _gm.Ui.UpdateSFToUI(_gm.Inventory.SoulFragments);
        Destroy(gameObject);
    }
}
