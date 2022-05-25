using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
[SerializeField] private GameObject _inventory;
[SerializeField] private TMP_Text _soulFragmentsText;


    public GameObject Inventory { get => _inventory; set => _inventory = value; }

    private void Start()
    {
        Inventory.SetActive(false);
    }
    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void UpdateSFToUI(int newSF)
    {
        _soulFragmentsText.text = newSF.ToString();
    }
}
