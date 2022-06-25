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
    [SerializeField] private GameObject _shopConfirmation;
    [SerializeField] private GameObject _sell;
    [SerializeField] private List<Sprite> _cristalsImage;
    [SerializeField] private GameObject _typeCristalGain;
    [SerializeField] private GameObject _typeCristalCost;
    [SerializeField] private GameObject _enterDungeon;
    [SerializeField] private GameObject _exitDungeon;
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject _startGameInfo;

    [SerializeField] private List<Image> _hablitiesImage;

    [SerializeField] private Image _circleBar;
    [SerializeField] private Image _extraBar;
    [SerializeField] private List<float> _timers;
    [SerializeField] private List<bool> _startTimer;
    [SerializeField] private List<float> _abilitieCD;
    private GameManager _gm;

    


    public GameObject Inventory { get => _inventory; set => _inventory = value; }
    public GameObject TypeCristalGain { get => _typeCristalGain; set => _typeCristalGain = value; }
    public List<Sprite> CristalsImage { get => _cristalsImage; set => _cristalsImage = value; }
    public GameObject TypeCristalCost { get => _typeCristalCost; set => _typeCristalCost = value; }
    public List<Image> HablitiesImage { get => _hablitiesImage; set => _hablitiesImage = value; }
    public GameObject EnterDungeon { get => _enterDungeon; set => _enterDungeon = value; }
    public GameObject ExitDungeon { get => _exitDungeon; set => _exitDungeon = value; }
    public GameObject PauseMenu { get => _pauseMenu; set => _pauseMenu = value; }
    public GameObject StartGameInfo { get => _startGameInfo; set => _startGameInfo = value; }

    private void Start()
    {
        if (_cristals.Count ==2)
        {
            Debug.Log("maybe é isto");
        }
        Inventory.SetActive(false);
        _gm = GameManager.instance;
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
        _shopConfirmation.SetActive(true);
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
        _shopConfirmation.SetActive(false);
    }
    private void Update()
    {
        if(_startTimer[0])
        {
            HablitiesImage[0].fillAmount = 0;
            _timers[0] +=Time.deltaTime;
            HablitiesImage[0].fillAmount =_timers[0]/_abilitieCD[0];
            if(_timers[0]>=_abilitieCD[0])
            {
                _startTimer[0] = false;
                _timers[0] = 0;
            }
        }
        if(_startTimer[1])
        {
            HablitiesImage[1].fillAmount = 0;
            _timers[1] += Time.deltaTime;
            HablitiesImage[1].fillAmount = _timers[1] / _abilitieCD[1];
            if (_timers[1] >= _abilitieCD[1])
            {
                _startTimer[1] = false;
                _timers[1] = 0;
            }
        }
        if(_startTimer[2])
        {
            HablitiesImage[2].fillAmount = 0;
            _timers[2] += Time.deltaTime;
            HablitiesImage[2].fillAmount = _timers[2] / _abilitieCD[2];
            if (_timers[2] >= _abilitieCD[2])
            {
                _startTimer[2] = false;
                _timers[2] = 0;
            }
        }
        if(_startTimer[3])
        {
            HablitiesImage[3].fillAmount = 0;
            _timers[3] += Time.deltaTime;
            HablitiesImage[3].fillAmount = _timers[3] / _abilitieCD[3];
            if (_timers[3] >= _abilitieCD[3])
            {
                _startTimer[3] = false;
                _timers[3] = 0;
            }
        }
        if(_startTimer[4])
        {
            HablitiesImage[4].fillAmount = 0;
            _timers[4] += Time.deltaTime;
            HablitiesImage[4].fillAmount = _timers[4] / _abilitieCD[4];
            if (_timers[4] >= _abilitieCD[4])
            {
                _startTimer[4] = false;
                _timers[4] = 0;
            }
        }
    }
    public void UpdateAblitiesCD(int habilitieNumber,  float cd)
    {


        _startTimer[habilitieNumber - 1] = true;
        _abilitieCD[habilitieNumber - 1] = cd;
    }
    
    public void UpdateHpBar(float currenthP, float maxHp)
    {
        float healthPercentage1 = currenthP / (maxHp / 4 * 3);
        float healthPercentage2 = currenthP / (maxHp / 4);
        if (currenthP <= maxHp * 0.75f)
        {
            _extraBar.fillAmount = healthPercentage1;
            _circleBar.fillAmount = 0;
        }
        if (currenthP >= maxHp * 0.75f)
        {

            _extraBar.fillAmount = 1;

            _circleBar.fillAmount = (((currenthP - maxHp * 0.75f) / (maxHp * 0.25f)) * 0.75f) + 0.25f;
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

    public override bool Equals(object obj)
    {
        return obj is UIManager manager &&
               base.Equals(obj) &&
               EqualityComparer<List<Image>>.Default.Equals(_hablitiesImage, manager._hablitiesImage);
    }
    public void EndTutorial()
    {
        SceneManager.LoadScene("Game");
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void CloseStartGameInfo()
    {
       StartGameInfo.SetActive(false);
    }
}
