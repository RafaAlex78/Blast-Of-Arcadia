using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject _inventory;
    [SerializeField] private TMP_Text _soulFragmentsText;
    [SerializeField] private List<TMP_Text> _cristals;
    [SerializeField] private GameObject _ShopConfirmation;
    [SerializeField] private GameObject _sell;
    [SerializeField] private List<Sprite> _cristalsImage;
    [SerializeField] private GameObject _TypeCristalGain;
    [SerializeField] private GameObject _TypeCristalCost;


    public GameObject Inventory { get => _inventory; set => _inventory = value; }
    public GameObject TypeCristalGain { get => _TypeCristalGain; set => _TypeCristalGain = value; }
    public List<Sprite> CristalsImage { get => _cristalsImage; set => _cristalsImage = value; }
    public GameObject TypeCristalCost { get => _TypeCristalCost; set => _TypeCristalCost = value; }

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
    public void UpdateCristals(int type,int number)
    {
        _cristals[type].text= number.ToString();
    }
   public void ShowConfirmation(bool IsEquipped)
    {
        _ShopConfirmation.SetActive(true);
        if (IsEquipped)
        {
            _sell.SetActive(false);
        }
        else
        {
            _sell.SetActive(true);
        }
    } 
    public void HideConfirmation()
    {
        _ShopConfirmation.SetActive(false);
    }

}