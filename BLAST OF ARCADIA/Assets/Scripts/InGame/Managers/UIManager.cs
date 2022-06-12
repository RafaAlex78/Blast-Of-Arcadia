using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
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

    [SerializeField] private Image _circleBar;
    [SerializeField] private Image _extraBar;
    [SerializeField] float _currentHeaklhp;

    private float _circlePercentage = 0.75f;
    


    public GameObject Inventory { get => _inventory; set => _inventory = value; }
    public GameObject TypeCristalGain { get => _TypeCristalGain; set => _TypeCristalGain = value; }
    public List<Sprite> CristalsImage { get => _cristalsImage; set => _cristalsImage = value; }
    public GameObject TypeCristalCost { get => _TypeCristalCost; set => _TypeCristalCost = value; }

    private void Start()
    {
        if (_cristals.Count ==2)
        {
            Debug.Log("maybe é isto");
        }
        Inventory.SetActive(false);
    }
    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void UpdateSFToUI(int newSF)
    {
        _soulFragmentsText.text = newSF.ToString();

        if(_soulFragmentsText.text== "ola")
        {

        }
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
    public void UpdateHpBar(float currenthP, float MaxHp)
    {
        float healthPercentage = currenthP / MaxHp;
        float circleFill = healthPercentage / _circlePercentage;
        _circleBar.fillAmount = circleFill;
    }
    private void Update()
    {
        float healthPercentage = _currentHeaklhp / 100;
        float circleFill = healthPercentage / _circlePercentage;
        _circleBar.fillAmount = circleFill;
    }
}
