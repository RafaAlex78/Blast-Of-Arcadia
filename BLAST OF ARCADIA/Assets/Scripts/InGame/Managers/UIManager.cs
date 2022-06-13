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
    [SerializeField] float _maxHP;

    private float _circlePercentage = 1.25f;
    


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
        float healthPercentage1 = _currentHeaklhp / (_maxHP/4*3);
        float healthPercentage2 = _currentHeaklhp / (_maxHP / 4);
        if(_currentHeaklhp <= _maxHP * 0.75f)
        {
        _extraBar.fillAmount = healthPercentage1;
            _circleBar.fillAmount = 0;
        }
        if(_currentHeaklhp>= _maxHP * 0.75f)
        {
           
            _extraBar.fillAmount = 1;
            Debug.Log((_currentHeaklhp - _maxHP*0.75f) / 25);

            _circleBar.fillAmount = (((_currentHeaklhp - _maxHP * 0.75f) / (_maxHP*0.25f))* 0.75f)+ 0.25f;
            //(_currentHeaklhp - 0.75f) * 0.25


            /*        float bar1full = (_maxHP / 4) * 3;
                float bar2MaxHp = _maxHP - bar1full;

                _circleBar.fillAmount = 0.25f+((_currentHeaklhp -bar1full)/bar2MaxHp);
            Debug.Log((_currentHeaklhp - bar1full) / bar2MaxHp);
            Debug.Log((_currentHeaklhp - bar1full) / bar2MaxHp);*/
        }
        /*
        1 - 25
        x - currenthp- maxhp*0.75(0.14) 

        1 - 0.75
        x - currenthp

        */

        //float circleFill = healthPercentage / _circlePercentage;
        
    }
}
